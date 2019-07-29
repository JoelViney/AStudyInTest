using AStudyInTest.Domain.Models;
using AStudyInTest.Domain.Models.Reports;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AStudyInTest.Domain.Services
{

    public class ReportService
    {
        private readonly DatabaseContext _databaseContext;

        public ReportService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<PickingListReport> GetPickingListAsync(int deliveryDayId)
        {
            var deliveryDay = await _databaseContext.DeliveryDays
                .Include(x => x.Orders)
                .ThenInclude(x => x.Lines)
                .ThenInclude(x => x.Product)
                .FirstAsync(x => x.Id == deliveryDayId);

            var report = new PickingListReport
            {
                OrderCount = deliveryDay.Orders.Count(x => x.Status != OrderStatus.Cancelled)
            };

            foreach (var order in deliveryDay.Orders)
            {
                if (order.Status != OrderStatus.Cancelled)
                {
                    foreach (var orderLine in order.Lines)
                    {
                        if (!orderLine.Cancelled)
                        {
                            var reportLine = report.Lines.FirstOrDefault(x => x.ProductId == orderLine.ProductId);

                            if (reportLine == null)
                            {
                                reportLine = new PickingListReportLine() { ProductId = orderLine.ProductId, ProductName = orderLine.Product.Name };
                                report.Lines.Add(reportLine);
                            }

                            reportLine.Quantity += orderLine.Quantity;
                        }
                    }
                }
            }

            return report;
        }
    }
}
