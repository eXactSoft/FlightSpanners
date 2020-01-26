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
	public class SpannersDataController : Controller
	{
		private IFlightSpannersData _flightSpannersData;
		private SpannersDataViewModel _spannersDataViewModel;
		private SpannersDataDetailViewModel _spannersDataDetailViewModel;

		//Constructor injection
		public SpannersDataController(IFlightSpannersData flightSpannersData, SpannersDataViewModel spannersDataViewModel, SpannersDataDetailViewModel spannersDataDetailViewModel)
		{
			_flightSpannersData = flightSpannersData;
			_spannersDataViewModel = spannersDataViewModel;
			_spannersDataDetailViewModel = spannersDataDetailViewModel;
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
				_spannersDataViewModel.SetSpannersDataViewModelProperties(page ?? 1);
				return View(nameof(SpannersDataController.Index), _spannersDataViewModel);
			}
		}

		public IActionResult Detail(string spannerCode)
		{
			if (String.IsNullOrEmpty(spannerCode))
			{
				return RedirectToAction(nameof(HomeController.Index));
			}
			else
			{
				_spannersDataDetailViewModel.SetSpannersDataDetailViewModelProperties(spannerCode);
				return View(nameof(SpannersDataController.Detail), _spannersDataDetailViewModel);
			}
		}

	}
}
