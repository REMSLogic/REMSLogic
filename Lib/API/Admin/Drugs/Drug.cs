using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Drugs
{
	public class Drug : Base
	{
		/*[SecurityRole("view_admin")]
		[Method("Admin/Drugs/Drug/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, string generic_name, string brand_name, string formulation, long company_id, long class_id, bool inpatient_requirements = false, bool outpatient_requirements = false, long? system_id = null, bool new_version = false, string message = null, DateTime? rems_updated = null, string fda_number = null, DateTime? rems_approved = null, bool active=false)
		{
			Lib.Data.Drug item = null;
			if (id > 0)
				item = new Lib.Data.Drug(id);
			else
				item = new Lib.Data.Drug();

			if( new_version && string.IsNullOrEmpty(message) )
			{
				return new ReturnObject() { Error = true, Message = "You must enter a message when you save a new version." };
			}

			var u = Framework.Security.Manager.GetUser();
			
			item.GenericName = generic_name;
			item.BrandName = brand_name;
			item.Formulation = formulation;
			item.CompanyID = company_id;
            item.ClassID = class_id;
			item.SystemID = system_id;
			item.InpatientRequirements = inpatient_requirements;
			item.OutpatientRequirements = outpatient_requirements;
			item.FdaApplicationNumber = fda_number;
			item.RemsApproved = rems_approved;
			item.RemsUpdated = rems_updated;
			item.UpdatedByID = u.ID.Value;
			item.Updated = DateTime.Now;
			item.Active = active;
			item.Save();
			
			if( id <= 0 )
			{
				new_version = true;
				message = "New Drug Created.";
			}

			if (new_version)
			{
				var last_version = Data.DrugVersion.FindLatestByDrug(item);

				var ver = new Data.DrugVersion();
				ver.DrugID = item.ID.Value;
				ver.UpdatedBy = u.ID.Value;
				ver.Message = message;
				ver.Updated = DateTime.Now;
				ver.Version = ((last_version == null) ? 1 : last_version.Version + 1);
				ver.Status = (approved ? "Approved" : "Pending");
				ver.Save();
			}

			return new ReturnObject()
			{
				Result = item,
				Redirect = new ReturnRedirectObject()
				{
					Hash = "admin/drugs/drugs/edit?id="+item.ID.Value
				},
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully saved this drug.",
						title = "Drug Saved"
					}
				}
			};
		}*/

		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/Drug/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Drug." };

			var item = new Lib.Data.Drug(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a drug.",
						title = "Drug Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = ".admin-drugs-list tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
