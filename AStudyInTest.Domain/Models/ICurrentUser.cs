using System;
using System.Collections.Generic;
using System.Text;

namespace AStudyInTest.Domain.Models
{
    public enum UserRole
    {
        Customer = 0,
        Retailer = 1,
    }

    public interface ICurrentUser
    {
        int UserId { get; }

        /// <summary>
        /// If true the user
        /// </summary>
        UserRole Role { get; }

        int? CustomerId { get; }
    }
}
