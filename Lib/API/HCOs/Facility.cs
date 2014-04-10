using System;
using System.Collections.Generic;
using System.Web;
using Framework.API;
using Lib.Data;
using Lib.Systems;

namespace Lib.API.HCOs
{
	public class Facility : Base
	{
		[Method("HCOs/Facility/Edit")]
		public static ReturnObject Edit(HttpContext context, long provider_id, string facility_name, string street1, string city, string state, string zip, long? id = null, string street2 = null)
		{
			Data.ProviderFacility facility = null;
			Address address = null;

			if (id > 0)
			{
				facility = new Data.ProviderFacility(id);
				address = facility.PrimaryAddress;
			}
			else
			{
				facility = new Data.ProviderFacility();
			}

			if (address == null)
				address = new Address();

			address.Street1 = street1;
			address.Street2 = street2;
			address.City = city;
			address.State = state;
			address.Zip = zip;
			address.Country = "United States";
			address.Save();

			facility.ProviderID = provider_id;
			facility.Name = facility_name;
			facility.AddressID = address.ID.Value;
			facility.Save();

			return new ReturnObject()
			{
				Result = facility,
				Actions = new List<ReturnActionObject> {
					new ReturnActionObject {
						Type = "back"
					}
				},
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully saved this facility.",
						title = "Facility Saved"
					}
				}
			};
		}

		[Method("HCOs/Facility/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Facility." };

			var item = new Lib.Data.ProviderFacility(id);
			item.Deleted = true;
			item.Save();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted this facility.",
						title = "Facility deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#facilities-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
