// using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemo.Database;

public class Student : BaseEntity {
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

     //[NotMapped]
    public string FullName => $"{Name} {LastName}";

    public List<Course> Courses { get; set; }
    public List<Grade> Grades { get; set; }
}