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
        
        public int Quantity { get; set; }
    }
}
