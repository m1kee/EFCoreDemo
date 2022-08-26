using EFCoreDemo.Database;

Console.WriteLine("EFCore Demo");

using ( var ctx = new DemoDbContext()) {
    // create students
    var michael = new Student() {
        Name = "Michael",
        LastName = "Núñez"
    };

    var jhon = new Student() {
        Name = "Jhon",
        LastName = "Doe"
    };

    // add students to context
    ctx.Add<Student>(michael);
    ctx.Students.Add(jhon);

    // save context to ins students to the db
    ctx.SaveChanges();

    // update student 
    jhon.LastName = "Poe";
    ctx.SaveChanges();

    // create course with student
    var math = new Course() {
        Name = "Math",
        Students = new List<Student> () {
            michael
        }
    };

    // save course 
    ctx.Add<Course>(math);
    ctx.SaveChanges();

    // add course to student
    if (jhon.Courses == null)
        jhon.Courses = new List<Course>();
        
    jhon.Courses.Add(math);
    ctx.SaveChanges();

    // delete student
    ctx.Remove<Student>(michael);
    ctx.Students.Remove(jhon);

    // delete course 
    ctx.Remove<Course>(math);

    ctx.SaveChanges();
}
