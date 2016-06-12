using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace situationWebBTS.Models
{
    public class Course
    {
        [Display(Name = "Identifiant")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Titre")]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }
        
        
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
