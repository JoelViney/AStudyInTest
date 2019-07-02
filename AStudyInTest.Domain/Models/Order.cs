using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AStudyInTest.Domain.Models
{
    public class Order : ModelBase
    {
        public Order()
        {
            this.Lines = new List<OrderLine>();
        }

        [Column("CustomerID"), ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Column("Distribution"), Required, ForeignKey("Distribution")]
        public int DistributionId { get; set; }
        public Distribution Distribution { get; set; }

        public virtual List<OrderLine> Lines { get; set; }
    }
}
