
using FlightSpanners.Areas.CommonArea.Models;
using FlightSpanners.Areas.CommonArea.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.OrganizerArea.ViewModels 
{
	public class FlightRecordDetailViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//Constructor injection
		public FlightRecordDetailViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;
		}

		//Initialize FlightRecordViewModel Properties
		public void SetFlightRecordDetailViewModelProperties(int flightRecordId)
		{
			//if (!flightRecordId.HasValue)
				//return;

			var currentGroup = _httpContext.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;

			FlightRecord flightRecord;
			
			FlightData flightData;
			AircraftType aircraftType;
			Approval approval;
			ApprovalDetail approvalDetail;
			Spanner spanner;

			flightRecord = _flightSpannersData.GetFlightRecordByFlightRecordId(flightRecordId);
			flightData = _flightSpannersData.GetFlightDataByFlightDataId(flightRecord.FlightDataId);
			aircraftType = _flightSpannersData.GetAircraftTypeByAircraftTypeId(flightRecord.AircraftTypeId);

			if (flightRecord.ApprovalId.HasValue)
			{
				approval = _flightSpannersData.GetApprovalByApprovalId(flightRecord.ApprovalId);
				approvalDetail = _flightSpannersData.GetApprovalDetailByApprovalDetailId(approval.ApprovalDetailId);
				spanner = _flightSpannersData.GetSpannerByCode(approval.SpannerCode);
				SpannerCode = spanner.SpannerCode;
				SpannerName = spanner.SpannerFName + " " + spanner.SpannerM1Name;
				ApprovalRating = approvalDetail.ApprovalRating;
				ApprovalCategory = approvalDetail.ApprovalCategory;
				ApprovalDetailSummary = SpannerName + " ," + SpannerCode + " ," + ApprovalRating + " ," + ApprovalCategory;
				ApprovalConstant = approvalDetail.ApprovalConstant;
			}

			RecordId = flightRecord.FlightRecordId;
			RecordDate = flightRecord.RecordDate;
			RecordTime = flightRecord.RecordTime;
			FlightDate = flightRecord.FlightDate;
			FlightTime = flightRecord.FlightTime;
			IsRecordSetteled = flightRecord.IsRecordSettled;
			IsRecordAutoSelected = flightRecord.IsRecordAutoSelected;
			AirportOrigin = flightData.AirportOrigin;
			AirportDestination = flightData.AirportDestination;
			DefaultSectorTime = flightData.DefaultSectorTime;
			FlightDataConstant = flightData.FlightDataConstant;
			IsFlightLocal = flightData.IsFlightLocal;
			IsFlightCargo = flightData.IsFlightCargo;
			FlightBonusCard = flightData.FlightBonusCard;
			FlightInfo = AirportOrigin + "/" + AirportDestination + " ," + DefaultSectorTime + "hrs" + " ," + FlightBonusCard + "Bonus";
			AircraftModel = aircraftType.AircraftModel;
			EngineModel = aircraftType.EngineModel;
			AircraftTypeSummary = AircraftModel + " ," + EngineModel;
			AircraftTypeConstant = aircraftType.AircraftTypeConstant;
			GroupName = currentGroup;
			FlightCompanyName = flightRecord.FlightCompanyName;
			OriginTypeName = flightRecord.OriginTypeName;
			EligibilityTypeName = flightRecord.EligibilityTypeName;
			DestinationTypeName = flightRecord.DestinationTypeName;
		}


		public int RecordId { get; set; }
		public DateTime RecordDate { get; set; }
		public TimeSpan RecordTime { get; set; }
		public DateTime FlightDate { get; set; }
		public TimeSpan FlightTime { get; set; }
		public bool IsRecordSetteled { get; set; }
		public bool IsRecordAutoSelected { get; set; }
		public string AirportOrigin { get; set; }
		public string AirportDestination { get; set; }
		public TimeSpan DefaultSectorTime { get; set; }
		public double FlightDataConstant { get; set; }
		public bool IsFlightLocal { get; set; }
		public bool IsFlightCargo { get; set; }
		public int FlightBonusCard { get; set; }
		public string FlightInfo { get; set; }
		public string AircraftModel { get; set; }
		public string EngineModel { get; set; }
		public string AircraftTypeSummary { get; set; }
		public double AircraftTypeConstant { get; set; }
		public string SpannerCode { get; set; }
		public string SpannerName { get; set; }
		public string ApprovalRating { get; set; }
		public string ApprovalCategory { get; set; }
		public double ApprovalConstant { get; set; }
		public string ApprovalDetailSummary { get; set; }
		public string GroupName { get; set; }
		public string FlightCompanyName { get; set; }
		public string OriginTypeName { get; set; }
		public string EligibilityTypeName { get; set; }
		public string DestinationTypeName { get; set; }
	}
}
