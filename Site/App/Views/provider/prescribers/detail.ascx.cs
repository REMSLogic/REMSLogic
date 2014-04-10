using System;
using System.Collections.Generic;
using Lib.Data;

namespace Site.App.Views.provider.prescribers
{
    public partial class detail : Lib.Web.AppControlPage
    {
        public Prescriber Prescriber;
        public PrescriberProfile PrescriberProfile;
        public Provider Provider;
        public IList<Drug> Drugs;
        public IList<ProviderFacility> ProviderFacilities;
        public IList<ProviderFacility> PrescriberFacilities;
        public IList<State> States {get; set;}
        public IList<Speciality> Specialities {get; set;}
        public long SpecialityId {get; set;}
        public IList<PrescriberType> PrescriberTypes {get; set;}
        public long TypeId {get; set;}

        protected void Page_Init(object sender, EventArgs e)
        {
            RequireRole( "view_provider" );

            string strID = Request.QueryString["id"];
            long id;
            if( string.IsNullOrEmpty( strID ) || !long.TryParse( strID, out id ) )
                RedirectHash( "provider/prescribers/list", true, "Invalid Prescriber" );
            else
                Prescriber = new Prescriber( id );

            Provider = Lib.Systems.Security.GetCurrentProvider();
            PrescriberProfile = PrescriberProfile.FindByPrescriberAndProvider(Prescriber, Provider);
            ProviderFacilities = ProviderFacility.FindByProvider(Provider);
            States = State.FindAll();
            Specialities = Speciality.FindAll();
            SpecialityId = Prescriber.SpecialityID ?? 0;
            PrescriberTypes = PrescriberType.FindAll();
            TypeId = PrescriberProfile.PrescriberTypeID ?? 0;

            if(PrescriberProfile != null)
            {
                PrescriberFacilities = PrescriberProfile.GetFacilities();
                Drugs = Lib.Systems.Lists.GetUsersDrugs(PrescriberProfile.ID ?? 0);
            }
            else
                RedirectHash( "provider/prescribers/list", true, "Invalid Prescriber" );
        }
    }
}