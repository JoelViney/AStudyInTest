﻿using System;

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

        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }
    }
}