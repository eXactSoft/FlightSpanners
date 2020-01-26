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
	public class FlightRecordController : Controller
	{
		private IFlightSpannersData _flightSpannersData;
		private FlightRecordViewModel _flightRecordViewModel;
		private FlightRecordDetailViewModel _flightRecordDetailViewModel;

		//Constructor injection
		public FlightRecordController(IFlightSpannersData flightSpannersData, FlightRecordViewModel flightRecordViewModel, FlightRecordDetailViewModel flightRecordDetailViewModel)
		{
			_flightSpannersData = flightSpannersData;
			_flightRecordViewModel = flightRecordViewModel;
			_flightRecordDetailViewModel = flightRecordDetailViewModel;
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
				_flightRecordViewModel.SetFlightRecordViewModelProperties(page ?? 1);
				return View(nameof(FlightRecordController.Index), _flightRecordViewModel);
			}
		}

		public IActionResult Detail(int? flightRecordId)
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
		}

	}
}
