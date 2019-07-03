using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AStudyInTest.Domain.Models
{
    public class OrderLine : ModelBase
    {
        [Column("OrderID"), Required, ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Column("ProductID"), Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public decimal Amount { get; set; }
        public int Quantity { get; set; }

        public bool Cancelled { get; set; }

        public decimal Total
        {
            get
            {
                return this.Amount * this.Quantity;
            }
        }

        public bool IsNew()
        {
            return (this.Id == 0);
        }
    }
}
