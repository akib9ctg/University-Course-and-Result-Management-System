using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class SemesterManager
    {
         private SemesterGateway _semesterGateway;

        public SemesterManager()
        {
            _semesterGateway = new SemesterGateway();
        }

      

        public List<Semester> GetAllSemesters()
        {
            return _semesterGateway.GetAllSemesters();
        }
    }
}