using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class Qualification
    {
        public Qualification()
        {
            Spanner = new HashSet<Spanner>();
        }

        public int QualificationId { get; set; }
        public string QualificationName { get; set; }
        public string QualificationDegree { get; set; }
        public string QualificationMajor { get; set; }
        public double QualificationCostant { get; set; }

        public ICollection<Spanner> Spanner { get; set; }
    }
}
