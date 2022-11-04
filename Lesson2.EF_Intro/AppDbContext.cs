using Microsoft.EntityFrameworkCore;

namespace Lesson2.EF_Intro;

public class AppDbContext : DbContext
{
    private const string ConnectionString = "Data Source=hello.db";

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString);
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Order> Orders => Set<Order>();
}

public class Order
{
    public long Id { get; set; } // GET И SET
    public string? Phone { get; set; } // GET И SET
    public decimal TotalPrice { get; set; } // GET И SET
}


public class Book
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
}