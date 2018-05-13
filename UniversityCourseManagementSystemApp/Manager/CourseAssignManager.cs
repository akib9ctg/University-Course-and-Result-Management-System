using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class CourseAssignManager
    {
        private CourseAssignGateway _courseAssignGateway;

        public CourseAssignManager()
        {
            _courseAssignGateway = new CourseAssignGateway();
        }

        public string InsertNewCourse(CourseAssign courseAssign)
        {
            int rowAffected = _courseAssignGateway.AssignNewCourse(courseAssign);
            return rowAffected > 0 ? "Course assign to Teacher Successfully" : "Failed";
        }

        public string UnAssignCourses()
        {

            int rowAffected = _courseAssignGateway.UnAssignCourses();
            int rowAffected2 = new EnrollCourseGateway().DisEnrollCourses();
            if (rowAffected2 > 0 && rowAffected > 0)
            {
                return "Unassign Courses Successfully";
            }
            else
            {
                return "Failed";
            }
            
        }
    }

}