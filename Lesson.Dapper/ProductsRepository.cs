using System.Data;
using Dapper;
using Lesson.Dapper.Example;
using Microsoft.Data.Sqlite;

namespace Lesson.Dapper;

public class ProductsRepository : IDisposable
{
    private readonly SqliteConnection _connection;

    public ProductsRepository(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        var sql = "select * from products";
        var products = await _connection.QueryAsync<Product>(sql);
        return products.ToList();
    }

    public async Task AddProductsAsync(IEnumerable<Product> products)
    {
        var sql = @"insert into products (Id, Name, Price) 
                    values (@Id, @Name, @Price)";
        await _connection.QueryAsync<Product>(sql, products);
    }

    public void Dispose() => _connection.Dispose();

    public static async Task Example()
    {
        using var repository = new ProductsRepository("Data Source=hello.db");
        var products = await repository.GetProductsAsync();
        await repository.AddProductsAsync(new[]
        {
            new Product(1, "Салат", 100), 
            new Product(2, "Пицца", 200)
        });
    }
    
    
}