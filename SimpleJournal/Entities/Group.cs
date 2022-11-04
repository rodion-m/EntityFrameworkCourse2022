using System;
using System.Collections.Generic;

namespace SimpleJournal.Entities;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public bool OnlyFullTimeEducation { get; set; }
    public List<Student>? Students { get; set; }

    public override bool Equals(object? obj)
    {
        Guid? id = (obj as Group)?.Id;
        return Id == id;
    }

    public override string ToString()
    {
        return Name;
    }


    //TODO: показать на уроке про скрытые поля
    public void AddStudent(Student student)
    {
        if (OnlyFullTimeEducation)
        {
            if (!student.IsFullTimeEducation)
            {
                throw new InvalidOperationException("В эту группу можно добавлять только очников");
            }
        }
        Students.Add(student);
    }
}