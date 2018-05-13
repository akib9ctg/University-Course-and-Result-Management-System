using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystemApp.Manager;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Controllers
{
    public class CoursesController : Controller
    {
        //
        // GET: /Courses/
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Message"] = "";
            ViewBag.Departments = GetDepartmentForDropDown();
            ViewBag.Semesters = GetSemesterForDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult Index(Course course)
        {
            if (ModelState.IsValid)
            {
                CoursesManager coursesManager = new CoursesManager();
                course.CourseCode = course.CourseCode.ToUpper();
                course = CheckInput(course);
                string result = coursesManager.InsertNewCourse(course);
                ViewBag.Departments = GetDepartmentForDropDown();
                ViewBag.Semesters = GetSemesterForDropDown();
                ViewData["Message"] = result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewData["Message"] = "";
                ViewBag.Departments = GetDepartmentForDropDown();
                ViewBag.Semesters = GetSemesterForDropDown();
                return View();
            }
        }
        public Course CheckInput(Course course)
        {
            course.CourseCode = course.CourseCode.Trim();
            course.CourseName = course.CourseName.Trim();
            
            return course;
        }
        public IEnumerable<SelectListItem> GetDepartmentForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},                
            };
            List<Department> departments = new DepartmentManager().GetAllDepartments();
            foreach (Department department in departments)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = department.DeptName;
                selectListItem.Value = department.Id.ToString();                
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }
        public IEnumerable<SelectListItem> GetSemesterForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},                
            };
            List<Semester> semesters = new SemesterManager().GetAllSemesters();
            foreach (Semester semester in semesters)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = semester.SemesterName;
                selectListItem.Value = semester.Id.ToString();
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }
        public JsonResult GetAllUnAssignCoursesByDeptId(int departmentId)
        {
            var courses = new CoursesManager().GetAllUnAssignCoursesByDeptId(departmentId);
            
            return Json(courses);
        }
        public JsonResult GetAllCoursesByDeptId(int departmentId)
        {
            var courses = new CoursesManager().GetAllCoursesByDeptId(departmentId);

            return Json(courses);
        }
        public JsonResult GetAllAssignCourses(int departmentId)
        {
            var courses = new ViewAssignCoursesManager().GetAllAssignCourses(departmentId);
            return Json(courses);
        }
        public JsonResult GetCourseInformationByCourseId(int courseId)
        {
            var course = new CoursesManager().GetCourseInformationByCourseId(courseId);
            return Json(course);
        }

        [HttpGet]
        public ActionResult ViewCourseStatics()
        {
            ViewBag.Departments = GetDepartmentForDropDown();
            return View();
        }


        [HttpGet]
        public ActionResult UnassignCourse()
        {
            ViewData["Message"] = "";
            return View();            
        }
        [HttpPost]
        public ActionResult UnassignCourse(string unAssignCourse)
        {
            ViewData["Message"] = new CourseAssignManager().UnAssignCourses();
            return View();
        }


	}
}