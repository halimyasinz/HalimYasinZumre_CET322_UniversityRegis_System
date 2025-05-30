using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CET322Final.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        [Range(0, 100, ErrorMessage = "Not 0 ile 100 arasında olmalı")]
        public decimal? Grade { get; set; }
    }

}
// decimal? nullable demek öğrencinin notu henüz girilmemiş olabilir yani boş olması gerekebilir
