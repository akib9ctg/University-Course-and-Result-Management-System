using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class SemesterGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public SemesterGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        public List<Semester> GetAllSemesters()
        {
            string query = "SELECT *from SemesterTable order by SemesterCode";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<Semester> listOfItems = new List<Semester>();
            while (_sqlDataReader.Read())
            {
                Semester item = new Semester();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.SemesterCode = Convert.ToInt32( _sqlDataReader["SemesterCode"]);
                item.SemesterName = _sqlDataReader["SemesterName"].ToString();
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }
    }
}