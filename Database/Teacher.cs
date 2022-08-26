namespace EFCoreDemo.Database;

public class Teacher : BaseEntity { 
    public string Name { get; set; }
    public string LastName { get; set; }

    public List<Course> Courses { get; set; }
}