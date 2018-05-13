using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystemApp.Manager;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Message"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Department department)
        {
            if (ModelState.IsValid)
            {
                DepartmentManager departmentManager=new DepartmentManager();
                department.DeptCode = department.DeptCode.ToUpper();
                department = CheckInput(department);
                string result=departmentManager.InsertNewDepartment(department);
                ViewData["Message"] = result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewData["Message"] = "";
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult ViewDepartment()
        {
            return View();
        }

        public Department CheckInput(Department department)
        {
            department.DeptCode = department.DeptCode.Trim();
            department.DeptName = department.DeptName.Trim();
            return department;
        }
        public JsonResult GetAllDepartment()
        {
            var departments = new DepartmentManager().GetAllDepartments();
            return Json(departments.ToList());
        }
	}
}