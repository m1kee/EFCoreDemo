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

    // delete student
    ctx.Remove<Student>(michael);
    ctx.Students.Remove(jhon);

    ctx.SaveChanges();
}
