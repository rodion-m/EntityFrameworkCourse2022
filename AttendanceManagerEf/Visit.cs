using System;

namespace AttendanceManagerEf;

public class Visit
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
}