using FlightSpanners.Areas.CommonArea.Controllers;
using FlightSpanners.Areas.CommonArea.Services;
using FlightSpanners.Areas.OrganizerArea.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.OrganizerArea.Controllers
{
	[Area("OrganizerArea")] //Let the framework know which area this controller belongs to
	[Authorize(Roles = "Organizer")]
	public class GroupsDataController : Controller
	{
		private IFlightSpannersData _flightSpannersData;
		private GroupsDataViewModel _groupsDataViewModel;
		//private FlightRecordDetailViewModel _flightRecordDetailViewModel;

		//Constructor injection
		public GroupsDataController(IFlightSpannersData flightSpannersData, GroupsDataViewModel groupsDataViewModel)//, FlightRecordDetailViewModel flightRecordDetailViewModel)
		{
			_flightSpannersData = flightSpannersData;
			_groupsDataViewModel = groupsDataViewModel;
		}

		public IActionResult Index(int? page)
		{
			string code = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			if (code == null)
			{
				return RedirectToAction(nameof(HomeController.Index), _flightSpannersData.ControllerName(nameof(HomeController)), new { area = nameof(CommonArea) });
			}
			else
			{
				//page ?? 1 {null coalescing operator} ==> return the valur of page if it has a value
				//,or return 1 if page is null
				_groupsDataViewModel.SetGroupsDataViewModelProperties(page ?? 1);
				return View(nameof(GroupsDataController.Index), _groupsDataViewModel);
			}

		}


		/*public IActionResult Detail(int? flightRecordId)
		{
			if (!flightRecordId.HasValue)
			{
				return RedirectToAction(nameof(HomeController.Index));
			}
			else
			{
				_flightRecordDetailViewModel.SetFlightRecordDetailViewModelProperties(flightRecordId.Value);
				return View(nameof(FlightRecordController.Detail), _flightRecordDetailViewModel);
			}
		}*/

	}
}
