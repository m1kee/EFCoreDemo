namespace EFCoreDemo.Database;

public class Course : BaseEntity {
    public string Name { get; set; } = string.Empty;

    //public int TeacherId { get;set; }
    public int? TeacherOfCourseId { get; set; }
    public Teacher? Teacher { get; set; } = null;
    public List<Student>? Students { get; set; } = null;
}