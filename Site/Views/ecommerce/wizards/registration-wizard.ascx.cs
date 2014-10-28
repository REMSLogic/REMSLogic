using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lib.Data;
using Lib.Systems;
using RemsLogic.Model;
using RemsLogic.Repositories;
using StructureMap;
using Address = Lib.Data.Address;
using Prescriber = Lib.Data.Prescriber;
using User = Framework.Security.User;

namespace Site.Views.ecommerce.wizards
{
    public partial class registration_wizard : System.Web.UI.UserControl
    {
#region Member Variables
        private readonly IOrganizationRepository _orgRepo;
        #endregion

        #region Properties
        public PrescriberProfile PrescriberProfile {get; set;}
        public Prescriber Prescriber {get; set;}
        public Address Address {get; set;}
        public Contact Contact {get; set;}

        //public ProviderFacility Facility {get; set;}
        public IList<PrescriberType> PrescriberTypes {get; set;}
        public IList<Speciality> Specialities {get; set;}
        public IList<State> States {get; set;}
        
        public IEnumerable<Facility> Facilities {get; set;}
        #endregion

        #region Constructors
        public registration_wizard()
        {
            _orgRepo = ObjectFactory.GetInstance<IOrganizationRepository>();
        }
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];

            Prescriber prescriber = Security.GetCurrentPrescriber();
            PrescriberProfile profile = PrescriberProfile.FindByPrescriber(prescriber).First();

            PrescriberProfile = profile;
            Prescriber = prescriber;
            Address = profile.Address;
            Contact = profile.Contact;
            PrescriberTypes = PrescriberType.FindAll();
            Specialities = Speciality.FindAll();
            States = State.FindAll();
        }
        #endregion
    }
}