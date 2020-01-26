
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
	public class SpannersDataDetailViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//Constructor injection
		public SpannersDataDetailViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;
		}

		//Initialize SpannersDataDetailViewModel Properties
		public void SetSpannersDataDetailViewModelProperties(string spannerCode)
		{
			//var currentGroup = _httpContext.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;

			Spanner spanner = _flightSpannersData.GetSpannerByCode(spannerCode);
			Qualification qualification = _flightSpannersData.GetQualificationByQualificationId(spanner.QualificationId);

			SpannerCode = spanner.SpannerCode;
			SpannerLicenseNo = spanner.SpannerLicenseNo;
			GroupName = spanner.GroupName;
			SpannerFName = spanner.SpannerFName;
			SpannerM1Name = spanner.SpannerM1Name;
			SpannerM2Name = spanner.SpannerM2Name;
			SpannerLName = spanner.SpannerLName;
			SpannerEmail = spanner.SpannerEmail;
			SpannerMobile1 = spanner.SpannerMobile1;
			SpannerMobile2 = spanner.SpannerMobile2;
			SpannerGender = spanner.SpannerGender;
			SpannerBirthday = spanner.SpannerBirthday;
			SpannerHireDate = spanner.SpannerHireDate;
			IsSpannerViewGroupData = spanner.IsSpannerViewGroupData;
			IsSpannerHasCar = spanner.IsSpannerHasCar;

			DepartmentName = spanner.DepartmentName;

			QualificationId = qualification.QualificationId;
			QualificationName = qualification.QualificationName;
			QualificationDegree = qualification.QualificationDegree;
			QualificationMajor = qualification.QualificationMajor;
		}

		public string SpannerCode { get; set; }
		public string SpannerLicenseNo { get; set; }
		public string GroupName { get; set; }
		public string SpannerName { get; set; }
		public string SpannerFName { get; set; }
		public string SpannerM1Name { get; set; }
		public string SpannerM2Name { get; set; }
		public string SpannerLName { get; set; }
		public string SpannerEmail { get; set; }
		public string SpannerMobile1 { get; set; }
		public string SpannerMobile2 { get; set; }
		public string SpannerGender { get; set; }
		public DateTime? SpannerBirthday { get; set; }
		public DateTime? SpannerHireDate { get; set; }
		public bool IsSpannerViewGroupData { get; set; }
		public bool IsSpannerHasCar { get; set; }

		public string DepartmentName { get; set; }
		public double? DepartmentConstant { get; set; }

		public int QualificationId  { get; set; }
		public string QualificationName { get; set; }
		public string QualificationDegree { get; set; }
		public string QualificationMajor { get; set; }
		public double? QualificationConstant { get; set; }
	}
}
