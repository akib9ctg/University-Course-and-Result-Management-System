using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class CoursesGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public CoursesGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        public int InsertNewCourse(Course course)
        {
            string query = "INSERT INTO CourseTable Values('" + course.CourseCode + "','" + course.CourseName + "','"+course.CourseCredit+"','"+course.CourseDescription+"','"+course.DepartmentId+"','"+course.SemesterId+"')";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            int rowAffect = _command.ExecuteNonQuery();
            _connection.Close();
            return rowAffect;
        }
        public bool IsCodeExist(Course course)
        {
            string query = "SELECT * FROM CourseTable WHERE CourseCode = '" + course.CourseCode + "'";
            
            _command = new SqlCommand(query, _connection);
            _command.Connection.Open();
            SqlDataReader reader = _command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            _connection.Close();
            return isExist;
        }
        public bool IsNameExist(Course course)
        {
            string query = "SELECT * FROM CourseTable WHERE CourseName = '" + course.CourseName + "'";

            _command = new SqlCommand(query, _connection);
            _command.Connection.Open();
            SqlDataReader reader = _command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            _connection.Close();
            return isExist;
        }
        public List<Course> GetAllCourses()
        {
            string query = "SELECT *from CourseTable order by CourseCode";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<Course> listOfItems = new List<Course>();
            while (_sqlDataReader.Read())
            {
                Course item = new Course();
                item.Id = (int) _sqlDataReader["id"];
                item.CourseCode = _sqlDataReader["CourseCode"].ToString();
                item.CourseName = _sqlDataReader["CourseName"].ToString();
                item.CourseCredit = (decimal) _sqlDataReader["CourseCredit"];
                item.CourseDescription = _sqlDataReader["CourseDescription"].ToString();
                item.DepartmentId = (int) _sqlDataReader["DepartmentID"];
                item.SemesterId = (int) _sqlDataReader["SemesterID"];
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }
        public List<Course> GetAllCoursesByDeptId(int deptId)
        {
            string query = "SELECT *from CourseTable where DepartmentID=" + deptId + " order by CourseCode";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<Course> listOfItems = new List<Course>();
            while (_sqlDataReader.Read())
            {
                Course item = new Course();
                item.Id = (int)_sqlDataReader["id"];
                item.CourseCode = _sqlDataReader["CourseCode"].ToString();
                item.CourseName = _sqlDataReader["CourseName"].ToString();
                item.CourseCredit = (decimal)_sqlDataReader["CourseCredit"];
                item.CourseDescription = _sqlDataReader["CourseDescription"].ToString();
                item.DepartmentId = (int)_sqlDataReader["DepartmentID"];
                item.SemesterId = (int)_sqlDataReader["SemesterID"];
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }
        public List<Course> GetAllUnAssignCoursesByDeptId(int deptId)
        {
            string query = "SELECT * from CourseTable where Id not in(select CourseId from AssignTeacherTable where Status='Assign') and DepartmentID='" + deptId + "' order by CourseCode";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<Course> listOfItems = new List<Course>();
            while (_sqlDataReader.Read())
            {
                Course item = new Course();
                item.Id = (int)_sqlDataReader["id"];
                item.CourseCode = _sqlDataReader["CourseCode"].ToString();
                item.CourseName = _sqlDataReader["CourseName"].ToString();
                item.CourseCredit = (decimal)_sqlDataReader["CourseCredit"];
                item.CourseDescription = _sqlDataReader["CourseDescription"].ToString();
                item.DepartmentId = (int)_sqlDataReader["DepartmentID"];
                item.SemesterId = (int)_sqlDataReader["SemesterID"];
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }
        public Course GetCourseInformationByCourseId(int courseId)
        {
            string query = "SELECT *from CourseTable where id=" + courseId + "";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            Course item = new Course();
            while (_sqlDataReader.Read())
            {
                
                item.Id = (int)_sqlDataReader["id"];
                item.CourseCode = _sqlDataReader["CourseCode"].ToString();
                item.CourseName = _sqlDataReader["CourseName"].ToString();
                item.CourseCredit = (decimal)_sqlDataReader["CourseCredit"];
                item.CourseDescription = _sqlDataReader["CourseDescription"].ToString();
                item.DepartmentId = (int)_sqlDataReader["DepartmentID"];
                item.SemesterId = (int)_sqlDataReader["SemesterID"];
                break;

            }
            _connection.Close();
            return item;
        }
        public double GetCourseCreditByCourseId(int courseId)
        {
            string query = "SELECT CourseCredit from CourseTable where id=" + courseId + "";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            double credit = 0;
            decimal cr = 0;
            while (_sqlDataReader.Read())
            {
                cr = (decimal)_sqlDataReader["CourseCredit"];
                credit = Convert.ToDouble(cr);
                break;

            }
            _connection.Close();
            return credit;
        }
        public List<Course> GetCourseByStudentId(string studentId)
        {
            string query = "SELECT CourseTable.id,CourseTable.CourseCode from CourseTable "+
                            "Inner JOIN DepartmentTable ON CourseTable.DepartmentID=DepartmentTable.Id "+
                            "Right JOIN StudentTable ON CourseTable.DepartmentID=StudentTable.DepartmentID where StudentTable.id='" + studentId + "' and CourseTable.id is not null";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<Course> listOfItems = new List<Course>();
            while (_sqlDataReader.Read())
            {
                Course item=new Course();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.CourseCode = _sqlDataReader["CourseCode"].ToString();
                listOfItems.Add(item);
            }
            _connection.Close();
            return listOfItems;
        }

    }
}