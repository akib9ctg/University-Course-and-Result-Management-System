using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class TeacherGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public TeacherGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        public int InsertNewTeacher(Teacher teacher)
        {
            string query = "INSERT INTO TeacherTable Values('" +teacher.TeacherName + "','" + teacher.TeacherAddress + "','"+teacher.TeacherEmail+"','"+teacher.TeacherContactNo+"','"+teacher.TeacherCredit+"','"+teacher.TeacherDesignationId+"','"+teacher.TeacherDepartmentId+"')";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            int rowAffect = _command.ExecuteNonQuery();
            _connection.Close();
            return rowAffect;
        }
        public bool IsEmailExist(Teacher teacher)
        {
            string query = "SELECT * FROM TeacherTable WHERE TeacherEmail = '" + teacher.TeacherEmail + "'";
            _command = new SqlCommand(query, _connection);
            _command.Connection.Open();
            SqlDataReader reader = _command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            _connection.Close();
            return isExist;
        }
        public List<Teacher> GetAllTeacherByDepartment(int departmentId)
        {
            string query = "SELECT *from TeacherTable where TeacherDepartmentID='" + departmentId + "' order by TeacherName";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<Teacher> listOfItems = new List<Teacher>();
            while (_sqlDataReader.Read())
            {
                Teacher item = new Teacher();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.TeacherName = _sqlDataReader["TeacherName"].ToString();
                item.TeacherCredit = Convert.ToDouble(_sqlDataReader["TeacherCredit"]);
                listOfItems.Add(item);
            }
            _connection.Close();
            return listOfItems;
        }
        public Teacher GetTeacherInformationById(int teacherId)
        {
            string query = "SELECT *from TeacherTable where id=" + teacherId + "";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<Teacher> listOfItems = new List<Teacher>();
            Teacher item = new Teacher();
            while (_sqlDataReader.Read())
            {
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.TeacherName = _sqlDataReader["TeacherName"].ToString();
                item.TeacherCredit = Convert.ToDouble(_sqlDataReader["TeacherCredit"]);
                break;
            }
            _connection.Close();
            return item;
        }

        public double GetTotalTakenCourses(int teacherId)
        {
            string query = "SELECT CourseCredit from AssignTeacherTable as AT inner join CourseTable as C on AT.CourseID=C.id where TeacherID='" + teacherId + "' and Status='Assign'";
            _command = new SqlCommand(query, _connection);
            double totalCredit = 0.0;
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            while (_sqlDataReader.Read())
            {
                totalCredit += Convert.ToDouble(_sqlDataReader["CourseCredit"]);
            }
            _connection.Close();
            return totalCredit;
        }

        public double GetTeacherCreditLimit(int teacherId)
        {
            string query = "SELECT TeacherCredit from TeacherTable where id=" + teacherId + "";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            double creditLimit=0.0;
            while (_sqlDataReader.Read())
            {
                creditLimit = Convert.ToDouble(_sqlDataReader["TeacherCredit"]);
                break;
            }
            _connection.Close();
            return creditLimit;
        }
    }
}