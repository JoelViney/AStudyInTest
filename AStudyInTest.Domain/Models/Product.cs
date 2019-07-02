using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AStudyInTest.Domain.Models
{
    public class Product
    {
        [Column("ID")]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
