using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPos.BusinessLayer.Tools
{
    public static class TimeConverter
    {
        public static DateTime GetDateTimeNowMexico()
        {
            var myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, myTimeZone);
            return currentDateTime;
        }

        public static DateTime GetDateTimeMexico(DateTime fecha)
        {
            var myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(fecha, myTimeZone);
            return currentDateTime;
        }
    }
}
