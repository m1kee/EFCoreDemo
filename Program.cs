using EFCoreDemo.Database;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("EFCore Demo");

// declare variables to share among all methods
Student michael;
Student jhon;
Course math;
Course history;

using (var ctx = new DemoDbContext())
{
    TruncateTables(ctx);
    CreateAndUpdateStudents(ctx);
    CreateCourses(ctx);
    CreateGrades(ctx);
    QueryingData(ctx);
}

void TruncateTables(DemoDbContext ctx)
{
    // delete previous execution values
    if (ctx.Grades.Any())
        ctx.Grades.RemoveRange(ctx.Grades);
    if (ctx.Students.Any())
        ctx.Students.RemoveRange(ctx.Students);
    if (ctx.Courses.Any())
        ctx.Courses.RemoveRange(ctx.Courses);
    if (ctx.Teachers.Any())
        ctx.Teachers.RemoveRange(ctx.Teachers);
}

void CreateAndUpdateStudents(DemoDbContext ctx)
{
    //create students
    michael = new Student()
    {
        Name = "Michael",
        LastName = "Núñez"
    };

    jhon = new Student()
    {
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
}

void CreateCourses(DemoDbContext ctx)
{
    // create course with student
    math = new Course()
    {
        Name = "Math",
        Students = new List<Student>() {
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

    // create a course with a untracked teacher
    history = new Course()
    {
        Name = "History",
        // note that teacher is not tracked at this moment
        Teacher = new Teacher()
        {
            Name = "Hoshi",
            LastName = "Katou"
        },
        // but students are already created
        Students = new List<Student>() {
            michael,
            jhon
        }
    };

    // start tracking both course and teacher
    ctx.Courses.Add(history);
    // save both course and teacher linked
    ctx.SaveChanges();
}

void CreateGrades(DemoDbContext ctx)
{
    // add some grades to our students
    // first check if michael has grades
    if (michael.Grades == null)
        michael.Grades = new List<Grade>();

    // then add grade to michael and history course
    michael.Grades.Add(new Grade()
    {
        Course = history,
        Value = 6.5M
    });

    // add grade from course to a student
    if (history.Grades == null)
        history.Grades = new List<Grade>();

    history.Grades.Add(new Grade()
    {
        Value = 7,
        Student = jhon
    });

    // add grades for all students in a course
    math.Students.ForEach(student =>
    {
        ctx.Add<Grade>(new Grade()
        {
            Course = math,
            Student = student,
            Value = 1.0M
        });
    });

    ctx.SaveChanges();

    var mathStudents = math.Students.ToList();
    foreach (var student in mathStudents)
    {
        ctx.Add<Grade>(new Grade()
        {
            Course = math,
            Student = student,
            Value = 2.0M
        });
    }

    ctx.SaveChanges();

    ctx.Grades.AddRange(math.Students.Select(s => new Grade()
    {
        Course = math,
        Student = s,
        Value = 3.5M
    }));

    // save the grade
    ctx.SaveChanges();
}

void QueryingData(DemoDbContext ctx)
{
    // now we have data to create queries
    // get a course without relationships
    var mathCourse = ctx.Courses
        .Where(x => x.Name.Equals("Math"))
        .FirstOrDefault();
    if (mathCourse != null)
    {
        Console.WriteLine(mathCourse.ToString());
    }

    var historyCourse = ctx.Courses
        .Include(x => x.Teacher)
        .Where(x => x.Name.Equals("History"))
        .FirstOrDefault();
    if (historyCourse != null)
    {
        Console.WriteLine(historyCourse.ToString());
    }
}