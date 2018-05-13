using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystemApp.Models
{
    public class CourseAssign
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Department ")]
        public int DeptId { get; set; }
        [Required]
        [Display(Name = "Teacher ")]
        public int TeacherId { get; set; }
        [Required]
        [Display(Name = "Course Code ")]
        public int CourseId { get; set; }
    }
}