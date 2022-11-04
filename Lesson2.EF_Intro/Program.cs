using Lesson2.EF_Intro;
using Microsoft.EntityFrameworkCore;

await using (var db = new AppDbContext())
{
    //CreateTable(db);
    List<Book> books = await db.Books.ToListAsync();
    foreach (var book in books)
    {
        Console.WriteLine($"{book.Title} by {book.Author}");
    }
}

async Task CreateTable(DbContext db)
{
    await db.Database.ExecuteSqlRawAsync(@"
        create table Books
        (
         Id int primary key, 
         Title text, 
         Author text
        )"
    );
}