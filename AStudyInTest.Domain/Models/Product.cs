using System.ComponentModel.DataAnnotations;

namespace AStudyInTest.Domain.Models
{
    public class Product : ModelBase
    {
        #region Constructors...

        public Product()
        {
            this.Price = 0.00M;
            this.Active = true;
        }

        #endregion

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }
    }
}
