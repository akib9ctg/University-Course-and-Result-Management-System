using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class StudentResultManager
    {
        private StudentResultGateway _studentResultGateway;

        public StudentResultManager()
        {
            _studentResultGateway = new StudentResultGateway();
        }

        public string SaveAResult(StudentResultModel studentResultModel)
        {
            if (_studentResultGateway.SaveAResult(studentResultModel) == 1)
            {
                return "Result Successfully Saved";
            }
            else
            {
                return "Failed";
            }
        }

        public List<GradeLetterModel> GetAllGrades()
        {
            return _studentResultGateway.GetAllGrades();
        }

        public List<Course> GetAllTakenCourses(int studentId)
        {
            return _studentResultGateway.GetAllTakenCourses(studentId);
        }

        public List<StudentResultModel> GetAllResultForView(int studentId)
        {
            return _studentResultGateway.GetAllResultForView(studentId);
        }
    }

}
