﻿using System;
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

		public bool ValidateOrganizerCode(string code)
		{
			object user = _context.Organizer.FirstOrDefault(r => r.OrganizerCode == code);

			if (user != null)
			{
				return true;
			}

			return false;
		}

		public bool ValidateSpannerCode(string code)
		{
			object user = _context.Spanner.FirstOrDefault(r => r.SpannerCode == code);

			if (user != null)
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

		public IEnumerable<Spanner> GetSpannersByGroupName(string groupName)
		{
			/*
			var spannerQuery = from spanner in _context.Spanner // outer sequence
													join groupOfSpanners in _context.GroupOfSpanners //inner sequence 
													on spanner.GroupName equals groupOfSpanners.GroupName // key selector 
												 //where spanner.GroupName.Equals(groupName)
												 select spanner;
			*/
			var spannerQuery = from spanner in _context.Spanner // outer sequence
											   where spanner.GroupName.Equals(groupName)
												 select spanner;

			return spannerQuery;
		}

		public double GetFlightsAll(string code)
		{
			return  (from fligthRecord in _context.FlightRecord // outer sequence
								join approval in _context.Approval //inner sequence 
								on fligthRecord.ApprovalId equals approval.ApprovalId // key selector 
								where approval.SpannerCode.Equals(code)
								select fligthRecord
								).Count();
		}

		//Get the number of short flights for a given spanner code
		public double GetFlightsShort(string code)
		{
			var flightRecordsForGivenCodeQuery = from fligthRecord in _context.FlightRecord // outer sequence
																						join approval in _context.Approval //inner sequence 
																						on fligthRecord.ApprovalId equals approval.ApprovalId // key selector 
																						where approval.SpannerCode.Equals(code)
																						select fligthRecord;

			var flightDataForGivenCodeQuery = from fligthRecord in flightRecordsForGivenCodeQuery // outer sequence
																				join flightData in _context.FlightData //inner sequence 
																				on fligthRecord.FlightDataId equals flightData.FlightDataId // key selector 
																				select flightData;

			//double shortDistanceTypeCostant = GetDistanceType("short").DistanceTypeCostant;
			DistanceType distanceType = GetDistanceType("short");
			double shortFlightsForGivenCode = (from fligthData in flightDataForGivenCodeQuery
																				 where SectorTimeCheckByDistanceTypeName(fligthData.DefaultSectorTime.TotalHours, distanceType)
																				 select fligthData.DefaultSectorTime
																				).Count();// * shortDistanceTypeCostant;

			return shortFlightsForGivenCode;
			
			//return 2.0;
		}

		public double GetFlightsLong(string code)
		{
			//*******To be implemented !!!
			return 1.0;
		}

		public double GetFlightsExtra(string code)
		{
			//*******To be implemented !!!
			return 2.0;
		}

		public double GetFlightsMultiple(string code)
		{
			//*******To be implemented !!!
			return 3.0;
		}

		public double GetFlightsApology(string code)
		{
			//*******To be implemented !!!
			return 4.0;
		}

		public double GetFlightsBonus(string code)
		{
			//*******To be implemented !!!
			return 5.0;
		}

		public double GetFlightsHoliday(string code)
		{
			//*******To be implemented !!!
			return 6.0;
		}

		public double GetFlightsCurrentMonth(string code)
		{
			//*******To be implemented !!!
			return 7.0;
		}

		public int InActiveDaysCount(string code)
		{
			//*******To be implemented !!!
			return 10;
		}

		public bool IsCurrentlyInActive(string code)
		{
			//*******To be implemented !!!
			return false;
		}

		public double GetSpannerBalance(string code)
		{
			//*******To be implemented !!!
			return 0.85;
		}

		public double GetSpannerDeservedFlights(string code)
		{
			//*******To be implemented !!!
			return 2.0;
		}
		//This method to check a given sectorTime is of type distanceTypeName or not.
		private bool SectorTimeCheckByDistanceTypeName(double sectorTime, DistanceType distancetype)
			{
				//var distancetype = GetDistanceType(distanceTypeName);

				return
					SectorTimeCheckByConverStringToOperator(distancetype.UpperOperator, sectorTime, distancetype.UpperSectorTime)
					&&
					SectorTimeCheckByConverStringToOperator(distancetype.LowerOperator, sectorTime, distancetype.LowerSectorTime);
			}

		//This method is to convert a operator string to relational operator.
		//It retruns true if the switch check returns true or if the operatorString is Null value
		private bool SectorTimeCheckByConverStringToOperator(string operatorString, double sectorTime, double? limitSectorTime)
		{
			bool sectorCheck = false;
			if (!String.IsNullOrEmpty(operatorString)) //Check if the operatorString is not Null value
			{
				switch (operatorString) //To convert string value to relational operator and use that operator to check the sector time
				{
					case ">":
						sectorCheck = sectorTime > limitSectorTime;
						break;
					case "<":
						sectorCheck = sectorTime < limitSectorTime;
						break;
					case ">=":
						sectorCheck = sectorTime >= limitSectorTime;
						break;
					case "<=":
						sectorCheck = sectorTime <= limitSectorTime;
						break;
					case "==":
						sectorCheck = sectorTime == limitSectorTime;
						break;
					default: throw new Exception("invalid logic");
				}
			}
			else
			{
				sectorCheck = true; //Set sectorCheck = true without check in case of operatorString is Null value only 
			}

			return sectorCheck;
		}

		public DistanceType GetDistanceType(string distanceTypeName)
		{
			return _context.DistanceType.FirstOrDefault(r => r.DistanceTypeName.ToLower().Equals(distanceTypeName.ToLower()));
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

		public string GetSpannerGroup(string code) //IQuarable
		{
			return _context.Spanner.FirstOrDefault(r => r.SpannerCode == code).GroupName;
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
								 + aircraftTypeQuery.AircraftModel + ", "
								 + aircraftTypeQuery.EngineModel 
				});
				i++;
			}

			return options;
		}

		public IEnumerable<Approval> GetApprovalBySpannerCode(string code)
		{
			return (from approval in _context.Approval
							where (approval.SpannerCode == code)
							select approval);
		}

		public IEnumerable<InActivePeriod> GetInActivePeriodByGroupName(string groupName)
		{
			return (from inActivePeriod in _context.InActivePeriod // outer sequence
							join spanner in _context.Spanner //inner sequence 
							on inActivePeriod.SpannerCode equals spanner.SpannerCode // key selector 
							where spanner.GroupName.Equals(groupName)
							select inActivePeriod
						 );
		}
		public IEnumerable<InActivePeriod> GetInActivePeriodBySpannerCode(string spannerCode)
		{
			return (from inActivePeriod in _context.InActivePeriod 
							where inActivePeriod.SpannerCode.Equals(spannerCode)
							select inActivePeriod
						 );
		}
		public IEnumerable<FlightRecord> GetFlightRecordByGroupName(string groupName)
		{
			//int organizerGroupId = _context.OrganizerGroup.FirstOrDefault(x => x.GroupName == groupName).OrganizerGroupId;
			return( from fligthRecord in _context.FlightRecord // outer sequence
							join organizerGroup in _context.OrganizerGroup //inner sequence 
							on fligthRecord.OrganizerGroupId equals organizerGroup.OrganizerGroupId // key selector 
							where organizerGroup.GroupName.Equals(groupName)
							select fligthRecord
			       );
		}

		public IEnumerable<OrganizerGroup> GetGroupsByOrganizerCode(string organizerCode)
		{
			return (from organizerGroup in _context.OrganizerGroup
							where (organizerGroup.OrganizerCode == organizerCode)
							select organizerGroup
							);
		}

		public GroupOfSpanners GetGroupOfSpannersByGroupName(string groupName)
		{
			return _context.GroupOfSpanners.FirstOrDefault(r => r.GroupName == groupName);

			/*
			 return (from groupOfSpanners in _context.GroupOfSpanners
							where (groupOfSpanners.GroupName == groupName)
							select groupOfSpanners
							);
							*/
		}

		public FlightData GetFlightDataByFlightDataId(int flightDataId)
		{
			return _context.FlightData.FirstOrDefault(r => r.FlightDataId == flightDataId);
		}
		public AircraftType GetAircraftTypeByAircraftTypeId(int aircraftTypeId)
		{
			return _context.AircraftType.FirstOrDefault(r => r.AircraftTypeId == aircraftTypeId);
		}
		public Approval GetApprovalByApprovalId(int? approvalId)
		{
			return _context.Approval.FirstOrDefault(r => r.ApprovalId == approvalId);
		}
		public ApprovalDetail GetApprovalDetailByApprovalDetailId(int approvalDetailId)
		{
			return _context.ApprovalDetail.FirstOrDefault(r => r.ApprovalDetailId == approvalDetailId);
		}
		public FlightRecord GetFlightRecordByFlightRecordId(int flightRecordId)
		{
			return _context.FlightRecord.FirstOrDefault(r => r.FlightRecordId == flightRecordId);
		}
		public Qualification GetQualificationByQualificationId(int qualificationId)
		{
			return _context.Qualification.FirstOrDefault(r => r.QualificationId == qualificationId);
		}
		/*public int[] ConvertLengthToIntArray(int length)
		{
			int[] arrayInt = new int[length];
			for (int i = 0; i < length; i++)
			{
				arrayInt[i] = i;
			}
			return arrayInt;
		}*/

		public string ControllerName(string name)
		{
			return name.Replace("Controller", String.Empty);
		}
	}
}
