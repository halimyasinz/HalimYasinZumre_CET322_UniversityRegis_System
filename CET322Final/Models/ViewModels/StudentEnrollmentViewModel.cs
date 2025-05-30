using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CET322Final.Models.ViewModels
{
    public class StudentEnrollmentViewModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public List<Student> Students { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
    }
}
