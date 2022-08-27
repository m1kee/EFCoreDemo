namespace EFCoreDemo.Database;

public class Teacher : BaseEntity { 
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{Name} {LastName}";

    public List<Course> Courses { get; set; }
}