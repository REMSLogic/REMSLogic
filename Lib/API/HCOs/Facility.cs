using System.Collections.Generic;
using System.Web;
using Framework.API;
using Lib.Data;
using RemsLogic.Services;
using StructureMap;

namespace Lib.API.HCOs
{
	public class Facility : Base
	{
        [Method("HCOs/Facility/Edit")]
        public static ReturnObject Edit(HttpContext context, long provider_id, string facility_name, string facility_size, string street1, string city, string state, string zip, string country, long id, string street2 = null)
        {
            IOrganizationService orgSvc = ObjectFactory.GetInstance<IOrganizationService>();

            RemsLogic.Model.Facility facility = orgSvc.GetFacility(id) ??
                new RemsLogic.Model.Facility
                {
                    OrganizationId = provider_id,
                    Address = new RemsLogic.Model.Address()
                };

            facility.Name = facility_name;
            facility.BedSize = facility_size;

            facility.Address.Street1 = street1;
            facility.Address.Street2 = street2;
            facility.Address.City = city;
            facility.Address.State = state;
            facility.Address.Zip = zip;
            facility.Address.Country = country;

            orgSvc.SaveFacility(facility);

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
