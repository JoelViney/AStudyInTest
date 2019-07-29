using System;
using System.Collections.Generic;

namespace AStudyInTest.Domain.Models
{
    public class DeliveryDay : ModelBase
    {
        #region Constructors...

        public DeliveryDay()
        {

        }

        #endregion

        /// <summary>The day that the delivery occurs.</summary>
        public DateTime Date { get; set; }

        public DateTime LastOrderDateTime { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
