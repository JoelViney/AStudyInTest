using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AStudyInTest.Domain.Models
{
    public class Distribution : ModelBase
    {
        public DateTime Date { get; set; }

        public DateTime LastOrderDateTime { get; set; }
    }
}
