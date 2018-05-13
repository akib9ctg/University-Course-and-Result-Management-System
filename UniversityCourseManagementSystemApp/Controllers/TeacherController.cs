using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Manager;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Controllers
{
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Message"] = "";
            ViewBag.Departments = GetDepartmentForDropDown();
            ViewBag.Designation = GetDesignationForDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult Index(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                TeacherManager teacherManager = new TeacherManager();
                teacher = CheckInput(teacher);
                string result = teacherManager.InsertNewTeacher(teacher);
                ViewBag.Departments = GetDepartmentForDropDown();
                ViewBag.Designation = GetDesignationForDropDown();
                ViewData["Message"] = result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewData["Message"] = "";
                ViewBag.Departments = GetDepartmentForDropDown();
                ViewBag.Designation = GetDesignationForDropDown();
                return View();
            }
        }
        public Teacher CheckInput(Teacher teacher)
        {
            teacher.TeacherName = teacher.TeacherName.Trim();
            teacher.TeacherAddress = teacher.TeacherAddress.Trim();
            teacher.TeacherEmail = teacher.TeacherEmail.Trim();
            teacher.TeacherContactNo = teacher.TeacherContactNo.Trim();
            return teacher;
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
        public IEnumerable<SelectListItem> GetDesignationForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},                
            };
            List<TeacherDesignation> teacherDesignations = new DesignationManager().GetAllDesignations();
            foreach (TeacherDesignation teacherDesignation in teacherDesignations)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = teacherDesignation.DesignationName;
                selectListItem.Value = teacherDesignation.Id.ToString();
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }
        public IEnumerable<SelectListItem> GetCoursesForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},                
            };
            List<Course> courses = new CoursesManager().GetAllCourses();
            foreach (Course course in courses)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = course.CourseName;
                selectListItem.Value = course.Id.ToString();
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }
        [HttpGet]
        public ActionResult CourseAssign()
        {
            ViewData["Message"] = "";
            ViewBag.Departments = GetDepartmentForDropDown();
            ViewBag.Courses = GetCoursesForDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult CourseAssign(CourseAssign courseAssign)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Departments = GetDepartmentForDropDown();
                ViewBag.Designation = GetDesignationForDropDown();
                string result = new CourseAssignManager().InsertNewCourse(courseAssign);
                ViewData["Message"] = result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewData["Message"] = "";
                ViewBag.Departments = GetDepartmentForDropDown();
                ViewBag.Courses = GetCoursesForDropDown();
                return View();
            }

        }
        public JsonResult GetTeacherByDepartmentId(int departmentId)
        {
            var teacher = new TeacherManager().GetAllTeachersByDepartment(departmentId);
            var teacherList = teacher.ToList();
            return Json(teacherList);
        }
        public JsonResult GetTeacherInformationById(int teacherId)
        {
            var teacher = new TeacherManager().GetTeacherInformationById(teacherId);
            teacher.CourseTaken = new TeacherGateway().GetTotalTakenCourses(teacherId);
            return Json(teacher);
        }
    }
}