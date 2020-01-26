using System;
using FlightSpanners.Areas.CommonArea.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlightSpanners.Areas.CommonArea.Data
{
    public partial class FlightSpannersContext : DbContext
    {
        public FlightSpannersContext()
        {
        }

        public FlightSpannersContext(DbContextOptions<FlightSpannersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AircraftType> AircraftType { get; set; }
        public virtual DbSet<Airport> Airport { get; set; }
        public virtual DbSet<Approval> Approval { get; set; }
        public virtual DbSet<ApprovalDetail> ApprovalDetail { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DestinationType> DestinationType { get; set; }
        public virtual DbSet<DistanceType> DistanceType { get; set; }
        public virtual DbSet<EligibilityType> EligibilityType { get; set; }
        public virtual DbSet<FlightCompany> FlightCompany { get; set; }
        public virtual DbSet<FlightData> FlightData { get; set; }
        public virtual DbSet<FlightRecord> FlightRecord { get; set; }
        public virtual DbSet<GroupOfSpanners> GroupOfSpanners { get; set; }
        public virtual DbSet<Holiday> Holiday { get; set; }
        public virtual DbSet<HolidayType> HolidayType { get; set; }
        public virtual DbSet<InActivePeriod> InActivePeriod { get; set; }
        public virtual DbSet<Organizer> Organizer { get; set; }
        public virtual DbSet<OrganizerGroup> OrganizerGroup { get; set; }
        public virtual DbSet<OriginType> OriginType { get; set; }
        public virtual DbSet<Qualification> Qualification { get; set; }
        public virtual DbSet<Spanner> Spanner { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=AEROSPACE\\SQLEXPRESS;Initial Catalog=FlightSpanners;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AircraftType>(entity =>
            {
                entity.Property(e => e.AircraftModel)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EngineModel)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(e => e.Iatacode);

                entity.Property(e => e.Iatacode)
                    .HasColumnName("IATACode")
                    .HasMaxLength(3)
                    .ValueGeneratedNever();

                entity.Property(e => e.AirportCity)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.AirportCountry)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.AirportName).HasMaxLength(40);

                entity.Property(e => e.Icaocode)
                    .IsRequired()
                    .HasColumnName("ICAOCode")
                    .HasMaxLength(4);
            });

            modelBuilder.Entity<Approval>(entity =>
            {
                entity.Property(e => e.SpannerCode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.HasOne(d => d.AircraftType)
                    .WithMany(p => p.Approval)
                    .HasForeignKey(d => d.AircraftTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Approval_AircraftType1");

                entity.HasOne(d => d.ApprovalDetail)
                    .WithMany(p => p.Approval)
                    .HasForeignKey(d => d.ApprovalDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Approval_ApprovalDetial");

                entity.HasOne(d => d.SpannerCodeNavigation)
                    .WithMany(p => p.Approval)
                    .HasForeignKey(d => d.SpannerCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Approval_Spanner");
            });

            modelBuilder.Entity<ApprovalDetail>(entity =>
            {
                entity.Property(e => e.ApprovalCategory)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ApprovalRating)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentName);

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<DestinationType>(entity =>
            {
                entity.HasKey(e => e.DestinationTypeName);

                entity.Property(e => e.DestinationTypeName)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<DistanceType>(entity =>
            {
                entity.HasKey(e => e.DistanceTypeName);

                entity.Property(e => e.DistanceTypeName)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.LowerOperator).HasMaxLength(2);

                entity.Property(e => e.UpperOperator).HasMaxLength(2);
            });

            modelBuilder.Entity<EligibilityType>(entity =>
            {
                entity.HasKey(e => e.EligibilityTypeName);

                entity.Property(e => e.EligibilityTypeName)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<FlightCompany>(entity =>
            {
                entity.HasKey(e => e.FlightCompanyName);

                entity.Property(e => e.FlightCompanyName)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<FlightData>(entity =>
            {
                entity.Property(e => e.AirportDestination)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.AirportOrigin)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.DefaultSectorTime).HasColumnType("time(0)");

                entity.HasOne(d => d.AirportDestinationNavigation)
                    .WithMany(p => p.FlightDataAirportDestinationNavigation)
                    .HasForeignKey(d => d.AirportDestination)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightData_Airport_Destination");

                entity.HasOne(d => d.AirportOriginNavigation)
                    .WithMany(p => p.FlightDataAirportOriginNavigation)
                    .HasForeignKey(d => d.AirportOrigin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightData_Airport_Origin");
            });

            modelBuilder.Entity<FlightRecord>(entity =>
            {
                entity.HasKey(e => e.FlightRecordId);

                entity.Property(e => e.DestinationTypeName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EligibilityTypeName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.FlightCompanyName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.FlightDate).HasColumnType("datetime");

                entity.Property(e => e.FlightTime).HasColumnType("time(0)");

                entity.Property(e => e.OriginTypeName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RecordDate).HasColumnType("date");

                entity.Property(e => e.RecordTime).HasColumnType("time(0)");

                entity.HasOne(d => d.AircraftType)
                    .WithMany(p => p.FlightRecord)
                    .HasForeignKey(d => d.AircraftTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightRecord_AircraftType");

                entity.HasOne(d => d.Approval)
                    .WithMany(p => p.FlightRecord)
                    .HasForeignKey(d => d.ApprovalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightRecord_Approval");

                entity.HasOne(d => d.DestinationTypeNameNavigation)
                    .WithMany(p => p.FlightRecord)
                    .HasForeignKey(d => d.DestinationTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightRecord_DestinationType");

                entity.HasOne(d => d.EligibilityTypeNameNavigation)
                    .WithMany(p => p.FlightRecord)
                    .HasForeignKey(d => d.EligibilityTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightRecord_EligibilityType");

                entity.HasOne(d => d.FlightCompanyNameNavigation)
                    .WithMany(p => p.FlightRecord)
                    .HasForeignKey(d => d.FlightCompanyName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightRecord_FlightCompany");

                entity.HasOne(d => d.FlightData)
                    .WithMany(p => p.FlightRecord)
                    .HasForeignKey(d => d.FlightDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightRecord_FlightData");

                entity.HasOne(d => d.OrganizerGroup)
                    .WithMany(p => p.FlightRecord)
                    .HasForeignKey(d => d.OrganizerGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightRecord_OrganizerGroup");

                entity.HasOne(d => d.OriginTypeNameNavigation)
                    .WithMany(p => p.FlightRecord)
                    .HasForeignKey(d => d.OriginTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightRecord_OriginType");
            });

            modelBuilder.Entity<GroupOfSpanners>(entity =>
            {
                entity.HasKey(e => e.GroupName);

                entity.Property(e => e.GroupName)
                    .HasMaxLength(25)
                    .ValueGeneratedNever();

                entity.Property(e => e.RecordStarting).HasColumnType("date");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.Property(e => e.HolidayDay).HasMaxLength(2);

                entity.Property(e => e.HolidayMonth).HasMaxLength(2);

                entity.Property(e => e.HolidayName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.HolidayTypeName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.HolidayYear).HasMaxLength(4);

                entity.HasOne(d => d.HolidayTypeNameNavigation)
                    .WithMany(p => p.Holiday)
                    .HasForeignKey(d => d.HolidayTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Holiday_HolidayType");
            });

            modelBuilder.Entity<HolidayType>(entity =>
            {
                entity.HasKey(e => e.HolidayTypeName);

                entity.Property(e => e.HolidayTypeName)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<InActivePeriod>(entity =>
            {
                entity.Property(e => e.InActiveFromDate).HasColumnType("date");

                entity.Property(e => e.InActiveToDate).HasColumnType("date");

                entity.Property(e => e.SpannerCode).HasMaxLength(5);

                entity.HasOne(d => d.SpannerCodeNavigation)
                    .WithMany(p => p.InActivePeriod)
                    .HasForeignKey(d => d.SpannerCode)
                    .HasConstraintName("FK_InActivePeriod_Spanner");
            });

            modelBuilder.Entity<Organizer>(entity =>
            {
                entity.HasKey(e => e.OrganizerCode);

                entity.Property(e => e.OrganizerCode)
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.OrganizerEmail)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.OrganizerFName)
                    .IsRequired()
                    .HasColumnName("OrganizerFName")
                    .HasMaxLength(20);

                entity.Property(e => e.OrganizerLName)
                    .IsRequired()
                    .HasColumnName("OrganizerLName")
                    .HasMaxLength(20);

                entity.Property(e => e.OrganizerM1Name)
                    .IsRequired()
                    .HasColumnName("OrganizerM1Name")
                    .HasMaxLength(20);

                entity.Property(e => e.OrganizerM2Name)
                    .IsRequired()
                    .HasColumnName("OrganizerM2Name")
                    .HasMaxLength(20);

                entity.Property(e => e.OrganizerMobile1)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.OrganizerMobile2).HasMaxLength(11);

                entity.Property(e => e.OrganizerOccupation).HasMaxLength(11);

                entity.Property(e => e.OrganizerPassword)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<OrganizerGroup>(entity =>
            {
                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.OrganizerCode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.HasOne(d => d.GroupNameNavigation)
                    .WithMany(p => p.OrganizerGroup)
                    .HasForeignKey(d => d.GroupName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrganizerGroup_Group");

                entity.HasOne(d => d.OrganizerCodeNavigation)
                    .WithMany(p => p.OrganizerGroup)
                    .HasForeignKey(d => d.OrganizerCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrganizerGroup_Organizer");
            });

            modelBuilder.Entity<OriginType>(entity =>
            {
                entity.HasKey(e => e.OriginTypeName);

                entity.Property(e => e.OriginTypeName)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.Property(e => e.QualificationDegree)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.QualificationMajor)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.QualificationName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Spanner>(entity =>
            {
                entity.HasKey(e => e.SpannerCode);

                entity.Property(e => e.SpannerCode)
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.SpannerBirthday).HasColumnType("date");

                entity.Property(e => e.SpannerEmail).HasMaxLength(30);

                entity.Property(e => e.SpannerFName)
                    .IsRequired()
                    .HasColumnName("SpannerFName")
                    .HasMaxLength(20);

                entity.Property(e => e.SpannerGender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.SpannerHireDate).HasColumnType("date");

                entity.Property(e => e.SpannerLicenseNo)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.SpannerLName)
                    .IsRequired()
                    .HasColumnName("SpannerLName")
                    .HasMaxLength(20);

                entity.Property(e => e.SpannerM1Name)
                    .IsRequired()
                    .HasColumnName("SpannerM1Name")
                    .HasMaxLength(20);

                entity.Property(e => e.SpannerM2Name)
                    .IsRequired()
                    .HasColumnName("SpannerM2Name")
                    .HasMaxLength(20);

                entity.Property(e => e.SpannerMobile1).HasMaxLength(11);

                entity.Property(e => e.SpannerMobile2).HasMaxLength(11);

                entity.Property(e => e.SpannerPassword)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.DepartmentNameNavigation)
                    .WithMany(p => p.Spanner)
                    .HasForeignKey(d => d.DepartmentName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spanner_Department1");

                entity.HasOne(d => d.GroupNameNavigation)
                    .WithMany(p => p.Spanner)
                    .HasForeignKey(d => d.GroupName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spanner_Group");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.Spanner)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spanner_Qualification");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminCode);

                entity.Property(e => e.AdminCode).ValueGeneratedNever();

                entity.Property(e => e.AdminEmail)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.AdminFName)
                    .IsRequired()
                    .HasColumnName("AdminFName")
                    .HasMaxLength(50);

                entity.Property(e => e.AdminLName)
                    .IsRequired()
                    .HasColumnName("AdminLName")
                    .HasMaxLength(50);

                entity.Property(e => e.AdminM1Name)
                    .IsRequired()
                    .HasColumnName("AdminM1Name")
                    .HasMaxLength(50);

                entity.Property(e => e.AdminM2Name)
                    .IsRequired()
                    .HasColumnName("AdminM2Name")
                    .HasMaxLength(50);

                entity.Property(e => e.AdminMobile1)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.AdminMobile2).HasMaxLength(11);

                entity.Property(e => e.AdminOccupation).HasMaxLength(50);

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasMaxLength(100);
            });

        }
    }
}
