using AStudyInTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
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


        public virtual async Task CreateAsync(T item)
        {
            this.DatabaseContext.Add(item);
            await this.DatabaseContext.SaveChangesAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            var item = await this.Table.SingleOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                throw new NotFoundException();
            }

            return item;
        }
    }
}
