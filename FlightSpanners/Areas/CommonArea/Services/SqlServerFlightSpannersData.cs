using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSpanners.Areas.CommonArea.Data;
using FlightSpanners.Areas.CommonArea.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlightSpanners.Areas.CommonArea.Services
{
  public class SqlServerFlightSpannersData : IFlightSpannersData
	{
    private FlightSpannersContext _context;

    public SqlServerFlightSpannersData(FlightSpannersContext context) //Constructor injection
    {
      _context = context;
    }

    public bool UpdatePassword(string code, string newPassword)
    {
			var organizer = _context.Organizer.FirstOrDefault(r => r.OrganizerCode == code);
			if (! (organizer == null) )
			{ 
				organizer.OrganizerPassword = newPassword;
				_context.Attach(organizer);
				_context.Entry(organizer).State = EntityState.Modified;
				//_context.Entry(organizer).Property(p => p.OrganizerPassword).IsModified = true;
			}

			var spanner = _context.Spanner.FirstOrDefault(r => r.SpannerCode == code);
			if (!(spanner == null))
			{
				spanner.SpannerPassword = newPassword;
				_context.Attach(spanner);
				_context.Entry(spanner).State = EntityState.Modified;
			}
			_context.SaveChanges();
      return true;
    }

		public bool ValidateCode(string code)
		{
			object user = _context.Organizer.FirstOrDefault(r => r.OrganizerCode == code);
			if (user == null)
			{
				user = _context.Spanner.FirstOrDefault(r => r.SpannerCode == code); 
			}
			
			if(user != null)
			{
				return true;
			}

			return false;
		}

		public bool ValidatePasswordOrganizer(string code, string password)
    {
			Organizer organizer = _context.Organizer.FirstOrDefault(r => r.OrganizerCode == code);

			if (organizer == null)
			{ 
				return false;
			}

			string databasePassword = organizer.OrganizerPassword;
			bool returnValue = databasePassword.Equals(password) ? true : false;
      return returnValue;
    }

		public bool ValidatePasswordSpanner(string code, string password)
		{
			Spanner spanner = _context.Spanner.FirstOrDefault(r => r.SpannerCode == code);

			if(spanner == null)
			{
				return false;
			}

			string databasePassword = spanner.SpannerPassword;
			bool returnValue = databasePassword.Equals(password) ? true : false;
			return returnValue;
		}

		public Spanner GetSpannerByCode(string code)
		{
			return _context.Spanner.FirstOrDefault(r => r.SpannerCode == code);
		}

		public Organizer GetOrganizerByCode(string code) 
		{
			return _context.Organizer.FirstOrDefault(r => r.OrganizerCode == code);
		}

		public List<SelectListItem> GetOrganizerGroupSelectListItems(string code) //IQuarable
    {
			IEnumerable<OrganizerGroup> organizerGroupQuery = from organizerGroup in _context.OrganizerGroup
																												 where (organizerGroup.OrganizerCode == code)
																												 select organizerGroup;
			List<SelectListItem> options = new List<SelectListItem>();
			int i = 0;
			foreach (var group in organizerGroupQuery)
			{
				options.Add(new SelectListItem { Value= i.ToString(), Text= group.GroupName });
				i++;
			}
			return options;

		}

		public Qualification GetQualificationOfSpanner(string code)
		{
			Spanner spanner = _context.Spanner.FirstOrDefault(r => r.SpannerCode == code);
			return _context.Qualification.FirstOrDefault(r => r.QualificationId == spanner.QualificationId);
		}

		public List<SelectListItem> GetApprovalSelectListItems(string code) //IQuarable
		{
			IEnumerable<Approval> spannerApprovalQuery = from approval in _context.Approval
																										where (approval.SpannerCode == code)
																										select approval;
			List<SelectListItem> options = new List<SelectListItem>();
			int i = 0;
			foreach (var item in spannerApprovalQuery)
			{
				ApprovalDetail approvalDetailQuery = ( from approvalDetail in _context.ApprovalDetail
																							 	where (approvalDetail.ApprovalDetailId == item.ApprovalDetailId)
																								select approvalDetail
																							).FirstOrDefault();

				AircraftType aircraftTypeQuery = (from aircraftType in _context.AircraftType
																					where (aircraftType.AircraftTypeId == item.AircraftTypeId)
																							select aircraftType
																							).FirstOrDefault();

				options.Add(new SelectListItem { Value = i.ToString(),
					Text = approvalDetailQuery.ApprovalRating + ", " 
								 + approvalDetailQuery.ApprovalCategory + ", "
								 + aircraftTypeQuery.AircrfatModel + ", "
								 + aircraftTypeQuery.EngineModel 
				});
				i++;
			}

			return options;
		}

		public int[] ConvertLengthToIntArray(int length)
		{
			int[] arrayInt = new int[length];
			for (int i = 0; i < length; i++)
			{
				arrayInt[i] = i;
			}
			return arrayInt;
		}

		public string ControllerName(string name)
		{
			return name.Replace("Controller", String.Empty);
		}
	}
}
