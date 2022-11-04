using Microsoft.Data.Sqlite;

namespace Lesson.Dapper.Example;

public class RawAdoNetExample
{
    public IReadOnlyList<Product> GetProducts()
    {
        var products = new List<Product>();
        var connectionString = "Data Source=hello.db";
        var sql = "select * from products";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqliteCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var product = new Product
                    {
                        ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                        ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    };
                    products.Add(product);
                }
            }
        }
        return products;
    }
}