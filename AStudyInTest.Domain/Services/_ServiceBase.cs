using AStudyInTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{
    public abstract class ServiceBase<T> 
        where T : ModelBase
    {
        private readonly DatabaseContext DatabaseContext;
        protected IQueryable<T> Table { get; private set; }

        public ServiceBase(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
            this.Table = this.DatabaseContext.Set<T>();
        }


        public async Task CreateAsync(T item)
        {
            this.DatabaseContext.Add(item);
            await this.DatabaseContext.SaveChangesAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await this.Table.SingleAsync(x => x.Id == id);
        }
    }
}
