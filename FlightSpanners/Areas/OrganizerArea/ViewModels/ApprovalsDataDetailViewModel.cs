
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
	public class ApprovalsDataDetailViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//Constructor injection
		public ApprovalsDataDetailViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;
		}

		//Initialize SpannersDataDetailViewModel Properties
		public void SetApprovalsDataDetailViewModelProperties(int approvalId)
		{
			var currentGroup = _httpContext.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;

			Approval approval = _flightSpannersData.GetApprovalByApprovalId(approvalId);
			Spanner spanner = _flightSpannersData.GetSpannerByCode(approval.SpannerCode);
			AircraftType aircraftType = _flightSpannersData.GetAircraftTypeByAircraftTypeId(approval.AircraftTypeId); ;
			ApprovalDetail approvalDetail = _flightSpannersData.GetApprovalDetailByApprovalDetailId(approval.ApprovalDetailId);

			GroupName = currentGroup;

			ApprovalId = approvalId;
			SpannerCode = approval.SpannerCode;
			SpannerLicenseNo = spanner.SpannerLicenseNo;
			SpannerName = spanner.SpannerFName + " " + spanner.SpannerM1Name;

			AircraftTypeId = aircraftType.AircraftTypeId;
			AircraftModel = aircraftType.AircraftModel;
			EngineModel = aircraftType.EngineModel;
			AircraftTypeConstant = aircraftType.AircraftTypeConstant;

			ApprovalDetailId = approvalDetail.ApprovalDetailId;
			ApprovalRating = approvalDetail.ApprovalRating;
			ApprovalCategory = approvalDetail.ApprovalCategory;
			ApprovalConstant = approvalDetail.ApprovalConstant;
	  }

		public string GroupName { get; set; }

		public int ApprovalId { get; set; }
		public string SpannerCode { get; set; }
		public string SpannerLicenseNo { get; set; }
		public string SpannerName { get; set; }

		public int AircraftTypeId { get; set; }
  	public string AircraftModel { get; set; }
		public string EngineModel { get; set; }
		public double? AircraftTypeConstant { get; set; }

		public int ApprovalDetailId { get; set; }
		public string ApprovalRating { get; set; }
		public string ApprovalCategory { get; set; }
		public double? ApprovalConstant { get; set; }
	}
}
