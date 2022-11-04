using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SimpleJournal.Entities;

public class Course
{
    public Course(string name, Subject subject, Group group)
    {
        Name = name;
        Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        Group = group ?? throw new ArgumentNullException(nameof(group));
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public DateTimeOffset StartedAt { get; set; }
    public DateTimeOffset FinishedAt { get; set; }

    public Subject? Subject { get; set; }

    //TODO many-to-many
    public Group? Group { get; set; }

    private readonly ILazyLoader? _lazyLoader;
    protected Course(ILazyLoader lazyLoader)
    {
        _lazyLoader = lazyLoader;
    }

    private IReadOnlyList<Student>? _students;
    public virtual async Task<IReadOnlyList<Student>> GetStudents()
    {
        if (_students is not null) return _students;
        if (Group is null)
        {
            if (_lazyLoader is null)
                throw new NullReferenceException(nameof(_lazyLoader));
            await _lazyLoader.LoadAsync(this, default, nameof(Group));
        }
        if (Group!.Students is null)
        {
            if (_lazyLoader is null)
                throw new NullReferenceException(nameof(_lazyLoader));
            await _lazyLoader.LoadAsync(Group!, default, nameof(Group.Students));
        }
        _students = Group!.Students;
        return Group.Students!;
    }
}