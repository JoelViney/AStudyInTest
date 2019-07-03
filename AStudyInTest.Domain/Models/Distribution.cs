using System;
using System.Collections.Generic;

namespace AStudyInTest.Domain.Models
{
    public class Distribution : ModelBase
    {
        public DateTime Date { get; set; }

        public DateTime LastOrderDateTime { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
