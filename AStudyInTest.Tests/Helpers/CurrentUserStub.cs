using AStudyInTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStudyInTest.Helpers
{
    internal class CurrentUserStub : ICurrentUser
    {
        public int UserId { get; set; }

        public UserRole Role { get; set; }

        public int? CustomerId { get; set; }
    }
}
