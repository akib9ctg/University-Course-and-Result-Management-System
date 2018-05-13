using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystemApp.Gateway;
using UniversityCourseManagementSystemApp.Models;

namespace UniversityCourseManagementSystemApp.Manager
{
    public class SevenDayWeekManager
    {
        private SevenDayWeekGateway _sevenDayWeekGateway;

        public SevenDayWeekManager()
        {
            _sevenDayWeekGateway=new SevenDayWeekGateway();
        }
        public List<SevenDayWeek> GetAllDay()
        {
            return _sevenDayWeekGateway.GetAllDay();
        }
    }
}