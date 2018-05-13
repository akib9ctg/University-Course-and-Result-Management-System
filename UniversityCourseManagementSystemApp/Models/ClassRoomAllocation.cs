using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystemApp.Models
{
    public class ClassRoomAllocation
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Time From")]
        public string TimeFrom { get; set; }
        [Required]
        [Display(Name = "Time To")]
        public string TimeTo { get; set; }
        public string Status { get; set; }
        [Required]
        [Display(Name = "Department ")]
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "Course ")]
        public int CourseId { get; set; }
        [Required]
        [Display(Name = "Room No. ")]
        public int RoomNoId { get; set; }
        [Required]
        [Display(Name = "Day ")]
        public int SevenDayWeekId { get; set; }

    }
}