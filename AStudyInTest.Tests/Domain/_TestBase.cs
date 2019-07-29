using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Services;
using AStudyInTest.Helpers;
using System.Threading.Tasks;

namespace AStudyInTest.Domain
{
    public abstract class TestBase
    {
        protected DatabaseContext GetInMemoryContext()
        {
            return DatabaseHelper.GetInMemoryContext();
        }

        protected ICurrentUser GetRetailerUser()
        {
            return new CurrentUserStub() { UserId = 0, Role = UserRole.Retailer, CustomerId = null };
        }

        protected ICurrentUser GetCustomerUser(int customerId)
        {
            return new CurrentUserStub() { UserId = 1, Role = UserRole.Customer, CustomerId = customerId };
        }

        protected async Task<DeliveryDay> AssureDeliveryDayExistsAsync(DeliveryDay item, DatabaseContext databaseContext)
        {
            var service = new DeliveryDayService(databaseContext, this.GetRetailerUser());
            await service.CreateAsync(item);
            return item;
        }

        protected async Task<Customer> AssureCustomerExistsAsync(Customer item, DatabaseContext databaseContext)
        {
            var service = new CustomerService(databaseContext, null);
            await service.CreateAsync(item);
            return item;
        }

        protected async Task<Product> AssureProductExistsAsync(Product item, DatabaseContext databaseContext)
        { 
            var service = new ProductService(databaseContext, this.GetRetailerUser());
            await service.CreateAsync(item);
            return item;
        }

        protected async Task<Order> AssureOrderExistsAsync(Order item, DatabaseContext databaseContext)
        {
            var service = new OrderService(databaseContext, this.GetCustomerUser(item.CustomerId));
            await service.CreateAsync(item);
            return item;
        }
    }
}
