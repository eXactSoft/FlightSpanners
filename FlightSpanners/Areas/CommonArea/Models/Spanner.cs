using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class Spanner
    {
        public Spanner()
        {
            Approval = new HashSet<Approval>();
            InActivePeriod = new HashSet<InActivePeriod>();
        }

        public string SpannerCode { get; set; }
        public string SpannerLicenseNo { get; set; }
        public string GroupName { get; set; }
        public string SpannerFName { get; set; }
        public string SpannerM1Name { get; set; }
        public string SpannerM2Name { get; set; }
        public string SpannerLName { get; set; }
        public string SpannerGender { get; set; }
        public string SpannerEmail { get; set; }
        public string SpannerPassword { get; set; }
        public string SpannerMobile1 { get; set; }
        public string SpannerMobile2 { get; set; }
        public DateTime? SpannerBirthday { get; set; }
        public DateTime? SpannerHireDate { get; set; }
        public bool IsSpannerViewGroupData { get; set; }
        public bool IsSpannerHasCar { get; set; }
        public string DepartmentName { get; set; }
        public int QualificationId { get; set; }

        public Department DepartmentNameNavigation { get; set; }
        public GroupOfSpanners GroupNameNavigation { get; set; }
        public Qualification Qualification { get; set; }
        public ICollection<Approval> Approval { get; set; }
        public ICollection<InActivePeriod> InActivePeriod { get; set; }
    }
}
