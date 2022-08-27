select * from Students;
select * from Courses; 
select * from Teachers;

select  
	c.Id CourseId, c.Name, s.Id StudentId, s.Name, s.LastName, g.Value, g.CreatedDate
from Grades g 
inner join Students s on s.Id = g.StudentId
inner join Courses c on c.Id = g.CourseId;

select  
	c.Id CourseId, c.Name, s.Id StudentId, s.Name, s.LastName
from CourseStudent cs 
inner join Students s on s.Id = cs.StudentsId
inner join Courses c on c.Id = cs.CoursesId;