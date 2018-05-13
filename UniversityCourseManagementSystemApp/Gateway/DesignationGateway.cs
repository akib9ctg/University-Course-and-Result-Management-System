using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class DesignationGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public DesignationGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        public List<TeacherDesignation> GetAllDesignations()
        {
            string query = "SELECT *from TeacherDesignationTable order by DesignationName";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<TeacherDesignation> listOfItems = new List<TeacherDesignation>();
            while (_sqlDataReader.Read())
            {
                TeacherDesignation item = new TeacherDesignation();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.DesignationId = Convert.ToInt32(_sqlDataReader["DesignationID"]);
                item.DesignationName = _sqlDataReader["DesignationName"].ToString();
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }
    }
}