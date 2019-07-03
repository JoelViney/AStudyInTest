using AStudyInTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace AStudyInTest.Domain.Services
{

    public class ReportService
    {
        private DatabaseContext _databaseContext;

        public ReportService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<PickingListReport> GetPickingListAsync(int distributionId)
        {
            var distribution = await _databaseContext.Distributions
                .Include(x => x.Orders)
                .ThenInclude(x => x.Lines)
                .ThenInclude(x => x.Product)
                .FirstAsync(x => x.Id == distributionId);

            var report = new PickingListReport
            {
                OrderCount = distribution.Orders.Count(x => !x.Cancelled)
            };

            foreach (var order in distribution.Orders)
            {
                if (!order.Cancelled)
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
