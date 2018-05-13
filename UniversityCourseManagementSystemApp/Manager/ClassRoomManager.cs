using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class ClassRoomManager
    {
        private ClassRoomGateway _classRoomGateway;

        public ClassRoomManager()
        {
            _classRoomGateway=new ClassRoomGateway();
        }
        public string AllocateNewClassRoom(ClassRoomAllocation classRoomAllocation)
        {

            if (!_classRoomGateway.IsAllocationPossible(classRoomAllocation))
            {
                return "Cannot Possible";
            }
            else
            {
                
                int rowAffected = _classRoomGateway.AllocateNewClassRoom(classRoomAllocation);
                return rowAffected > 0 ? "Class Room Allocated Successfully" : "Failed";
            }
            
        }
        public List<ClassRoom> GetAllClassRooms()
        {
            return _classRoomGateway.GetAllClassRooms();
        }

    }
}