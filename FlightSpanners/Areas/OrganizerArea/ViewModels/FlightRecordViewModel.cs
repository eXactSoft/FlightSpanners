
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
	public class FlightRecordViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//Constructor injection
		public FlightRecordViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;
		}

		//Initialize FlightRecordViewModel Properties
		public void SetFlightRecordViewModelProperties(int pageNo)
		{
			var currentGroup = _httpContext.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;

			//The number of flight record PageSize in one page
			PageSize = 10;//****This will be taken from a configuration file

			PageNo = pageNo;
			var recordList = GetRecordList(currentGroup);
			TotalRecords = recordList.Count();

			//Return the RecordList for the current organizer according to the current group name ordered by flight date
			//and for pagination to operate skip the ((PageNo - 1) * PageSize) rows starting from 1st row
			//and take from the remaining rows the first PageSize rows only
			RecordList = recordList.OrderByDescending(x => x.FlightDate).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();  
		}

		public int TotalRecords { get; set; }
		
		public int PageNo { get; set; }

		public int PageSize { get; set; }

		public List<Record> RecordList { get; set; }

		public class Record
		{
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
			public string AircraftModel { get; set; }
			public string EngineModel { get; set; }
			public double AircraftTypeConstant { get; set; }
			public string SpannerCode { get; set; }
			public string SpannerName { get; set; }
			public string ApprovalRating { get; set; }
			public string ApprovalCategory { get; set; }
			public double ApprovalConstant { get; set; }
			public string GroupName { get; set; }
			public string FlightCompanyName { get; set; }
			public string OriginTypeName { get; set; }
			public string EligibilityTypeName { get; set; }
			public string DestinationTypeName { get; set; }
		}

		private List<Record> GetRecordList(string groupName)
		{
			RecordList = new List<Record>();
			var flightRecordQuery = _flightSpannersData.GetFlightRecordByGroupName(groupName);
			Approval approval;
			FlightData flightData;
			AircraftType aircraftType;
			ApprovalDetail approvalDetail;
			Spanner spanner;
			bool ApprovalNotNull = false;
			foreach (var flightRecord in flightRecordQuery)
			{
				flightData = _flightSpannersData.GetFlightDataByFlightDataId(flightRecord.FlightDataId);
				aircraftType = _flightSpannersData.GetAircraftTypeByAircraftTypeId(flightRecord.AircraftTypeId);
				ApprovalNotNull = flightRecord.ApprovalId.HasValue;
				if (ApprovalNotNull)
				{
					approval = _flightSpannersData.GetApprovalByApprovalId(flightRecord.ApprovalId.Value);
					approvalDetail = _flightSpannersData.GetApprovalDetailByApprovalDetailId(approval.ApprovalDetailId);
					spanner = _flightSpannersData.GetSpannerByCode(approval.SpannerCode);

					RecordList.Add(new Record
					{
						RecordId = flightRecord.FlightRecordId,
						RecordDate = flightRecord.RecordDate,
						RecordTime = flightRecord.RecordTime,
						FlightDate = flightRecord.FlightDate,
						FlightTime = flightRecord.FlightTime,
						IsRecordSetteled = flightRecord.IsRecordSettled,
						IsRecordAutoSelected = flightRecord.IsRecordAutoSelected,
						AirportOrigin = flightData.AirportOrigin,
						AirportDestination = flightData.AirportDestination,
						DefaultSectorTime = flightData.DefaultSectorTime,
						FlightDataConstant = flightData.FlightDataConstant,
						IsFlightLocal = flightData.IsFlightLocal,
						IsFlightCargo = flightData.IsFlightCargo,
						FlightBonusCard = flightData.FlightBonusCard,
						AircraftModel = aircraftType.AircraftModel,
						EngineModel = aircraftType.EngineModel,
						AircraftTypeConstant = aircraftType.AircraftTypeConstant,
						//SpannerCode = ApprovalNotNull ? spanner.SpannerCode : null,
						SpannerCode = spanner.SpannerCode,
						SpannerName = spanner.SpannerFName + " " + spanner.SpannerM1Name,
						ApprovalRating = approvalDetail.ApprovalRating,
						ApprovalCategory =approvalDetail.ApprovalCategory,
						ApprovalConstant =approvalDetail.ApprovalConstant,
						GroupName = groupName,
						FlightCompanyName = flightRecord.FlightCompanyName,
						OriginTypeName = flightRecord.OriginTypeName,
						EligibilityTypeName = flightRecord.EligibilityTypeName,
						DestinationTypeName = flightRecord.DestinationTypeName
					});
				}
				else
				{
					RecordList.Add(new Record
					{
						RecordId = flightRecord.FlightRecordId,
						RecordDate = flightRecord.RecordDate,
						RecordTime = flightRecord.RecordTime,
						FlightDate = flightRecord.FlightDate,
						FlightTime = flightRecord.FlightTime,
						IsRecordSetteled = flightRecord.IsRecordSettled,
						IsRecordAutoSelected = flightRecord.IsRecordAutoSelected,
						AirportOrigin = flightData.AirportOrigin,
						AirportDestination = flightData.AirportDestination,
						DefaultSectorTime = flightData.DefaultSectorTime,
						FlightDataConstant = flightData.FlightDataConstant,
						IsFlightLocal = flightData.IsFlightLocal,
						IsFlightCargo = flightData.IsFlightCargo,
						FlightBonusCard = flightData.FlightBonusCard,
						AircraftModel = aircraftType.AircraftModel,
						EngineModel = aircraftType.EngineModel,
						AircraftTypeConstant = aircraftType.AircraftTypeConstant,
						GroupName = groupName,
						FlightCompanyName = flightRecord.FlightCompanyName,
						OriginTypeName = flightRecord.OriginTypeName,
						EligibilityTypeName = flightRecord.EligibilityTypeName,
						DestinationTypeName = flightRecord.DestinationTypeName
					});
				}
			}
			return RecordList;
		}

	}

}
