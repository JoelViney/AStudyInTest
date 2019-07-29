using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AStudyInTest.Domain.Models
{
    public class Customer : ModelBase
    {
        #region Constructors...

        public Customer()
        {

        }

        #endregion

        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
