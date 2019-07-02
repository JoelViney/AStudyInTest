using AStudyInTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{
    public class ProductService : ServiceBase<Product>
    {
        public ProductService(DatabaseContext databaseContext) : base(databaseContext)
        {

        }
    }
}
