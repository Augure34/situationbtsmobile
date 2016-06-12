using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using situationWebBTS.DataAccessLayer;
using situationWebBTS.ViewModels;
using System.Web.Helpers;

namespace situationWebBTS.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Statistics()
        {
            IQueryable<EnrollmentDateGroup> enrollmentData = from student in db.Students
                                                   group student by student.SchoolEnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       StudentCount = dateGroup.Count(),
                                                       GradeAverage = dateGroup.Select(s => s.Enrollments.Average(e => e.Grade)).Average()                                                                                                                            
        };
            IQueryable<CoursesData> courseData = from course in db.Courses
                                                    select new CoursesData()
                                                    {
                                                        course = course,
                                                        EnrolledStudentsCount = course.Enrollments.Count()
                                                    };

            StatisticsData stats = new StatisticsData() { EnrollmentData = enrollmentData.ToList(), TopCoursesData = courseData.OrderByDescending(c => c.EnrolledStudentsCount).ToList() };

            return View(stats);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}