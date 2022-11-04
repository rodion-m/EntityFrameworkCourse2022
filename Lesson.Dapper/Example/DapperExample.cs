using Dapper;
using Microsoft.Data.Sqlite;

namespace Lesson.Dapper.Example;

public class DapperExample
{
    public IReadOnlyList<Product> GetProducts()
    {
        var connectionString = "Data Source=hello.db";
        var sql = "select * from products";
        using (var connection = new SqliteConnection(connectionString))
        {
            IEnumerable<Product>? products = connection.Query<Product>(sql);
            return products.ToList();
        }
    }
    
    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        var connectionString = "Data Source=hello.db";
        var sql = "select * from products";
        using (var connection = new SqliteConnection(connectionString))
        {
            var products = await connection.QueryAsync<Product>(sql);
            return products.ToList();
        }
    }
    
}