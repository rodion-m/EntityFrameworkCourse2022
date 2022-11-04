namespace Lesson.Dapper;


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
    
    public Guid Id { get; init; }

    public string Name { get; set; } = "";

    /// <summary> Бюджетное обучение </summary>
    public bool IsStateFunded { get; set; }

    /// <summary> Стоимость обучения </summary>
    public int EducationCost { get; set; }

    public DateOnly Birthday { get; set; }
    /// <summary> Очное обучение </summary>
    public bool IsFullTimeEducation { get; set; }

}
