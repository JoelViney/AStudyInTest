using AStudyInTest.Domain.Models;

namespace AStudyInTest.Domain.Services
{
    public class DeliveryDayService : ServiceBase<DeliveryDay>
    {
        public DeliveryDayService(DatabaseContext databaseContext, ICurrentUser currentUser) : base(databaseContext, currentUser)
        {

        }
    }
}
