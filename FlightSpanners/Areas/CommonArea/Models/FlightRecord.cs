using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class FlightRecord
    {
        public int FlightRecoedId { get; set; }
        public DateTime RecordDate { get; set; }
        public TimeSpan RecordTime { get; set; }
        public DateTime FlightDate { get; set; }
        public TimeSpan FlightTime { get; set; }
        public bool IsRecordSettled { get; set; }
        public bool IsRecordAutoSelected { get; set; }
        public int FlightDataId { get; set; }
        public int AircraftTypeId { get; set; }
        public int ApprovalId { get; set; }
        public int OrganizerGroupId { get; set; }
        public string FlightCompanyName { get; set; }
        public string OriginTypeName { get; set; }
        public string EligibilityTypeName { get; set; }
        public string DestinationTypeName { get; set; }

        public AircraftType AircraftType { get; set; }
        public Approval Approval { get; set; }
        public DestinationType DestinationTypeNameNavigation { get; set; }
        public EligibilityType EligibilityTypeNameNavigation { get; set; }
        public FlightCompany FlightCompanyNameNavigation { get; set; }
        public FlightData FlightData { get; set; }
        public OrganizerGroup OrganizerGroup { get; set; }
        public OriginType OriginTypeNameNavigation { get; set; }
    }
}
