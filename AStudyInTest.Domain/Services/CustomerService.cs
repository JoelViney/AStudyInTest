﻿using AStudyInTest.Domain.Models;

namespace AStudyInTest.Domain.Services
{
    public class CustomerService : ServiceBase<Customer>
    {
        public CustomerService(DatabaseContext databaseContext, ICurrentUser currentUser) : base(databaseContext, currentUser)
        {

        }
    }
}
