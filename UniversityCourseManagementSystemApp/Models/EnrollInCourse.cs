using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystemApp.Models
{
    public class EnrollInCourse
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Student Reg. No.")]
        public int StudentId { get; set; }
        [Required]
        [Display(Name = "Select Course")]
        public int  CourseId { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public int GradeId { get; set; }
    }
}