using System;

namespace AStudyInTest.Helpers
{
    public static class DateHelper
    {
        public static DateTime Today
        {
            get { return DateTime.Now.Date; }
        }

        public static DateTime Tomorrow
        {
            get { return DateTime.Now.Date.AddDays(1); }
        }

        public static DateTime Yesterday
        {
            get { return DateTime.Now.Date.AddDays(-1); }
        }

        public static DateTime Next(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            var result = dateTime.AddDays(1);
            while (result.DayOfWeek != dayOfWeek)
                result = result.AddDays(1);

            return dateTime;
        }

        public static DateTime Previous(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            var result = dateTime.AddDays(-1);
            while (result.DayOfWeek != dayOfWeek)
                result = result.AddDays(-1);

            return result;
        }

        public static DateTime AtNoon(this DateTime dateTime)
        {
            var result = dateTime.Date.AddHours(12);
            return result;
        }

        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }

        public static DateTime StartOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }
    }
}
