using System.Collections.Generic;

namespace AStudyInTest.Domain.Models.Reports
{
    public class PickingListReport
    {
        public PickingListReport()
        {
            this.Lines = new List<PickingListReportLine>();
        }

        public List<PickingListReportLine> Lines { get; set; }

        public int OrderCount { get; set; }
    }

    public class PickingListReportLine
    {
        public int ProductId { get; internal set; }
        public string ProductName { get; internal set; }

        public int Quantity { get; internal set; }
    }
}
