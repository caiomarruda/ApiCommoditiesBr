using System;
using TimeZoneConverter;

namespace ApiCommoditiesBr.Helper
{
    public static class DateTimeHelper
    {
        public static void ConvertDateToLocalDateTime(DateTime date, out DateTime newDate)
        {
            try
            {
                TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
                newDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, TimeZoneInfo.Local.Id, tzi.Id);
            }
            catch (TimeZoneNotFoundException)
            {
                throw;
            }
        }
    }
}
