using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using situationWebBTS.DataAccessLayer;
using situationWebBTS.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using situationWebBTS.ViewModels;

namespace situationWebBTS.Controllers
{
    public class StudentsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Students
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "firstName" ? "firstName_desc" : "firstName";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var students = from s in db.Students
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                
                students = students.Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "date":
                    students = students.OrderBy(s => s.SchoolEnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.SchoolEnrollmentDate);
                    break;
                case "firstName":
                    students = students.OrderBy(s => s.FirstMidName);
                    break;
                case "firstName_desc":
                    students = students.OrderByDescending(s => s.FirstMidName);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            var student = new Student();
            student.Enrollments = new List<Enrollment>();
            PopulateAssignedCourseData(student);
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstMidName")] Student student, string[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                student.Enrollments = new List<Enrollment>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    student.Enrollments.Add(new Enrollment()
                    {
                        Course = courseToAdd,
                        CourseID = courseToAdd.CourseID,
                        Student = student,
                        StudentID = student.ID
                    });
                }
            }
            student.SchoolEnrollmentDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseData(student);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students
                .Include(i => i.Enrollments)
                .Where(i => i.ID == id)
                .Single();
            PopulateAssignedCourseData(student);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.Students
               .Include(i => i.Enrollments)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
            {
                try
                {
                    UpdateStudentCourses(selectedCourses, studentToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedCourseData(studentToUpdate);
            return View(studentToUpdate);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Opération impossible. Veuillez réessayer ultérieurement.";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateAssignedCourseData(Student student)
        {
            var allCourses = db.Courses;
            var studentCourses = new HashSet<int>(student.Enrollments.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = studentCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }

        private void UpdateStudentCourses(string[] selectedCourses, Student studentToUpdate)
        {
            if (selectedCourses == null)
            {
                studentToUpdate.Enrollments = new List<Enrollment>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var studentCourses = new HashSet<int>
                (studentToUpdate.Enrollments.Select(c => c.CourseID));
            foreach (var course in db.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!studentCourses.Contains(course.CourseID))
                    {
                        studentToUpdate.Enrollments.Add(new Enrollment()
                        {
                            Student = studentToUpdate,
                            StudentID = studentToUpdate.ID,
                            Course = course,
                            CourseID = course.CourseID
                        });
                    }
                }
                else
                {
                    if (studentCourses.Contains(course.CourseID))
                    {
                        Enrollment courseToRemove = studentToUpdate.Enrollments.Where(e => e.Course.CourseID == course.CourseID).Single();
                        studentToUpdate.Enrollments.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
