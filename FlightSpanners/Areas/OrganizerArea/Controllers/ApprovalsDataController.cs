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
	public class ApprovalsDataController : Controller
	{
		private IFlightSpannersData _flightSpannersData;
		private ApprovalsDataViewModel _approvalsDataViewModel;
		private ApprovalsDataDetailViewModel _approvalsDataDetailViewModel;

		//Constructor injection
		public ApprovalsDataController(IFlightSpannersData flightSpannersData, ApprovalsDataViewModel approvalsDataViewModel, ApprovalsDataDetailViewModel approvalsDataDetailViewModel)
		{
			_flightSpannersData = flightSpannersData;
			_approvalsDataViewModel = approvalsDataViewModel;
			_approvalsDataDetailViewModel = approvalsDataDetailViewModel;
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
				_approvalsDataViewModel.SetApprovalsDataViewModelProperties(page ?? 1);
				return View(nameof(ApprovalsDataController.Index), _approvalsDataViewModel);
			}
		}

		public IActionResult Detail(int? approvalId)
		{
			if (!approvalId.HasValue)
			{
				return RedirectToAction(nameof(HomeController.Index));
			}
			else
			{
				_approvalsDataDetailViewModel.SetApprovalsDataDetailViewModelProperties(approvalId.Value);
				return View(nameof(ApprovalsDataController.Detail), _approvalsDataDetailViewModel);
			}
		}

	}
}
