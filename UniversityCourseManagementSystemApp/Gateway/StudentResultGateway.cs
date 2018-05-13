using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class StudentResultGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public StudentResultGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        public int SaveAResult(StudentResultModel studentResultModel)
        {
            string query = "UPDATE AssignStudentTable SET GradeId = '"+studentResultModel.GradeId+"' where StudentID ='"+studentResultModel.StudentId+"' AND CourseID='"+studentResultModel.CourseId+"' AND Status ='0'";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            int rowAffect = _command.ExecuteNonQuery();
            _connection.Close();
            return rowAffect;
        }
        public List<GradeLetterModel> GetAllGrades()
        {
            string query = "SELECT *from GradeTable where id!=10 order  by id";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<GradeLetterModel> listOfItems = new List<GradeLetterModel>();
            while (_sqlDataReader.Read())
            {
                GradeLetterModel item = new GradeLetterModel();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.GradeLetter = _sqlDataReader["Grade"].ToString();
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }
        public List<Course> GetAllTakenCourses(int studentId)
        {
            string query = "SELECT CourseTable.CourseCode,CourseTable.id from AssignStudentTable "+
                            "Inner JOIN CourseTable ON CourseTable.id=AssignStudentTable.CourseID " +
                            "where AssignStudentTable.StudentID='" + studentId + "' and AssignStudentTable.Status=0";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<Course> listOfItems = new List<Course>();
            while (_sqlDataReader.Read())
            {
                Course item = new Course();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.CourseCode = _sqlDataReader["CourseCode"].ToString();
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }

        public List<StudentResultModel> GetAllResultForView(int studentId)
        {
            string query = "SELECT CourseTable.CourseCode,CourseTable.CourseName,GradeTable.Grade from AssignStudentTable "+
                            "Inner JOIN CourseTable ON CourseTable.id=AssignStudentTable.CourseID "+
                            "Inner JOIN GradeTable On AssignStudentTable.GradeID=GradeTable.id "+
                            "where AssignStudentTable.StudentID='"+studentId+"' and AssignStudentTable.Status=0";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<StudentResultModel> listOfItems = new List<StudentResultModel>();
            while (_sqlDataReader.Read())
            {
                StudentResultModel item = new StudentResultModel();
                item.CourseCode = _sqlDataReader["CourseCode"].ToString();
                item.CourseName = _sqlDataReader["CourseName"].ToString();
                item.Grade = _sqlDataReader["Grade"].ToString();
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }
    }
}