using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class SevenDayWeekGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public SevenDayWeekGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        public List<SevenDayWeek> GetAllDay()
        {
            string query = "SELECT *from SevenDayWeekTable order by id";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<SevenDayWeek> listOfItems = new List<SevenDayWeek>();
            while (_sqlDataReader.Read())
            {
                SevenDayWeek item = new SevenDayWeek();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.Day = _sqlDataReader["Day"].ToString();
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }
    }
}