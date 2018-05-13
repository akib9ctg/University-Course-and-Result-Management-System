using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using UniversityCourseManagementSystemApp.Manager;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/



        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Departments = new TeacherController().GetDepartmentForDropDown();
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Student student)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Departments = new TeacherController().GetDepartmentForDropDown();

                string deptCode = new DepartmentManager().GetDepartmentCode(student.DepartmentId);
                string temp = student.StudentAddDate.Substring(0, 4);
                string year = temp;
                string lastRegNo = new StudentManager().GetLastStudentRegNo(student.DepartmentId, year);
                if (lastRegNo == "")
                {
                    student.StudentRegNo = deptCode + "-" + year + "-" + "001";
                    ViewBag.Message = new StudentManager().InsertNewStudent(student) + "\n And " +
                                          student.StudentName + "'s RegNo is " + student.StudentRegNo;
                }
                else
                {
                    string tempString = "";
                    student.StudentRegNo = deptCode + "-" + year + "-";
                    for (int i = lastRegNo.Length - 1; i >= lastRegNo.Length - 1 - 2; i--)
                    {
                        tempString += lastRegNo[i];
                    }
                    tempString = Reverse(tempString);
                    int tempRegNo = Convert.ToInt32(tempString);
                    tempRegNo ++;
                    tempString = tempRegNo.ToString();
                    if (tempString.Length == 1)
                    {
                        student.StudentRegNo +="00"+ tempString;
                    }
                    else if (tempString.Length == 2)
                    {
                        student.StudentRegNo += "0" + tempString;
                    }
                    else
                    {
                        student.StudentRegNo += tempString;
                    }

                    ViewBag.Message = new StudentManager().InsertNewStudent(student);
                    if (ViewBag.Message.Equals("New Student Successfully Added"))
                    {
                        ViewBag.Message += " And " + student.StudentName + "'s RegNo is " + student.StudentRegNo;
                    }
                }
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewBag.Message = "";
                return View();
            }
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }


        //Student Enroll in Course
        [HttpGet]
        public ActionResult StudentEnrollInCourse()
        {
            ViewData["Message"] = "";
            ViewBag.StudentsRegNo = GetAllStudentForDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult StudentEnrollInCourse(EnrollInCourse enrollCourse)
        {
            if (ModelState.IsValid)
            {
                ViewBag.StudentsRegNo = GetAllStudentForDropDown();
                string result = new EnrollCourseManager().AssignNewCourseToStudent(enrollCourse);
                ViewData["Message"] = result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewData["Message"]= "";
                ViewBag.StudentsRegNo = GetAllStudentForDropDown();
                return View();
            }
        }

        //Save Student Result
        [HttpGet]
        public ActionResult StudentResult()
        {
            ViewData["Message"] = "";
            ViewBag.StudentsRegNo = GetAllStudentForDropDown();
            ViewBag.Grades = GetAllGradesForDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult StudentResult(StudentResultModel studentResultModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.StudentsRegNo = GetAllStudentForDropDown();
                ViewBag.Grades = GetAllGradesForDropDown();
                string result = new StudentResultManager().SaveAResult(studentResultModel);
                ViewData["Message"] = result;
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewData["Message"] = "";
                ViewBag.StudentsRegNo = GetAllStudentForDropDown();
                ViewBag.Grades = GetAllGradesForDropDown();
                return View();
            }
            
        }


        public IEnumerable<SelectListItem> GetAllGradesForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},                
            };
            List<GradeLetterModel> grades = new StudentResultManager().GetAllGrades();
            foreach (GradeLetterModel resultModel in grades)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = resultModel.GradeLetter;
                selectListItem.Value = resultModel.Id.ToString();
                selectListItems.Add(selectListItem);
            }
            return selectListItems;
        }

        //Student Result View
        public ActionResult ViewStudentResult()
        {
            ViewBag.StudentsRegNo = GetAllStudentForDropDown();
            return View();
        }

        public IEnumerable<SelectListItem> GetAllStudentForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},                
            };
            List<Student> students = new StudentManager().GetAllStudents();
            foreach (Student student in students)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = student.StudentRegNo;
                selectListItem.Value = student.Id.ToString();
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }


        
        public JsonResult GetStudentByStudentId(int studentId)
        {
            var students = new StudentManager().GetStudentById(studentId);
            return Json(students);
        }

        public JsonResult GetCourseByStudentId(string studentId)
        {
            var courses = new CoursesManager().GetCourseByStudentId(studentId);
            return Json(courses);
        }

        public JsonResult GetTakenCourseByStudentId(int studentId)
        {
            var courses = new StudentResultManager().GetAllTakenCourses(studentId);
            return Json(courses);
        }
        public JsonResult GetAllCoursesByDeptId(int departmentId)
        {
            var courses = new CoursesManager().GetAllCoursesByDeptId(departmentId);
            return Json(courses);
        }

        public JsonResult GetAllResultByStudentId(int studentId)
        {
            var courses = new StudentResultManager().GetAllResultForView(studentId);
            return Json(courses);
        }
	}
}