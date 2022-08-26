namespace EFCoreDemo.Database;

public class Course : BaseEntity {
    public string Name { get; set; }

    //public int TeacherId { get;set; }
    public int TeacherOfCourseId { get; set; }
    public Teacher Teacher { get; set; }
    public List<Student> Students { get; set; }
}