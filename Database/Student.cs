// using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemo.Database;

public class Student : BaseEntity {
    public string Name { get; set; }
    public string LastName { get; set; }

     //[NotMapped]
    public string FullName => $"{Name} {LastName}";

    public List<Course>? Courses { get; set; }
}