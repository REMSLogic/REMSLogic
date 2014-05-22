using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint12
{
    [Migration(201405220456)]
    public class Migration001AddColumnsToEocTable : Migration
    {
        public override void Up()
        {
            Alter.Table("Eocs")
                .AddColumn("ShortDisplayName").AsString(32).NotNullable().WithDefaultValue("eoc")
                .AddColumn("LargeIcon").AsString(256).NotNullable().WithDefaultValue("large-icon")
                .AddColumn("SmallIcon").AsString(256).NotNullable().WithDefaultValue("small-icon")
                .AddColumn("DisplayOrder").AsInt32().NotNullable().WithDefaultValue(0);

            Update.Table("Eocs")
                .Set(new {
                    Name = "facility-pharmacy-enrollment", 
                    DisplayName = "Facility Enrollment", 
                    Roles = "view_provider", 
                    ShortDisplayName = "Facility Enrollment", 
                    LargeIcon = "/App/images/icons/FP EN.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 2
                }).Where(new {Id = 1});

            Update.Table("Eocs")
                .Set(new {
                    Name = "patient-enrollment", 
                    DisplayName = "Patient Enrollment", 
                    Roles = "view_prescriber", 
                    ShortDisplayName = "Patient Enrollment", 
                    LargeIcon = "/App/images/icons/PAEN.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 3
                }).Where(new {Id = 2});

            Update.Table("Eocs")
                .Set(new {
                    Name = "prescriber-enrollment", 
                    DisplayName = "Prescriber Enrollment", 
                    Roles = "view_prescriber", 
                    ShortDisplayName = "Prescriber Enrollment", 
                    LargeIcon = "/App/images/icons/PREN.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 4
                }).Where(new {Id = 3});

            Update.Table("Eocs")
                .Set(new {
                    Name = "education-training", 
                    DisplayName = "Prescriber Eduction", 
                    Roles = "view_prescriber", 
                    ShortDisplayName = "Prescriber Eduction", 
                    LargeIcon = "/App/images/icons/EDUCRT.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 5
                }).Where(new {Id = 4});

            Update.Table("Eocs")
                .Set(new {
                    Name = "monitoring-management", 
                    DisplayName = "Monitoring", 
                    Roles = "view_prescriber", 
                    ShortDisplayName = "Monitoring", 
                    LargeIcon = "/App/images/icons/MON.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 6
                }).Where(new {Id = 5});

            Update.Table("Eocs")
                .Set(new {
                    Name = "informed-consent", 
                    DisplayName = "Informed Consent", 
                    Roles = "view_prescriber", 
                    ShortDisplayName = "Informed Consent", 
                    LargeIcon = "/App/images/icons/IC.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 9
                }).Where(new {Id = 6});

            Update.Table("Eocs")
                .Set(new {
                    Name = "pharmacy-requirements", 
                    DisplayName = "Pharmacy Requirements", 
                    Roles = "view_provider", 
                    ShortDisplayName = "Pharmacy Requirements", 
                    LargeIcon = "/App/images/icons/PR.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 12
                }).Where(new {Id = 7});

            Update.Table("Eocs")
                .Set(new {
                    Name = "etasu", 
                    DisplayName = "ETASU", 
                    Roles = "view_prescriber", 
                    ShortDisplayName = "ETASU", 
                    LargeIcon = "/App/images/icons/ETASU.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 1
                }).Where(new {Id = 8});

            /*
            Update.Table("Eocs")
                .Set(new {
                    Name = "nursing-education", 
                    DisplayName = "Nursing Education", 
                    Roles = "view_prescriber", 
                    ShortDisplayName = "Nursing Education", 
                    LargeIcon = "/App/images/icons/EOC-Nursing_Education.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 7
                }).Where(new {Id = 9});

            Update.Table("Eocs")
                .Set(new {
                    Name = "medication-guide", 
                    DisplayName = "Medication Guide", 
                    Roles = "view_provider", 
                    ShortDisplayName = "Medication Guide", 
                    LargeIcon = "/App/images/icons/MG.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 8
                }).Where(new {Id = 10});

            Update.Table("Eocs")
                .Set(new {
                    Name = "forms-document", 
                    DisplayName = "Forms and Documents", 
                    Roles = "view_prescriber|view_provider", 
                    ShortDisplayName = "Forms Documents", 
                    LargeIcon = "/App/images/icons/FD.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 10
                }).Where(new {Id = 11});

            Update.Table("Eocs")
                .Set(new {
                    Name = "patient-education", 
                    DisplayName = "Patient Education", 
                    Roles = "view_prescriber", 
                    ShortDisplayName = "Patient Education", 
                    LargeIcon = "/App/images/icons/EOC-Patient_Education.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 11
                }).Where(new {Id = 12});
            */
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}

