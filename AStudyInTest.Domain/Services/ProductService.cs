using AStudyInTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{
    public class ProductService : ServiceBase<Product>
    {
        public ProductService(DatabaseContext databaseContext, ICurrentUser currentUser) : base(databaseContext, currentUser)
        {

        }

        public async Task<List<Product>> GetActiveListAsync()
        {
            var results = await this.DatabaseContext.Products.Where(x => x.Active).ToListAsync();

            return results;
        }

        public override async Task CreateAsync(Product item)
        {
            if (this.CurrentUser.Role != UserRole.Retailer)
            {
                throw new UnauthorizedException();
            }

            await base.CreateAsync(item);
        }

        public override async Task UpdateAsync(Product item)
        {
            if (this.CurrentUser.Role != UserRole.Retailer)
            {
                throw new UnauthorizedException();
            }

            await base.UpdateAsync(item);
        }

        public override async Task DeleteAsync(Product item)
        {
            if (this.CurrentUser.Role != UserRole.Retailer)
            {
                throw new UnauthorizedException();
            }

            // A product can only be deleted if it hasn't been used.
            var existsInOrder = await this.DatabaseContext.OrderLines.AnyAsync(x => x.ProductId == item.Id);

            if (existsInOrder)
            {
                throw new MethodNotAllowedException($"Unable to delete the product {item.Name} as it has been used in an existing order.");
            }

            await base.DeleteAsync(item);
        }
    }
}
