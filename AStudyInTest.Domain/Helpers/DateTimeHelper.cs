using System;
using System.Collections.Generic;
using System.Text;

namespace AStudyInTest.Domain.Helpers
{
    internal static class DateTimeHelper
    {
        internal static string GetDaySuffix(DateTime dateTime)
        {
            switch (dateTime.Day)
            {
                case 1:
                case 21:
                case 31:
                    return $"{dateTime.Day.ToString()}st";
                case 2:
                case 22:
                    return $"{dateTime.Day.ToString()}nd";
                case 3:
                case 23:
                    return $"{dateTime.Day.ToString()}rd";
                default:
                    return $"{dateTime.Day.ToString()}th";
            }
        }
    }
}
