namespace Projekt.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsInClass { get; set; } = true;

        public int SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }
    }
}
