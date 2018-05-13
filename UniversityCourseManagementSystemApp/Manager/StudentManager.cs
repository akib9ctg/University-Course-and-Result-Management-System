using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class StudentManager
    {
        private StudentGateway _studentGateway;

        public StudentManager()
        {
            _studentGateway = new StudentGateway();
        }
        public string InsertNewStudent(Student student)
        {
            if (_studentGateway.IsEmailExist(student))
            {
                return "Same Email already Exist";
            }
            else
            {
                int rowAffected = _studentGateway.InsertNewStudent(student);
                return rowAffected > 0 ? "New Student Successfully Added" : "Failed";
            }
        }


        public List<Student> GetAllStudents()
        {
            return _studentGateway.GetAllStudents();
        }

        public Student GetStudentById(int studentId)
        {
            return _studentGateway.GetStudentById(studentId);
        }


        public string GetLastStudentRegNo(int deptId,string year)
        {
            return _studentGateway.GetLastStudentRegNo(deptId,year);
        }

        
    }
}