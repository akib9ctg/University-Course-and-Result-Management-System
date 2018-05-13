using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystemApp.Models
{
    public class TeacherAssign
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }

    }
}