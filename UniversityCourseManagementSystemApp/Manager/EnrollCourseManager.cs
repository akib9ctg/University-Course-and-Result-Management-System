using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    
    public class EnrollCourseManager
    {
        private EnrollCourseGateway _enrollCoursesGateway;

        public EnrollCourseManager()
        {
            _enrollCoursesGateway = new EnrollCourseGateway();
        }

        public string AssignNewCourseToStudent(EnrollInCourse enrollInCourse)
        {
            if (_enrollCoursesGateway.IsCourseIdAndStudentIdExist(enrollInCourse))
            {
                return "This Course already Taken by this Student";
            }
            else
            {
                int rowAffected = _enrollCoursesGateway.EnrollNewCourseToStudent(enrollInCourse);
                return rowAffected > 0 ? "This Course Successfully Enrolled to the Student" : "Failed";
            }
        }
    }
}