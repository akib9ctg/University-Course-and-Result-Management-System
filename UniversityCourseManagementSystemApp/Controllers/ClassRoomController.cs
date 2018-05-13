using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using UniversityCourseManagementSystemApp.Manager;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Controllers
{
    public class ClassRoomController : Controller
    {
        //
        // GET: /ClassRoom/
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Departments = new TeacherController().GetDepartmentForDropDown();
            ViewBag.ClassRooms = GetClassRoomForDropDown();
            ViewBag.Days = GetDayForDropDown();
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(ClassRoomAllocation classRoomAllocation)
        {
            ViewBag.Departments = new TeacherController().GetDepartmentForDropDown();
            ViewBag.ClassRooms = GetClassRoomForDropDown();
            ViewBag.Days = GetDayForDropDown();
            classRoomAllocation.TimeFrom = classRoomAllocation.TimeFrom.Remove(2, 1);
            classRoomAllocation.TimeTo = classRoomAllocation.TimeTo.Remove(2, 1);
            ViewBag.Message = new ClassRoomManager().AllocateNewClassRoom(classRoomAllocation);
            ModelState.Clear();
            return View();
        }
        public IEnumerable<SelectListItem> GetClassRoomForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},                
            };
            List<ClassRoom> classRooms = new ClassRoomManager().GetAllClassRooms();
            foreach (ClassRoom classRoom in classRooms)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = classRoom.RoomNo;
                selectListItem.Value = classRoom.Id.ToString();
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }
        public IEnumerable<SelectListItem> GetDayForDropDown()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "---Select---",Value = ""},                
            };
            List<SevenDayWeek> sevenDayWeeks = new SevenDayWeekManager().GetAllDay();
            foreach (SevenDayWeek sevenDayWeek in sevenDayWeeks)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = sevenDayWeek.Day;
                selectListItem.Value = sevenDayWeek.Id.ToString();
                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }

        [HttpGet]
        public ActionResult ViewClassRoutine()
        {
            ViewBag.Departments = new TeacherController().GetDepartmentForDropDown();
            return View();
        }
        public JsonResult GetAllClassSchedule(int departmentId)
        {
            var getAllClassScheduleViews = GetAllClassScheduleViews(departmentId);
            return Json(getAllClassScheduleViews);
        }

        public List<ClassScheduleView> GetAllClassScheduleViews(int departmentId )
        {
            List<ClassScheduleView> list = new ClassScheduleViewManager().GetAllClassScheduleViewsByDeptId(departmentId);
            List<ClassScheduleView>myList=new List<ClassScheduleView>();
            
            for (var i = 0; i < list.Count;)
            {
                ClassScheduleView classView = list[i];
                ClassScheduleView temp=new ClassScheduleView();
                temp.CourseCode = classView.CourseCode;
                temp.CourseName = classView.CourseName;
                temp.ScheduleInfo=("R. No : " +classView.RoomName + ", " + classView.DayShortName + ", " + classView.TimeFrom + " - " + classView.TimeTo)+"</br>";
                int ck = 0;
                i++;
                
                for (var j = i; j < list.Count; j++)
                {
                    ck = 1;
                    ClassScheduleView classView2 = list[j - 1];
                    ClassScheduleView classView3 = list[j];

                    if (classView2.CourseCode == classView3.CourseCode)
                    {
                        i++;
                        temp.ScheduleInfo += ("R. No : " + classView3.RoomName + ", " + classView3.DayShortName + ", " +
                                                         classView3.TimeFrom + " - " + classView3.TimeTo + "</br>");
                        
                        //myList.Add(temp);
                    }
                    else
                    {
                        if (classView.RoomName == "" )
                        {
                            temp.ScheduleInfo = "Not Assigned Yet";
                        }
                        myList.Add(temp);
                        break;
                    }
                }
                if (ck == 0)
                {
                    if (classView.RoomName == "")
                    {
                        temp.ScheduleInfo = "Not Assigned Yet";
                    }
                    myList.Add(temp);
                }
               
            }
            return myList;
        }
        [HttpGet]
        public ActionResult UnallocatedClassRooms()
        {
            ViewData["Message"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult UnallocatedClassRooms(string unallocatedClassRoom)
        {
            ViewData["Message"] = new ClassScheduleViewManager().UnAllocateClassRoom();
            return View();
        }
	}
}