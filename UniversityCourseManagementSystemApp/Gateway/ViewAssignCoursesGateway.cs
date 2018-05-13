using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.WebPages;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class ViewAssignCoursesGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public ViewAssignCoursesGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        
        public List<ViewAssignCourse> GetAllAssignCourses(int deptId)
        {
            string query = "SELECT CourseTable.CourseCode, CourseTable.CourseName,SemesterTable.SemesterName,TeacherTable.TeacherName"
                            +" FROM AssignTeacherTable"
                            +" Right JOIN CourseTable ON CourseTable.id=AssignTeacherTable.CourseID"
                            +" LEFT JOIN TeacherTable ON TeacherTable.Id=AssignTeacherTable.TeacherID"
                            +" Inner JOIN SemesterTable ON CourseTable.SemesterID=SemesterTable.Id"
                            + " where CourseTable.DepartmentID='" + deptId + "' and AssignTeacherTable.Status='Assign' order by CourseCode";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<ViewAssignCourse> listOfItems = new List<ViewAssignCourse>();
            while (_sqlDataReader.Read())
            {
                ViewAssignCourse viewAssignCourse = new ViewAssignCourse();

                viewAssignCourse.CourseCode = _sqlDataReader["CourseCode"].ToString();
                viewAssignCourse.CourseName = _sqlDataReader["CourseName"].ToString();
                viewAssignCourse.CourseSemester = _sqlDataReader["SemesterName"].ToString();
                viewAssignCourse.AssignTeacherName = _sqlDataReader["TeacherName"].ToString();
                if (viewAssignCourse.AssignTeacherName == "")
                {
                    viewAssignCourse.AssignTeacherName = "Not Assigned Yet";
                }
                listOfItems.Add(viewAssignCourse);

            }
            _connection.Close();
            return listOfItems;
        }
    }
}