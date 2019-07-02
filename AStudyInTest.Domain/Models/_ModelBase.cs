using System.ComponentModel.DataAnnotations.Schema;

namespace AStudyInTest.Domain.Models
{
    public abstract class ModelBase
    {
        [Column("ID")]
        public int Id { get; set; }
    }
}
