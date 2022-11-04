using System.Collections;
using Dapper;
using Lesson.Dapper.Example;
using Microsoft.Data.Sqlite;

namespace Lesson.Dapper;

public class StudentsRepository : IDisposable
{
    private readonly SqliteConnection _connection = new("Data Source=hello.db");
    
    public async Task<IReadOnlyList<Student>> GetStudents()
    {
        var sql = "select * from Students";
        var students = await _connection.QueryAsync<Student>(sql);
        return students.ToList();
    }
    
    public async Task<Student> GetStudentById(Guid id)
    {
        var sql = @"select * from Students where Id=@id";
        Student? student = await _connection.QuerySingleAsync<Student>(
            sql, new { id });
        return student;
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

}