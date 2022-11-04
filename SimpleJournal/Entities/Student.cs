using System;
using System.Collections.Generic;
using SimpleJournal.ValueObjects;

namespace SimpleJournal.Entities;


public class Student
{
    public Student()
    {
    }

    public Student(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    // public static void Examples()
    // {
    //     var petya = new Student(..);
    //     var vasya = new Student(..);
    //     petya.Phone = vasya.Email;
    //     vasya.Email = petya.Phone;
    //     petya.Birthday = vasya.Email;
    // }
    
    public Guid Id { get; init; }

    public string Name { get; set; } = "";

    /// <summary> Бюджетное обучение </summary>
    public bool IsStateFunded { get; set; }

    /// <summary> Стоимость обучения </summary>
    public int EducationCost { get; set; }

    public DateOnly Birthday { get; set; }

    public Phone? Phone { get; set; }
    //public Email? Email { get; set; }

    /// <summary> Очное обучение </summary>
    public bool IsFullTimeEducation { get; set; }

    /// <summary> Посещения студента </summary>
    public List<Visit>? Visits { get; set; }

    public Group? Group { get; set; }

    public int? VisitsCount => Visits?.Count;
}
