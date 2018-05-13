using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class DesignationManager
    {
        private DesignationGateway _designationGateway;

        public DesignationManager()
        {
            _designationGateway = new DesignationGateway();
        }

      

        public List<TeacherDesignation> GetAllDesignations()
        {
            return _designationGateway.GetAllDesignations();
        }
    }
}