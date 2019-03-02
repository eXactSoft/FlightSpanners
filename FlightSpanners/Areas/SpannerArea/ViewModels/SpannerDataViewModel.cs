
using FlightSpanners.Areas.CommonArea.Models;
using FlightSpanners.Areas.CommonArea.Services;
//using FlightSpanners.Areas.OrganizerArea.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.SpannerArea.ViewModels //.Home
{
  public class SpannerDataViewModel
  {
		//Constructor injection
		public SpannerDataViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			//IServiceProvider services = HttpContext.RequestServices;  
			//[Optional] [FromServices] 
			//var log = (IOrganizerData)services.GetService(typeof(IOrganizerData));

			string code = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			//Return the spanner based on the code
			var spanner = flightSpannersData.GetSpannerByCode(code);

			this.SpannerCode = spanner.SpannerCode;
			this.SpannerLicenseNo = spanner.SpannerLicenseNo;
			this.GroupName = spanner.GroupName;
			this.SpannerEmail = spanner.SpannerEmail;
			this.SpannerFName = spanner.SpannerFName;
			this.SpannerLName = spanner.SpannerLName;
			this.SpannerM1Name = spanner.SpannerM1Name;
			this.SpannerM2Name = spanner.SpannerM2Name;
			this.SpannerMobile1 = spanner.SpannerMobile1;
			this.SpannerMobile2 = spanner.SpannerMobile2;
			this.SpannerGender = spanner.SpannerGender;
			this.SpannerBirthday = spanner.SpannerBirthday;
			this.DepartmentName = spanner.DepartmentName;
			this.SpannerHireDate = spanner.SpannerHireDate;
			this.IsSpannerViewGroupData = spanner.IsSpannerViewGroupData;
			this.IsSpannerHasCar = spanner.IsSpannerHasCar;

			////Get the Qualification of the spanner using GetQualificationSpanner(code) method
			this.SpannerQualification = flightSpannersData.GetQualificationOfSpanner(code);

			//Get the ApprovalList item of the model using GetApprovalSelectListItems(code) method
			this.ApprovalList = flightSpannersData.GetApprovalSelectListItems(code);

			//Get the InActivePeriodList item of the model using GetInActivePeriodListItems(code) method
			//this.InActivePeriodList = flightSpannersData.GetInActivePeriodListItems(code);

			//Get the ApprovalListValues array from the length of ApprovalList
			this.ApprovalListValues = flightSpannersData.ConvertLengthToIntArray(this.ApprovalList.Count);

			//Get the InActivePeriodListValues array from the length of InActivePeriodList
			//this.InActivePeriodListValues = flightSpannersData.ConvertLengthToIntArray(this.InActivePeriodList.Count);
		}

		public string SpannerCode { get; set; }
		public string SpannerLicenseNo { get; set; }
		public string GroupName { get; set; }
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
		public string DepartmentName { get; set; }
		public bool IsSpannerViewGroupData { get; set; }
		public bool IsSpannerHasCar { get; set; }
		public Qualification SpannerQualification { get; set; }

		public List<SelectListItem> ApprovalList { get; set; }
		//public List<SelectListItem> InActivePeriodList { get; set; }
		public int[] ApprovalListValues { get; set; }
		//public int[] InActivePeriodListValues { get; set; }
	}
}
