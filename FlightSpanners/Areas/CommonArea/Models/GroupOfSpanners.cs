using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class GroupOfSpanners
    {
        public GroupOfSpanners()
        {
            OrganizerGroup = new HashSet<OrganizerGroup>();
            Spanner = new HashSet<Spanner>();
        }

        public string GroupName { get; set; }
        public DateTime RecordStarting { get; set; }
        public int? CalculationResetEvery { get; set; }
        public bool IsCalculationSectorTime { get; set; }
        public double? GroupCostant { get; set; }

        public ICollection<OrganizerGroup> OrganizerGroup { get; set; }
        public ICollection<Spanner> Spanner { get; set; }
    }
}
