using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib.Data;

namespace Site.App.Views.provider.prescribers
{
	public partial class list : Lib.Web.AppControlPage
    {
        #region Member Variables
        private ProviderUser _providerUser;
        private Provider _provider;
        #endregion

        #region Properties
        public IList<Prescriber> Prescribers {get; set;}

        public string DistributionLists {get; set;}
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            _providerUser = ProviderUser.FindByProfile(UserProfile.FindByUser(Framework.Security.Manager.GetUser()));
            _provider = Provider.FindByUser(_providerUser);

            Prescribers = _provider.GetPrescribers();

            BuildDistributionListArray(DistributionList.FindByUserProfile(UserProfile.FindByUser(Framework.Security.Manager.GetUser())));
        }

        #region Utility Methods
        public string GetPrescriberTypes()
        {
            IList<PrescriberType> types = (from pt in PrescriberType.FindAll()
                                           orderby pt.DisplayName
                                           select pt).ToList();

            StringBuilder sb = new StringBuilder();

            sb.Append("{'Any':'',");
            foreach(PrescriberType pt in types)
            {
                sb.Append("'");
                sb.Append(pt.DisplayName);
                sb.Append("':'");
                sb.Append(pt.DisplayName);
                sb.Append("'");

                if(pt != types.Last())
                    sb.Append(",");
            }
            sb.Append("}");

            return sb.ToString();
        }

        public string GetPrescriberSpecialties()
        {
            string options =  (
                from s in Speciality.FindAll()
                orderby s.Name
                select s)
                .Select(x => String.Format("'{0}':'{1}'", x.Name, x.Name))
                .Aggregate((c,n) => String.Format("{0},{1}", c, n));

            return String.Format("{{'Any':'',{0}}}",options);
        }

        public PrescriberProfile GetPrescriberProfile(Prescriber prescriber)
        {
            return PrescriberProfile.FindByOrganization(_providerUser.OrganizationID, prescriber.ID ?? 0);
        }

        public string GetPrescriberType(PrescriberProfile profile)
        {
            return (profile != null && profile.PrescriberTypeID != null)
                ? profile.PrescriberType.DisplayName
                : String.Empty;
        }

        public string GetPrescriberSpecialty(Prescriber prescriber)
        {
            return (prescriber != null && prescriber.Speciality != null)
                ? prescriber.Speciality.Name
                : String.Empty;
        }

        public string GetPrescriberFacilityName(PrescriberProfile profile)
        {
            return (profile != null && profile.Facility != null)
                ? profile.Facility.Name
                : String.Empty;
        }

        private void BuildDistributionListArray(IList<DistributionList> lists)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            foreach(DistributionList list in lists)
            {
                sb.Append("{");
                sb.Append("\"label\":\""+list.Name+"\",");
                sb.Append("\"value\":"+list.ID);
                sb.Append("}");

                if(list != lists.Last())
                    sb.Append(",");
            }
            sb.Append("]");

            DistributionLists = sb.ToString();
        }
        #endregion
    }
}