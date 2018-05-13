using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class StudentGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public StudentGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        public int InsertNewStudent(Student student)
        {
            string query = "INSERT INTO StudentTable Values('"+student.StudentName+"','"+student.StudentEmail+"','"+student.StudentContactNo+"','"+student.StudentAddDate+"','"+student.StudentAddress+"','"+student.StudentRegNo+"',"+student.DepartmentId+")";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            int rowAffect = _command.ExecuteNonQuery();
            _connection.Close();
            return rowAffect;
        }

        public string GetLastStudentRegNo(int deptId,string year)
        {
            string query = "SELECT TOP 1 * FROM StudentTable where DepartmentID='" + deptId + "' and StudentAddDate like '"+year+"%'  ORDER BY StudentRegNo DESC";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            string lastRegNo="";
            while (_sqlDataReader.Read())
            {
                lastRegNo = _sqlDataReader["StudentRegNo"].ToString();
            }
            _connection.Close();
            return lastRegNo;
        }
        public bool IsEmailExist(Student student)
        {
            string query = "SELECT * FROM StudentTable WHERE StudentEmail = '" + student.StudentEmail + "'";
            _command = new SqlCommand(query, _connection);
            _command.Connection.Open();
            SqlDataReader reader = _command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            _connection.Close();
            return isExist;
        }

        public List<Student> GetAllStudents()
        {
            string query = "select StudentTable.id,StudentTable.StudentRegNo, StudentTable.StudentName , StudentTable.StudentEmail,DepartmentTable.DeptCode " +
                            "from StudentTable Join DepartmentTable on StudentTable.DepartmentID=DepartmentTable.id  order by StudentRegNo";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<Student> allStudent = new List<Student>();
            while (_sqlDataReader.Read())
            {
                Student item = new Student();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.StudentRegNo = _sqlDataReader["StudentRegno"].ToString();
                item.StudentEmail = _sqlDataReader["StudentEmail"].ToString();
                item.DepartmentName = _sqlDataReader["DeptCode"].ToString();
                item.StudentName = _sqlDataReader["StudentName"].ToString();
                allStudent.Add(item);
            }
            _connection.Close();
            return allStudent;
        }
        public Student GetStudentById(int studentId)
        {
            string query = "select StudentTable.id,StudentTable.StudentRegNo, StudentTable.StudentName , StudentTable.StudentEmail,DepartmentTable.DeptCode " +
                            "from StudentTable Join DepartmentTable on StudentTable.DepartmentID=DepartmentTable.id where StudentTable.id='" + studentId + "' order by StudentRegNo";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            Student item=new Student();
            while (_sqlDataReader.Read())
            {
          
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.StudentRegNo = _sqlDataReader["StudentRegno"].ToString();
                item.StudentEmail = _sqlDataReader["StudentEmail"].ToString();
                item.DepartmentName = _sqlDataReader["DeptCode"].ToString();
                item.StudentName =_sqlDataReader["StudentName"].ToString();
                break;
            }
            
            _connection.Close();
            return item;
        }
        
    }
}