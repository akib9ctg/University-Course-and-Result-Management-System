using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class DepartmentGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public DepartmentGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        public int InsertNewDepartment(Department department)
        {
            string query = "INSERT INTO DepartmentTable Values('" + department.DeptCode + "','" + department.DeptName + "')";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            int rowAffect = _command.ExecuteNonQuery();
            _connection.Close();
            return rowAffect;
        }
        public bool IsCodeExist(Department department)
        {
            string query = "SELECT * FROM DepartmentTable WHERE DeptCode = '" + department.DeptCode + "'";
            
            _command = new SqlCommand(query, _connection);
            _command.Connection.Open();
            SqlDataReader reader = _command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            _connection.Close();
            return isExist;
        }
        public bool IsNameExist(Department department)
        {
            string query = "SELECT * FROM DepartmentTable WHERE DeptName = '" + department.DeptName + "'";

            _command = new SqlCommand(query, _connection);
            _command.Connection.Open();
            SqlDataReader reader = _command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            _connection.Close();
            return isExist;
        }
        public List<Department> GetAllDepartments()
        {
            string query = "SELECT *from DepartmentTable order by DeptCode";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<Department> listOfItems = new List<Department>();
            while (_sqlDataReader.Read())
            {
                Department item = new Department();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.DeptName = _sqlDataReader["DeptName"].ToString();
                item.DeptCode = _sqlDataReader["DeptCode"].ToString();
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }
        public string GetDepartmentName(int deptId)
        {
            string query = "SELECT DeptCode from DepartmentTable where id='"+deptId+"'";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            string deptCode = "";
            while (_sqlDataReader.Read())
            {
                deptCode = _sqlDataReader["DeptCode"].ToString();
                break;

            }
            _connection.Close();
            return deptCode;
        }
    }
}