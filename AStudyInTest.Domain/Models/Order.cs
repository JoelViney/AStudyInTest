using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AStudyInTest.Domain.Models
{
    public enum OrderStatus
    {
        Draft,
        Ordered,
        Cancelled,
    }

    public class Order : ModelBase
    {
        #region Constuctors..

        public Order()
        {
            this.Lines = new List<OrderLine>();
        }

        #endregion


        public int CustomerId { get; set; }

        public int DeliveryDayId { get; set; }

        public OrderStatus Status { get; set; }

        public virtual List<OrderLine> Lines { get; set; }


        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("DeliveryDayId")]
        public DeliveryDay DeliveryDay { get; set; }


        public decimal GetTotal()
        {
            var total = 0.00M;

            foreach (var line in this.Lines)
            {
                total += line.GetTotal();
            }

            return total;
        }

    }
}
