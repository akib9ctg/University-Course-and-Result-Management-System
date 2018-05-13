using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class CoursesManager
    {
        private CoursesGateway _coursesGateway;

        public CoursesManager()
        {
            _coursesGateway = new CoursesGateway();
        }

        public string InsertNewCourse(Course course)
        {
            if (_coursesGateway.IsCodeExist(course))
            {
                return "Same Code already Exist";
            }
            else if (_coursesGateway.IsNameExist(course))
            {
                return "Same Name already Exist";
            }
            else
            {
                int rowAffected = _coursesGateway.InsertNewCourse(course);
                return rowAffected > 0 ? "New Course Successfully Added" : "Failed";
            }
        }

        public List<Course> GetAllCourses()
        {
            return _coursesGateway.GetAllCourses();
        }

        public List<Course> GetAllUnAssignCoursesByDeptId(int deptId)
        {
            return _coursesGateway.GetAllUnAssignCoursesByDeptId(deptId);
        }
        public List<Course> GetAllCoursesByDeptId(int deptId)
        {
            return _coursesGateway.GetAllCoursesByDeptId(deptId);
        }

        public Course GetCourseInformationByCourseId(int courseId)
        {
            return _coursesGateway.GetCourseInformationByCourseId(courseId);
        }

        public double GetCourseCreditByCourseId(int courseId)
        {
            return _coursesGateway.GetCourseCreditByCourseId(courseId);
        }

        public List<Course> GetCourseByStudentId(String studentId)
        {
            return _coursesGateway.GetCourseByStudentId(studentId);
        }
    }
}