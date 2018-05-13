using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class DepartmentManager
    {
        private DepartmentGateway _departmentGateway;

        public DepartmentManager()
        {
            _departmentGateway = new DepartmentGateway();
        }

        public string InsertNewDepartment(Department department)
        {
            if (_departmentGateway.IsCodeExist(department))
            {
                return "Same Code already Exist";
            }
            else if (_departmentGateway.IsNameExist(department))
            {
                return "Same Name already Exist";
            }
            else
            {
                int rowAffected = _departmentGateway.InsertNewDepartment(department);
                return rowAffected > 0 ? "New Department Successfully Added" : "Failed";
            }
        }

        public List<Department> GetAllDepartments()
        {
            return _departmentGateway.GetAllDepartments();
        }

        public string GetDepartmentCode(int deptId)
        {
            return _departmentGateway.GetDepartmentName(deptId);
        }
    }
}