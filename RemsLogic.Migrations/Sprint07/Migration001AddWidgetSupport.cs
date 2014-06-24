using FluentMigrator;

namespace RemsLogic.Migrations.Sprint07
{
    [Migration(201404110039)]
    public class Migration001AddWidgetSupport : Migration
    {
        public override void Up()
        {
            Create.Table("Widgets")
                .WithColumn("Id").AsInt64()
                    .PrimaryKey()
                    .Identity()
                    .NotNullable()
                .WithColumn("Name").AsString()
                    .NotNullable()
                .WithColumn("Location").AsString()
                    .NotNullable()
                .WithColumn("Roles").AsString()
                    .NotNullable();

            Insert.IntoTable("Widgets")
                .Row(new{Name="Account Statistics", Location="~/Controls/Widgets/AccountStatistics.ascx", Roles="view_admin"})
                .Row(new{Name="Compliance Graph", Location="~/Controls/Widgets/ComplianceGraph.ascx", Roles="view_provider"})
                .Row(new{Name="Compliance Status", Location="~/Controls/Widgets/ComplianceStatus.ascx", Roles="view_prescriber"})
                .Row(new{Name="Facility Drug List", Location="~/Controls/Widgets/FacilityDrugList.ascx", Roles="view_provider"})
                .Row(new{Name="My Drug List", Location="~/Controls/Widgets/MyDrugList.ascx", Roles="view_prescriber"})
                .Row(new{Name="My Drugs", Location="~/Controls/Widgets/MyDrugs.ascx", Roles="dashboard_drugcompany_view"})
                .Row(new{Name="Organization Summary", Location="~/Controls/Widgets/OrganizationSummary.ascx", Roles="view_provider"})
                .Row(new{Name="Pending Drug Changes", Location="~/Controls/Widgets/PendingDrugChanges.ascx", Roles="view_admin"})
                .Row(new{Name="Prescriber Updates", Location="~/Controls/Widgets/PrescriberUpdates.ascx", Roles="view_provider"})
                .Row(new{Name="Quick Links", Location="~/Controls/Widgets/QuickLinks.ascx", Roles="view_prescriber"})
                .Row(new{Name="Reports", Location="~/Controls/Widgets/Reports.ascx", Roles="view_admin|view_prescriber|view_provider|dashboard_drugcompany_view"})
                .Row(new{Name="User Activity Graph", Location="~/Controls/Widgets/UserActivityGraph.ascx", Roles="view_admin|view_provider"});
        }

        public override void Down()
        {
            Delete.Table("Widgets");    
        }
    }
}
