using AStudyInTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{
    public class DistributionService : ServiceBase<Distribution>
    {
        public DistributionService(DatabaseContext databaseContext) : base(databaseContext)
        {

        }
    }
}
