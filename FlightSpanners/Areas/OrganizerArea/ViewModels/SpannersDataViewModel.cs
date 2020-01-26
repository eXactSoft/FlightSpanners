
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
	public class SpannersDataViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//Constructor injection
		public SpannersDataViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;
		}

		//Initialize SpannersDataViewModel Properties
		public void SetSpannersDataViewModelProperties(int pageNo)
		{
			var currentGroup = _httpContext.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;

			//The number of Spanners Data PageSize in one page
			PageSize = 10;//****This will be taken from a configuration file

			PageNo = pageNo;

			var spannersDataList = GetSpannersDataList(currentGroup);
			TotalRecords = spannersDataList.Count();

			//Return the SpannersDataList for the current organizer according to the current group name ordered by spanners name
			//and for pagination to operate skip the ((PageNo - 1) * PageSize) rows starting from 1st row
			//and take from the remaining rows the first PageSize rows only
			SpannersDataList = spannersDataList.OrderBy(x => x.SpannerName).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();  
			 //.OrderBy(x => x.DeservedFlights)
		}

		public int TotalRecords { get; set; }
		
		public int PageNo { get; set; }

		public int PageSize { get; set; }

		public List<SpannerData> SpannersDataList { get; set; }

		public class SpannerData
		{
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
			public string DepartmentName { get; set; }
			public bool IsSpannerViewGroupData { get; set; }
			public bool IsSpannerHasCar { get; set; }
			public Qualification SpannerQualification { get; set; }
		}

		private List<SpannerData> GetSpannersDataList(string groupName)
		{
			SpannersDataList = new List<SpannerData>();
			var spannerQuery = _flightSpannersData.GetSpannersByGroupName(groupName);
			foreach (var spanner in spannerQuery)
			{
				SpannersDataList.Add(new SpannerData {
					SpannerCode = spanner.SpannerCode,
					SpannerName = spanner.SpannerFName + " " + spanner.SpannerM1Name,
					SpannerLicenseNo = spanner.SpannerLicenseNo,
					DepartmentName = spanner.DepartmentName
				});
			}
			return SpannersDataList;
		}

	}

}
