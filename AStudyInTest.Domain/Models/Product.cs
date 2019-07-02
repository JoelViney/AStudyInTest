using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AStudyInTest.Domain.Models
{
    public class Product : ModelBase
    {
        public Product() 
        {
            this.Price = 0.00M; 
            this.Active = true;
        }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }
    }
}
