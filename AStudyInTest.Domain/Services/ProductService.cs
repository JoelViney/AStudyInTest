using AStudyInTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{
    public class ProductService
    {
        private readonly DatabaseContext DatabaseContext;

        public ProductService(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        public async Task CreateAsync(Product product)
        {
            this.DatabaseContext.Add(product);
            await this.DatabaseContext.SaveChangesAsync();
        }

        public Task<Product> GetAsync(int id)
        {
            return this.DatabaseContext.Products.SingleAsync(x => x.Id == id);
        }
    }
}
