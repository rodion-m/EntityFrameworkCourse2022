using System;

namespace AttendanceManagerEf
{
    public class Student
    {
        public Student() {}
        public Student(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string? Name { get; set; }
        
        /// <summary>
        /// Бюджетное обучение
        /// </summary>
        public bool IsStateFunded { get; set; }
    }
}
