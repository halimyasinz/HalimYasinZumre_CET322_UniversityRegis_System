using System.ComponentModel.DataAnnotations;

namespace CET322Final.Models.ViewModels
{
    public class EditEnrollmentViewModel
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        [Range(0, 100, ErrorMessage = "Not 0 ile 100 arasında olmalı")]
        public decimal? Grade { get; set; }

        public string? StudentName { get; set; }
        public string? CourseTitle { get; set; }
    }
}
