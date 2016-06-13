namespace situationWebBTS.Migrations
{
    using DataAccessLayer;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<situationWebBTS.DataAccessLayer.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstMidName = "Carson",   LastName = "Alexander",
                    SchoolEnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstMidName = "Meredith", LastName = "Alonso",
                    SchoolEnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Arturo",   LastName = "Anand",
                    SchoolEnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Gytis",    LastName = "Barzdukas",
                    SchoolEnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Yan",      LastName = "Li",
                    SchoolEnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Peggy",    LastName = "Justice",
                    SchoolEnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstMidName = "Laura",    LastName = "Norman",
                    SchoolEnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Nino",     LastName = "Olivetto",
                    SchoolEnrollmentDate = DateTime.Parse("2005-09-01") },
                new Student { FirstMidName = "Michel",   LastName = "Baudrier",
                    SchoolEnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstMidName = "Roch", LastName = "Juliette",
                    SchoolEnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Molier",   LastName = "Arnaud",
                    SchoolEnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Aleman",    LastName = "Poli",
                    SchoolEnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Trouvard",      LastName = "Jean",
                    SchoolEnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Fil",    LastName = "Michel",
                    SchoolEnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstMidName = "Rouchon",    LastName = "Gerard",
                    SchoolEnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Mer",     LastName = "Bertrand",
                    SchoolEnrollmentDate = DateTime.Parse("2005-09-01") },

            };


            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { FirstMidName = "Kim",     LastName = "Johnatan",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstMidName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstMidName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") },
                new Instructor { FirstMidName = "Michel", LastName = "Mich",
                    HireDate = DateTime.Parse("2015-04-11") }
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();


            var courses = new List<Course>
            {
                new Course {Title = "C#", Credits = 3,
                  Instructors = new List<Instructor>()
                },
                new Course {Title = "PHP", Credits = 3,
                  Instructors = new List<Instructor>()
                },
                new Course {Title = "Economie", Credits = 3,
                  Instructors = new List<Instructor>()
                },
                new Course {Title = "Mathématiques", Credits = 4,
                  Instructors = new List<Instructor>()
                },
                new Course {Title = "Java", Credits = 4,
                  Instructors = new List<Instructor>()
                },
                new Course {Title = "Modélisation", Credits = 3,
                  Instructors = new List<Instructor>()
                },
                new Course {Title = "Litérature", Credits = 4,
                  Instructors = new List<Instructor>()
                },
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "C#", "Kapoor");
            AddOrUpdateInstructor(context, "C#", "Harui");
            AddOrUpdateInstructor(context, "Economie", "Zheng");
            AddOrUpdateInstructor(context, "Mathématiques", "Zheng");
            AddOrUpdateInstructor(context, "Java", "Mich");
            AddOrUpdateInstructor(context, "Java", "Kapoor");
            AddOrUpdateInstructor(context, "Mathématiques", "Fakhouri");
            AddOrUpdateInstructor(context, "PHP", "Harui");
            AddOrUpdateInstructor(context, "Modélisation", "Johnatan");
            AddOrUpdateInstructor(context, "Litérature", "Johnatan");

            context.SaveChanges();


            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "C#" ).CourseID,
                    Grade = 18
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "PHP" ).CourseID,
                    Grade = 8
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Economie" ).CourseID,
                    Grade = 17
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Mathématiques" ).CourseID,
                    Grade = 14
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Java" ).CourseID,
                    Grade = 13
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Modélisation" ).CourseID,
                    Grade = 9
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Li").ID,
                    CourseID = courses.Single(c => c.Title == "Modélisation").CourseID,
                    Grade = 10
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Justice").ID,
                    CourseID = courses.Single(c => c.Title == "Litérature").CourseID,
                    Grade = 12
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Bertrand").ID,
                    CourseID = courses.Single(c => c.Title == "C#" ).CourseID,
                    Grade = 5
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Gerard").ID,
                    CourseID = courses.Single(c => c.Title == "PHP" ).CourseID,
                    Grade = 14
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Michel").ID,
                    CourseID = courses.Single(c => c.Title == "Economie" ).CourseID,
                    Grade = 13
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Bertrand").ID,
                    CourseID = courses.Single(c => c.Title == "Mathématiques" ).CourseID,
                    Grade = 19
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Gerard").ID,
                    CourseID = courses.Single(c => c.Title == "Java" ).CourseID,
                    Grade = 14
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Michel").ID,
                    CourseID = courses.Single(c => c.Title == "Modélisation" ).CourseID,
                    Grade = 11
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Bertrand").ID,
                    CourseID = courses.Single(c => c.Title == "Modélisation").CourseID,
                    Grade = 9
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Jean").ID,
                    CourseID = courses.Single(c => c.Title == "Litérature").CourseID,
                    Grade = 14
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Poli").ID,
                    CourseID = courses.Single(c => c.Title == "C#" ).CourseID,
                    Grade = 16
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Poli").ID,
                    CourseID = courses.Single(c => c.Title == "PHP" ).CourseID,
                    Grade = 12
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Arnaud").ID,
                    CourseID = courses.Single(c => c.Title == "Economie" ).CourseID,
                    Grade = 19
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Juliette").ID,
                    CourseID = courses.Single(c => c.Title == "Mathématiques" ).CourseID,
                    Grade = 11
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Jean").ID,
                    CourseID = courses.Single(c => c.Title == "Java" ).CourseID,
                    Grade = 7
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Baudrier").ID,
                    CourseID = courses.Single(c => c.Title == "Modélisation" ).CourseID,
                    Grade = 3
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Juliette").ID,
                    CourseID = courses.Single(c => c.Title == "Modélisation").CourseID,
                    Grade = 12
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Arnaud").ID,
                    CourseID = courses.Single(c => c.Title == "Litérature").CourseID,
                    Grade = 11
                 }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateInstructor(SchoolContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
        }

    }
}
