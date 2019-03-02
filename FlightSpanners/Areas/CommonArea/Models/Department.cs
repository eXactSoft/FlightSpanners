using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class Department
    {
        public Department()
        {
            Spanner = new HashSet<Spanner>();
        }

        public string DepartmentName { get; set; }
        public double DepartmentCostant { get; set; }

        public ICollection<Spanner> Spanner { get; set; }
    }
}
