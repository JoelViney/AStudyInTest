using AStudyInTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{
    public abstract class ServiceBase<T> 
        where T : ModelBase
    {
        protected DatabaseContext DatabaseContext { get; }
        protected ICurrentUser CurrentUser { get; }
        protected IQueryable<T> Table { get; }

        public ServiceBase(DatabaseContext databaseContext, ICurrentUser currentUser)
        {
            this.DatabaseContext = databaseContext;
            this.CurrentUser = currentUser;
            this.Table = this.DatabaseContext.Set<T>();
        }

        /// <summary>Returns the item from the datastore. If the item doesn't exist it will throw a NotFoundException.</summary>
        public async Task<T> GetAsync(int id)
        {
            var item = await this.FindAsync(id);

            if (item == null)
            {
                throw new NotFoundException();
            }

            return item;
        }

        public async Task<List<T>> GetListAsync()
        {
            var results = await this.Table.ToListAsync();

            return results;
        }

        /// <summary>Returns the item if it exists in the datastore, otherwise it returns null.</summary>
        public async Task<T> FindAsync(int id)
        {
            var item = await this.Table.SingleOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public virtual async Task CreateAsync(T item)
        {
            this.DatabaseContext.Add(item);
            await this.DatabaseContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T item)
        {
            // Not sure what it needs to do here...
            await this.DatabaseContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T item)
        {
            // Not sure what it needs to do here...
            this.DatabaseContext.Remove(item);
            await this.DatabaseContext.SaveChangesAsync();
            item.Id = 0;
        }
    }
}
