using System;
using System.Collections.Generic;
using Lib.Data;
using Lib.Systems.Tasks;

namespace Site.App.Views.admin.tasks
{
    public partial class list : Lib.Web.AppControlPage
    {
        #region Properties
        public IList<Task> Tasks {get; set;}
        #endregion

        #region Page Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            Tasks = TaskService.GetPending();
        }
        #endregion
    }
}