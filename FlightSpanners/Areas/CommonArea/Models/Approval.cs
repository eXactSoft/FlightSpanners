using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class Approval
    {
        public Approval()
        {
            FlightRecord = new HashSet<FlightRecord>();
        }

        public int ApprovalId { get; set; }
        public string SpannerCode { get; set; }
        public int ApprovalDetailId { get; set; }
        public int AircraftTypeId { get; set; }

        public AircraftType AircraftType { get; set; }
        public ApprovalDetail ApprovalDetail { get; set; }
        public Spanner SpannerCodeNavigation { get; set; }
        public ICollection<FlightRecord> FlightRecord { get; set; }
    }
}
