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

        public bool Cancelled { get; set; }

        public decimal Total
        {
            get
            {
                var total = 0.00M;

                foreach (var line in this.Lines)
                {
                    total += line.Total;
                }

                return total;
            }
        }
    }
}
