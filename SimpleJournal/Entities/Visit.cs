using System;

namespace SimpleJournal.Entities;

public class Visit
{
    protected Visit() { }
    public Visit(Guid id, DateOnly date)
    {
        Id = id;
        Date = date;
    }

    public Guid Id { get; init; }
    public DateOnly Date { get; set; }

    public Guid StudentId { get; set; }

    // Конвенция: {property_name}Id
    public Student? Student { get; set; }

    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Date: {Date}, Student: {Student?.Name}, Subject: {Subject?.Name}";
    }
}
