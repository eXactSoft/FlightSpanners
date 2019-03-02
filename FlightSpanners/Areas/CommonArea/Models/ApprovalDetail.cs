using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class ApprovalDetail
    {
        public ApprovalDetail()
        {
            Approval = new HashSet<Approval>();
        }

        public int ApprovalDetailId { get; set; }
        public string ApprovalRating { get; set; }
        public string ApprovalCategory { get; set; }
        public double ApprovalCostant { get; set; }

        public ICollection<Approval> Approval { get; set; }
    }
}
