namespace EFCoreDemo.Database;

public class Grade { 
    public int Id { get; set; }
    public decimal Value { get; set; } 
    public Student Student { set; get; }
    public Course Course { get; set; }
}