using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace CET322Final.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ders adı zorunludur.")]
        public string Title { get; set; }

        [Range(1, 10, ErrorMessage = "Kredi 1 ile 10 arasında olmalıdır.")]
        public int Credits { get; set; }

        [BindNever] 
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
// int boş olamaz o yüzden credit required gerekmiyor, enrollment bir öğrencinin bir derse kayıtlı olduğunu belirtir ve burdaki bindnever sistem tarafından değerin gelmesi gerektiği için var