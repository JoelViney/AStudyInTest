using System;
using System.Collections.Generic;
using System.Text;

namespace AStudyInTest.Domain.Services
{
    public interface ITimeService
    {
        DateTime GetDateTimeNow();
    }

    public class TimeService : ITimeService
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
