using AStudyInTest.Domain.Models;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{
    public class ProductService : ServiceBase<Product>
    {
        public ProductService(DatabaseContext databaseContext, ICurrentUser currentUser) : base(databaseContext, currentUser)
        {
        }

        public override Task CreateAsync(Product item)
        {
            if (this.CurrentUser.Role != UserRole.Retailer)
            {
                throw new UnauthorizedException();
            }

            return base.CreateAsync(item);
        }
    }
}
