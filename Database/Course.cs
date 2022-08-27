namespace EFCoreDemo.Database;

public class Course : BaseEntity {
    public string Name { get; set; } = string.Empty;

    //public int TeacherId { get;set; }
    public int? TeacherOfCourseId { get; set; }
    public Teacher Teacher { get; set; }
    public List<Student> Students { get; set; }
    public List<Grade> Grades { get; set; }

    public override string ToString()
    {
        return $"Course: {Name} - Students: {Students?.Count ?? 0} - Teacher: { Teacher?.Name ?? "Course Without Teacher Assigned" }";
    }
}