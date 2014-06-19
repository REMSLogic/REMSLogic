using System;
using FluentMigrator;

namespace RemsLogic.Migrations.Sprint12
{
    [Migration(201405220835)]
    public class Migration002AddNewEocs : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("Eocs")
                .Row(new {
                    Name = "nursing-education", 
                    DisplayName = "Nursing Education", 
                    Roles = "view_prescriber", 
                    ShortDisplayName = "Nursing Education", 
                    LargeIcon = "/images/icons/EOC-Nursing_Education.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 7
                });

            Insert.IntoTable("Eocs")
                .Row(new {
                    Name = "medication-guide", 
                    DisplayName = "Medication Guide", 
                    Roles = "view_provider", 
                    ShortDisplayName = "Medication Guide", 
                    LargeIcon = "/images/icons/MG.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 8
                });

            Insert.IntoTable("Eocs")
                .Row(new {
                    Name = "forms-document", 
                    DisplayName = "Forms and Documents", 
                    Roles = "view_prescriber|view_provider", 
                    ShortDisplayName = "Forms Documents", 
                    LargeIcon = "/images/icons/FD.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 10
                });

            Insert.IntoTable("Eocs")
                .Row(new {
                    Name = "patient-education", 
                    DisplayName = "Patient Education", 
                    Roles = "view_prescriber", 
                    ShortDisplayName = "Patient Education", 
                    LargeIcon = "/images/icons/EOC-Patient_Education.png",
                    SmallIcon = "small icon",
                    DisplayOrder = 11
                });
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
