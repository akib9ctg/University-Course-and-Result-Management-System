using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Gateway
{
    public class ClassRoomGateway
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["UniversityProjectManagementSystem"].ConnectionString;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        private SqlCommand _command;

        public ClassRoomGateway()
        {
            _connection = new SqlConnection(_conString);
        }
        public int AllocateNewClassRoom(ClassRoomAllocation classRoomAllocation)
        {
            string query = "INSERT INTO ClassRoomAllocationTable Values('" + classRoomAllocation.TimeFrom + "','" + classRoomAllocation.TimeTo + "','0','" + classRoomAllocation.DepartmentId + "','" + classRoomAllocation.CourseId + "','" + classRoomAllocation.RoomNoId + "','" + classRoomAllocation.SevenDayWeekId + "')";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            int rowAffect = _command.ExecuteNonQuery();
            _connection.Close();
            return rowAffect;
        }

        public bool IsAllocationPossible(ClassRoomAllocation classRoomAllocation)
        {
            string query = "SELECT *from ClassRoomAllocationTable where SevenDayWeekID ='" + classRoomAllocation.SevenDayWeekId + "' and RoomNoID ='" + classRoomAllocation.RoomNoId + "'  and Status=0 order by TimeFrom";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<ClassRoomAllocation> listOfItems = new List<ClassRoomAllocation>();
            while (_sqlDataReader.Read())
            {
                ClassRoomAllocation item = new ClassRoomAllocation();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.TimeFrom = _sqlDataReader["TimeFrom"].ToString();
                item.TimeTo = _sqlDataReader["TimeTo"].ToString();
                listOfItems.Add(item);
            }
            _connection.Close();
            int[] timeArray = new int[3000];
            foreach (ClassRoomAllocation c in listOfItems)
            {
                for (int i = Convert.ToInt32(c.TimeFrom); i <= Convert.ToInt32(c.TimeTo); i++)
                {
                    timeArray[i] = 1;
                }
                timeArray[Convert.ToInt32(c.TimeTo)] = 0;
            }
            for (int i = Convert.ToInt32(classRoomAllocation.TimeFrom); i <= Convert.ToInt32(classRoomAllocation.TimeTo); i++)
            {
                if (timeArray[i] == 1)
                {
                    return false;
                }
            }
            return true;
        }
        public List<ClassRoom> GetAllClassRooms()
        {
            string query = "SELECT *from ClassRoomTable order by RoomNo";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            _sqlDataReader = _command.ExecuteReader();
            List<ClassRoom> listOfItems = new List<ClassRoom>();
            while (_sqlDataReader.Read())
            {
                ClassRoom item = new ClassRoom();
                item.Id = Convert.ToInt32(_sqlDataReader["id"]);
                item.RoomNo = _sqlDataReader["RoomNo"].ToString();
                listOfItems.Add(item);

            }
            _connection.Close();
            return listOfItems;
        }
    }
}