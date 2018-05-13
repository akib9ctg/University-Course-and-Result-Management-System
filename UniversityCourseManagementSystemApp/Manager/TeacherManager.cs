using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class TeacherManager
    {
         private TeacherGateway _teacherGateway;

        public TeacherManager()
        {
            _teacherGateway = new TeacherGateway();
        }

        public string InsertNewTeacher(Teacher teacher)
        {
            if (_teacherGateway.IsEmailExist(teacher))
            {
                return "Same Email already Exist";
            }
            else
            {
                int rowAffected = _teacherGateway.InsertNewTeacher(teacher);
                return rowAffected > 0 ? "New Teacher Successfully Added" : "Failed";
            }
        }

        public List<Teacher> GetAllTeachersByDepartment(int departmentId)
        {
            return _teacherGateway.GetAllTeacherByDepartment(departmentId);
        }

        public Teacher GetTeacherInformationById(int teacherId)
        {
            return _teacherGateway.GetTeacherInformationById(teacherId);
        }

        public double GetTotalTakenCourses(int teacherId)
        {
            return _teacherGateway.GetTotalTakenCourses(teacherId);
        }

        public double GetTeacherCreditLimit(int teacherId)
        {
            return _teacherGateway.GetTeacherCreditLimit(teacherId);
        }
    }
}