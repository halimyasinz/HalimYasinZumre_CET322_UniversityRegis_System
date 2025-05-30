using System.ComponentModel.DataAnnotations;

namespace CET322Final.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}

// Öğrenci Modeli İsim zorunlu alan 