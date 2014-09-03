﻿using System;
using System.Collections.Generic;
using Lib.Data;
using RemsLogic.Model.Ecommerce;
using RemsLogic.Services;
using StructureMap;

namespace Site.Views.admin.security.ecommerce
{
    public partial class list : Lib.Web.AppControlPage
    {
        #region Member Variables
        private IAccountService _accountSvc;
        #endregion

        #region Properties
        public IList<ProviderUser> ProviderUsers {get; set;}
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            _accountSvc = ObjectFactory.GetInstance<IAccountService>();

            ProviderUsers = ProviderUser.FindByClass(ProviderUser.ProviderClass.Ecommerce);
        }

        protected Account GetProviderUserAccount(ProviderUser providerUser)
        {
            return _accountSvc.GetByProviderUserId(providerUser.ID ?? 0);
        }
    }
}