using System.ComponentModel.DataAnnotations.Schema;

namespace AStudyInTest.Domain.Models
{
    public class OrderLine : ModelBase
    {
        #region Constuctors..

        public OrderLine()
        {

        }

        #endregion

        public int OrderId { get; set; }

        public int ProductId { get; set; }
        
        public decimal Amount { get; set; }

        public int Quantity { get; set; }

        public bool Cancelled { get; set; }


        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        public decimal GetTotal()
        {
            return this.Amount * this.Quantity;
        }

        public bool IsNew()
        {
            return (this.Id == 0);
        }
    }
}
