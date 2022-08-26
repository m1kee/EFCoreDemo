namespace EFCoreDemo.Database;

public class Grade : BaseEntity { 
    public decimal Value { get; set; } 
    public Student Student { set; get; }
    public Course Course { get; set; }
}