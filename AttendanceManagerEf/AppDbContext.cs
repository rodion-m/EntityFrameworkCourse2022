using AttendanceManagerEf;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagerEf;

public class AppDbContext : DbContext
{
    //private const string ConnectionString 
        //= @"Data Source=C:\Users\rodio\RiderProjects\EntityFrameworkCourse2022\AttendanceManagerEf\att.db";
    private const string ConnectionString = @"Host=localhost;Database=my_db;Username=my_db;Password=coolpass";

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Visit> Visits => Set<Visit>();
}
