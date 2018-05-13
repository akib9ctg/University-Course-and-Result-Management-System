using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class ClassScheduleViewManager
    {
         private ClassScheduleViewGateway _classScheduleViewGateway;

         public ClassScheduleViewManager()
        {
            _classScheduleViewGateway = new ClassScheduleViewGateway();
        }

        public List<ClassScheduleView> GetAllClassScheduleViewsByDeptId(int deptId)
        {
            return _classScheduleViewGateway.GetAllClassScheduleViewsByDeptId(deptId);
        }
        public string  UnAllocateClassRoom()
        {
            int rowAffected = _classScheduleViewGateway.UnAllocateClassRoom();
            if (rowAffected > 0)
            {
                return "All Classes Successfully Unallocated";
            }
            else
            {
                return "Failed";
            }
        }
    }
}