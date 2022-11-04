using Bogus;
using Microsoft.EntityFrameworkCore;
using SimpleJournal;
using SimpleJournal.Entities;
using SimpleJournal.ValueObjects;

namespace SimpleJournalTester;

public class EntitiesGenerator
{
    public static async Task GenerateStudents(int count)
    {
        await using var db = new AppDbContext();
        var groups = await db.Groups.ToListAsync();
        var studentsFaker = new Faker<Student>("ru") //Bogus
                .CustomInstantiator(_ => new Student() { Id = Guid.NewGuid() })
                .RuleFor(it => it.Name, f => f.Name.FullName())
                .RuleFor(it => it.Phone, f => new Phone(f.Phone.PhoneNumber()))
                .RuleFor(it => it.Group, f => f.Random.ListItem(groups))
            ;
        await db.AddRangeAsync(studentsFaker.Generate(count));
        await db.SaveChangesAsync();
    }
}