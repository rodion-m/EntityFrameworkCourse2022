using System.Data;
using AdoNetVisitations;
using Lesson.Dapper.Example;
using Microsoft.Data.Sqlite;

namespace Lesson1.AdoNet;

internal class Examples
{
    public IReadOnlyList<Product> GetProducts()
    {
        var result = new List<Product>();
        var connectionString = "Data Source=myapp.db";
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var sql = "select * from Products";
        using var command = new SqliteCommand(sql, connection);
        using var reader = command.ExecuteReader();
        foreach (IDataRecord row in reader)
        {
            var product = new Product
            (
                Id:    (long) row["Id"],
                Name:  (string) row["Name"],
                Price: (decimal) row["Price"]
            );
            result.Add(product);
        }
        return result;
    }
}