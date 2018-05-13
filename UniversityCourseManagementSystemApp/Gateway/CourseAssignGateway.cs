using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class CourseAssignGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public CourseAssignGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        public int AssignNewCourse(CourseAssign courseAssign)
        {
            string query = "INSERT INTO AssignTeacherTable Values('Assign','" + courseAssign.TeacherId + "','"+courseAssign.CourseId+"')";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            int rowAffect = _command.ExecuteNonQuery();
            _connection.Close();
            return rowAffect;
        }
        public int UnAssignCourses()
        {
            string query = "UPDATE AssignTeacherTable SET Status = 'UnAssign' ";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            int rowAffect = _command.ExecuteNonQuery();
            _connection.Close();
            return rowAffect;
        }
        
    }

    
}