namespace EFCoreDemo.Database;

public class Teacher { 
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    public List<Course> Courses { get; set; }
}