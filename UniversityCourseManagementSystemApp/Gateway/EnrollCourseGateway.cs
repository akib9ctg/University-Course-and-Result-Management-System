using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class EnrollCourseGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public EnrollCourseGateway()
        {
            _connection = new SqlConnection(_conString);
        }

        public int EnrollNewCourseToStudent(EnrollInCourse enrollInCourse)
        {
            string query = "INSERT INTO AssignStudentTable Values('0','" + enrollInCourse.Date + "','" + enrollInCourse.StudentId + "','"+enrollInCourse.CourseId+"','10')";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            int rowAffect = _command.ExecuteNonQuery();
            _connection.Close();
            return rowAffect;
        }
        public bool IsCourseIdAndStudentIdExist(EnrollInCourse enrollInCourse)
        {
            string query = "SELECT * FROM AssignStudentTable WHERE CourseID = '" + enrollInCourse.CourseId + "' And StudentID='" + enrollInCourse.StudentId + "' AND Status='0'";

            _command = new SqlCommand(query, _connection);
            _command.Connection.Open();
            SqlDataReader reader = _command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            _connection.Close();
            return isExist;
        }
        public int DisEnrollCourses()
        {
            string query = "UPDATE AssignStudentTable SET Status = '1' ";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            int rowAffect = _command.ExecuteNonQuery();
            _connection.Close();
            return rowAffect;
        }

    }
}