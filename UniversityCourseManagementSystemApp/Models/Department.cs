using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace UniversityCourseManagementSystemApp.Models
{
    public class Department
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "Code  ")]
        [StringLength(7,MinimumLength = 2,ErrorMessage = "Length must be between 2 to 7")]
        public string DeptCode { get; set; }



        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Department Name cannot be a number")]
        [Display(Name = "Name  ")]
        public string DeptName { get; set; }
    }
}