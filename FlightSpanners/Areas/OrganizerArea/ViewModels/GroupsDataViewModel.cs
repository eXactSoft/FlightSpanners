
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
	public class GroupsDataViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//Constructor injection
		public GroupsDataViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;
		}

		//Initialize GroupsDataViewModel Properties
		public void SetGroupsDataViewModelProperties(int pageNo)
		{
			var currentOrganizerCode = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			//The number of Groups Data PageSize in one page
			PageSize = 10;//****This will be taken from a configuration file

			PageNo = pageNo;

			var groupsDataList = GetGroupsDataList(currentOrganizerCode);
			TotalRecords = groupsDataList.Count();

			//Return the GroupDataList for the current organizer according to the current organizer code ordered by groups name
			//and for pagination to operate skip the ((PageNo - 1) * PageSize) rows starting from 1st row
			//and take from the remaining rows the first PageSize rows only
			GroupsDataList = groupsDataList.OrderBy(x => x.GroupName).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();  
			 //.OrderBy(x => x.DeservedFlights)
		}

		public int TotalRecords { get; set; }
		
		public int PageNo { get; set; }

		public int PageSize { get; set; }

		public List<GroupData> GroupsDataList { get; set; }

		public class GroupData
		{
			public int OrganizerGroupId { get; set; }
			public string OrganizerCode { get; set; }
			public string GroupName { get; set; }
			public DateTime RecordStarting { get; set; }
			public int? CalculationResetEvery { get; set; }
			public bool IsCalculationSectorTime { get; set; }
			public double? GroupCostant { get; set; }
		}

		private List<GroupData> GetGroupsDataList(string organizerCode)
		{
			GroupsDataList = new List<GroupData>();
			var groupQuery = _flightSpannersData.GetGroupsByOrganizerCode(organizerCode);
			GroupOfSpanners groupOfSpanners;
			foreach (var group in groupQuery)
			{
				groupOfSpanners = _flightSpannersData.GetGroupOfSpannersByGroupName(group.GroupName);
				GroupsDataList.Add(new GroupData {
					OrganizerCode = group.OrganizerCode,
					GroupName = group.GroupName,
					RecordStarting = groupOfSpanners.RecordStarting,
					IsCalculationSectorTime = groupOfSpanners.IsCalculationSectorTime,
					CalculationResetEvery = groupOfSpanners.CalculationResetEvery
				});
			}
			return GroupsDataList;
		}

	}

}
