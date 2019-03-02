using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class InActivePeriod
    {
        public int InActivePeriodId { get; set; }
        public string SpannerCode { get; set; }
        public DateTime InActiveFromDate { get; set; }
        public DateTime InActiveToDate { get; set; }

        public Spanner SpannerCodeNavigation { get; set; }
    }
}
