
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
	public class ApprovalsDataViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//Constructor injection
		public ApprovalsDataViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;
		}

		//Initialize ApprovalsViewModel Properties
		public void SetApprovalsDataViewModelProperties(int pageNo)
		{
			var currentGroup = _httpContext.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;

			//The number of Approvals PageSize in one page
			PageSize = 10;//****This will be taken from a configuration file

			PageNo = pageNo;
			var approvalsDataList = GetApprovalsDataList(currentGroup);
			TotalRecords = approvalsDataList.Count();

			//Return the ApprovalsDataList for the spanners according to the current group name ordered by spanners name
			//and for pagination to operate skip the ((PageNo - 1) * PageSize) rows starting from 1st row
			//and take from the remaining rows the first PageSize rows only
			ApprovalsDataList = approvalsDataList.OrderBy(x => x.SpannerName).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();  
			 //.OrderBy(x => x.DeservedFlights)
		}

		public int TotalRecords { get; set; }
		
		public int PageNo { get; set; }

		public int PageSize { get; set; }

		public List<ApprovalsData> ApprovalsDataList { get; set; }

		public class ApprovalsData
		{
			public int ApprovalId { get; set; }
			public string SpannerCode { get; set; }
			public string SpannerLicenseNo { get; set; }
			//public string GroupName { get; set; }
			public string SpannerName { get; set; }
			public string AircraftModel { get; set; }
			public string EngineModel { get; set; }
			public string ApprovalRating { get; set; }
			public string ApprovalCategory { get; set; }
		}

		private List<ApprovalsData> GetApprovalsDataList(string groupName)
		{
			ApprovalsDataList = new List<ApprovalsData>();

			var spannerQuery = _flightSpannersData.GetSpannersByGroupName(groupName);

			//Approval approval;
			AircraftType aircraftType;
			ApprovalDetail approvalDetail;
			foreach (var spanner in spannerQuery)
			{
				var approvalsDataQuery = _flightSpannersData.GetApprovalBySpannerCode(spanner.SpannerCode);

				foreach (var approval in approvalsDataQuery)
				{
					aircraftType = _flightSpannersData.GetAircraftTypeByAircraftTypeId(approval.AircraftTypeId);
					approvalDetail = _flightSpannersData.GetApprovalDetailByApprovalDetailId(approval.ApprovalDetailId);

					ApprovalsDataList.Add(new ApprovalsData{
						ApprovalId = approval.ApprovalId,
						SpannerCode = spanner.SpannerCode,
						SpannerName = spanner.SpannerFName + " " + spanner.SpannerM1Name,
						SpannerLicenseNo = spanner.SpannerLicenseNo,
						AircraftModel = aircraftType.AircraftModel,
						EngineModel = aircraftType.EngineModel,
						ApprovalRating = approvalDetail.ApprovalRating,
						ApprovalCategory = approvalDetail.ApprovalCategory
					});
				}

			}
			return ApprovalsDataList;
		}

	}

}
