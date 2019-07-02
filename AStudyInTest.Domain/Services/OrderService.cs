using AStudyInTest.Domain.Models;

namespace AStudyInTest.Domain.Services
{
    public class OrderService : ServiceBase<Order>
    {
        public OrderService(DatabaseContext databaseContext) : base(databaseContext)
        {

        }
    }
}
