using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class ViewAssignCoursesManager
    {
        private ViewAssignCoursesGateway _viewAssignCoursesGateway;
        public ViewAssignCoursesManager()
        {
            _viewAssignCoursesGateway = new ViewAssignCoursesGateway();
        }
        public List<ViewAssignCourse> GetAllAssignCourses(int deptId)
        {
            return new ViewAssignCoursesGateway().GetAllAssignCourses(deptId);
        }
    }
}