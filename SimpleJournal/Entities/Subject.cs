using System;

namespace SimpleJournal.Entities
{
    public class Subject
    {
        protected Subject() { }
        public Subject(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = "";
    }
}
