using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using SimpleJournal.Entities;

namespace SimpleJournal;

public class AppDbContext : DbContext
{
    private readonly bool _enableLogging;
    private readonly string _logFileName;

    //private const string ConnectionString 
    //= @"Data Source=C:\Users\rodio\RiderProjects\EntityFrameworkCourse2022\AttendanceManagerEf\att.db";
    private const string ConnectionString = @"Host=localhost;Database=my_db;Username=my_db;Password=coolpass";

    public AppDbContext(bool enableLogging = true, string? logFileName = null)
    {
        _enableLogging = enableLogging;
        _logFileName = logFileName ?? "simple_journal.log";
    }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
        if (_enableLogging)
        {
            optionsBuilder.LogTo(line =>
                {
                    Console.WriteLine(line);
                    if (_logFileName is not null)
                    {
                        File.AppendAllText(
                            _logFileName, line + Environment.NewLine);
                    }
                })
                .EnableSensitiveDataLogging();
        }
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Course> Courses => Set<Course>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Явная настройка навигационных свойств
        modelBuilder.Entity<Visit>()
            .HasOne(visit => visit.Student)
            .WithMany(student => student.Visits);
        //Для VO:
         // modelBuilder.Entity<Student>()
         //     .OwnsOne(s => s.Phone, 
         //         builder => builder.Property(it => it.Value)
         //             .HasColumnName("phone")
         //         );
    }

    public async Task AddCourse()
    {
        //TODO
    }

    public async Task<List<Course>> GetCoursesWithStudentsWithVisits()
    {
        return await Courses.Include(c => c.Group)
            .ThenInclude(g => g!.Students!)
            .ThenInclude(g => g.Visits!)
            .ThenInclude(v => v.Subject)
            .ToListAsync();
    }

    private async void VisitsSimple()
    {
        var query =
            from v in Visits
            select v;

        List<Visit> list = await query.ToListAsync();
    }

    private async void VisitsSimple2()
    {
        var query =
            from v in Visits
            select v.Date;

        List<DateOnly> list = await query.ToListAsync();
    }

    private async void VisitsSimple3()
    {
        var query =
            from v in Visits
            join stud in Students on v.StudentId equals stud.Id
            select new { Date = v.Date, Student = stud.Name };

        var list = await query.ToListAsync();

        Visits.Join(Students,
            v => v.StudentId,
            stud => stud.Id,
            (v, stud) =>
                new
                {
                    Date = v.Date,
                    Student = stud.Name
                }
        );
    }

    private async void VisitsFullJoined()
    {
        var query =
            from v in Visits
            join stud in Students on v.StudentId equals stud.Id
            join subj in Subjects on v.SubjectId equals subj.Id
            select new
            {
                Date = v.Date,
                Student = stud.Name,
                Subject = subj.Name
            };

        var list = await query.ToListAsync();

        Visits
            .Join(Students,
                v => v.StudentId,
                stud => stud.Id,
                (v, stud) => new { v, stud }
            )
            .Join(Subjects,
                v_stud => v_stud.v.SubjectId,
                subj => subj.Id,
                (v_stud, subj) =>
                    new
                    {
                        Date = v_stud.v.Date,
                        Student = v_stud.stud.Name,
                        Subject = subj.Name
                    }
            );
    }

    public async Task FillDb()
    {
        var visit = new Visit(
            id: Guid.NewGuid(), date: DateOnly.FromDateTime(DateTime.Now))
        {
            Student = new Student(id: Guid.Empty, name: "Иммануил Кант"),
            Subject = new Subject(id: Guid.Empty, name: "Философия"),
        };
        await Visits.AddAsync(visit);
        await SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Visit>> GetAllVisitsInclude()
    {
        List<Visit> list = await Visits
            .Include(it => it.Student)
            .Include(it => it.Subject)
            .ToListAsync();
        return list;
    }

    public async Task<Visit> GetFirstVisitLoadReference()
    {
        Visit visit = await Visits.FirstAsync();
        await Entry(visit).Reference(v => v.Student).LoadAsync();
        await Entry(visit).Reference(v => v.Subject).LoadAsync();
        return visit;
    }
    
    public async Task<Group> GetBiggestGroup()
    {
        Group group = await Groups
            .OrderByDescending(g => g.Students!.Count)
            .FirstAsync();
        await Entry(group).Collection(v => v.Students!).LoadAsync();
        return group;
    }

    public async Task<string> GetVisitsAsText()
    {
        return string.Join('\n', await GetAllVisitsInclude());
    }

    public async Task<IReadOnlyList<Student>> GetStudentsWithVisits()
    {
        var students = await Students
            .Include(stud => stud.Visits)
            .ToListAsync();
        return students;
    }

    public async Task<IReadOnlyList<Student>> GetStudentsWithVisitsWithSubject()
    {
        var students = await Students
            .Include(stud => stud.Visits!)
            .ThenInclude(visit => visit.Subject)
            .ToListAsync();
        return students;
        
        //Аналог с джойнами:
        var res = await (
            from stud in Students
            join vi in 
                from v in Visits
                    join subj in Subjects on v.SubjectId equals subj.Id
                    select new { visit = v, subj}
                on stud.Id equals vi.visit.StudentId
            select new {stud, visit = vi.visit, vi.subj})
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Visit>> GetStateFundedVisits()
    {
        return await Visits
            .Where(visit => visit.Student.IsStateFunded)
            .ToListAsync();
    }
    
    public async Task<IReadOnlyList<Student>> GetStudentsWithVisitsGreater0()
    {
        return await Students
            .Where(s => s.Visits.Count > 0)
            .ToListAsync();
    }
    public async Task<IReadOnlyList<Student>> GetStudentsWithVisitsInMonth()
    {
        var minDate = new DateOnly(2022, 9, 1);
        var maxDate = new DateOnly(2022, 9, 30);
        return await Students
            .Where(s => s.Visits.Count > 0 
                         && s.Visits.All(v => v.Date >= minDate && v.Date <= maxDate))
            .ToListAsync();
    }
    
    public async Task<IReadOnlyList<Student>> GetStudentsWithVisitsFromVisits()
    {
        var minDate = new DateOnly(2022, 9, 1);
        var maxDate = new DateOnly(2022, 9, 30);
        return await Visits
            .Where(it => it.Date >= minDate && it.Date <= maxDate)
            .Include(it => it.Student)
            .Select(it => it.Student!)
            .Distinct() //!!!!!!!!!!!!!!!!!!!!!!!
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Student>> FindStudentByNameEq(string name)
    {
        List<Student> students = await Students
            .Where(it => it.Name == name)
            .ToListAsync();
        return students;
    }
    
    public async Task<IReadOnlyList<Student>> FindStudentByName(string text)
    {
        // var id = new Guid("018fc039-98f7-41f1-8608-1be091e506b5");
        // var student = await Students
        //     .Include(s => s.Visits)
        //     .FirstAsync(s => s.Id == id);
        
        
        List<Student> students = await Students
            .Where(it => it.Name.Contains(text)) //Expression Tree
            .ToListAsync();

        var arr = new[] { 0, 1 }
            .Where(it =>
            {
                
                return it == 0;
            });
        //StringComparison.InvariantCultureIgnoreCase
        return students;
    }
    
        public async Task<IReadOnlyList<string>> FindStudentsEvenEduCost()
        {
            var names = await Students
                .Where(it => it.EducationCost % 2 == 0)
                .Select(it => it.Name)
                .Distinct()
                .ToListAsync();
            return names;
        }
}