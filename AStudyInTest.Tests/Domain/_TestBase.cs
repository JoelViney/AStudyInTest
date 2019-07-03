using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AStudyInTest.Domain
{
    public abstract class TestBase
    {
        protected async Task<Distribution> AssureDistributionExistsAsync(Distribution item, DatabaseContext databaseContext)
        {
            var service = new DistributionService(databaseContext);
            await service.CreateAsync(item);
            return item;
        }

        protected async Task<Customer> AssureCustomerExistsAsync(Customer item, DatabaseContext databaseContext)
        {
            var service = new CustomerService(databaseContext);
            await service.CreateAsync(item);
            return item;
        }

        protected async Task<Product> AssureProductExistsAsync(Product item, DatabaseContext databaseContext)
        { 
            var service = new ProductService(databaseContext);
            await service.CreateAsync(item);
            return item;
        }

        protected async Task<Order> AssureOrderExistsAsync(Order item, DatabaseContext databaseContext)
        {
            var service = new OrderService(databaseContext);
            await service.CreateAsync(item);
            return item;
        }
    }
}
