using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Drugs
{
	public class DrugFormulation : Base
	{
		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/DrugFormulation/Edit")]
		public static ReturnObject Edit(HttpContext context, long drug_id, string formulation, long drug_company_id, long? id = null, string brand_name = "", string drug_company_url = "", string drug_company_email = "", string drug_company_phone = "", string drug_company_fax = "")
		{
			Lib.Data.DrugFormulation item = null;
			if (id > 0)
				item = new Lib.Data.DrugFormulation(id);
			else
				item = new Lib.Data.DrugFormulation();

			var formulations = Lib.Data.Formulation.FindAll();
			long formulation_id = 0;

			foreach (var f in formulations)
			{
				if (f.Name.ToLower() == formulation.ToLower())
				{
					formulation_id = f.ID.Value;
					break;
				}
			}

			if (formulation_id == 0)
			{
				return new ReturnObject()
				{
					Result = null,
					Growl = new ReturnGrowlObject()
					{
						Type = "default",
						Vars = new ReturnGrowlVarsObject()
						{
							text = "You must select an existing drug formulation.",
							title = "Drug Formulation Error"
						}
					}
				};
			}

			item.DrugID = drug_id;
			item.FormulationID = formulation_id;
			item.BrandName = brand_name;
			item.DrugCompanyID = drug_company_id;
			item.DrugCompanyURL = drug_company_url;
			item.DrugCompanyEmail = drug_company_email;
			item.DrugCompanyPhone = drug_company_phone;
			item.DrugCompanyFax = drug_company_fax;
			item.Save();

			return new ReturnObject()
			{
				Result = item,
				Redirect = new ReturnRedirectObject()
				{
					Hash = "admin/dsq/edit?id="+drug_id.ToString()
				},
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully saved this drug formulation.",
						title = "Drug Formulation Saved"
					}
				}
			};
		}

		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/DrugFormulation/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Drug System." };

			var item = new Lib.Data.DrugFormulation(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a drug formulation.",
						title = "Drug Formulation Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#formulations-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
