using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystemApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name cannot be a number")]
        [Display(Name = "Name")]
        public string StudentName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string StudentEmail { get; set; }
        [Required]
        [RegularExpression(@"^\(?[+. ]?([0-9]{2})\)?[-. ]?([0-9]{11})$", ErrorMessage = "Invalid Phone number(e.+8801XXXXXXXXX)")]
        [Display(Name = "Contact No")]
        public string StudentContactNo { get; set; }
        [Required]
        [Display(Name = "Date")]
        public string StudentAddDate { get; set; }
        [Display(Name = "Address")]
        public string StudentAddress { get; set; }

        public string StudentRegNo { get; set; }
        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public String  DepartmentName { get; set; }

    }
}