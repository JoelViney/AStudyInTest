using AStudyInTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{
    public class DistributionService
    {
        private readonly DatabaseContext DatabaseContext;

        public DistributionService(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        public async Task CreateAsync(Distribution distribution)
        {
            this.DatabaseContext.Add(distribution);
            await this.DatabaseContext.SaveChangesAsync();
        }

        public Task<Distribution> GetAsync(int id)
        {
            return this.DatabaseContext.Distributions.SingleAsync(x => x.Id == id);
        }
    }
}
