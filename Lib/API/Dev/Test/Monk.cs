using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Dev.Test
{
	public class Monk : Base
	{
		[SecurityRole( "view_dev" )]
		[Method( "Dev/Test/Monk/UpdateDrugLists" )]
		public static ReturnObject UpdateDrugLists( HttpContext context )
		{
			Lib.Systems.Lists.UpdateDrugLists();

			return new ReturnObject() {
				Result = null,
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "Lists Updated.",
						title = "Mmmmmm Bananas..."
					}
				}
			};
		}

		[SecurityRole("view_dev")]
		[Method("Dev/Test/Monk/IndexPrescribers")]
		public static ReturnObject IndexPrescribers(HttpContext context)
		{
			var items = Lib.Data.Prescriber.FindAll();

			foreach (var item in items)
			{
				Lib.Search.Manager.UpdateDocument(item);
			}

			return new ReturnObject()
			{
				Result = null,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "Indexed Prescribers.",
						title = "Monk wants Bananas"
					}
				}
			};
		}

		[SecurityRole("view_dev")]
		[Method("Dev/Test/Monk/IndexProviders")]
		public static ReturnObject IndexProviders(HttpContext context)
		{
			var items = Lib.Data.Provider.FindAll();

			foreach (var item in items)
			{
				Lib.Search.Manager.UpdateDocument(item);
			}

			return new ReturnObject()
			{
				Result = null,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "Indexed Providers.",
						title = "Monk wants Bananas"
					}
				}
			};
		}

		[SecurityRole("view_dev")]
		[Method("Dev/Test/Monk/IndexDrugCompanies")]
		public static ReturnObject IndexDrugCompanies(HttpContext context)
		{
			var items = Lib.Data.DrugCompany.FindAll();

			foreach (var item in items)
			{
				Lib.Search.Manager.UpdateDocument(item);
			}

			return new ReturnObject()
			{
				Result = null,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "Indexed Drug Companies.",
						title = "Monk wants Bananas"
					}
				}
			};
		}

		[SecurityRole("view_dev")]
		[Method("Dev/Test/Monk/IndexDrugSystems")]
		public static ReturnObject IndexDrugSystems(HttpContext context)
		{
			var items = Lib.Data.DrugSystem.FindAll();

			foreach (var item in items)
			{
				Lib.Search.Manager.UpdateDocument(item);
			}

			return new ReturnObject()
			{
				Result = null,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "Indexed Drug Systems.",
						title = "Monk wants Bananas"
					}
				}
			};
		}

		[SecurityRole("view_dev")]
		[Method("Dev/Test/Monk/IndexDrugs")]
		public static ReturnObject IndexDrugs(HttpContext context)
		{
			var items = Lib.Data.Drug.FindAll(true);

			foreach (var item in items)
			{
				Lib.Search.Manager.UpdateDocument(item);
			}

			return new ReturnObject()
			{
				Result = null,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "Indexed Drugs.",
						title = "Monk wants Bananas"
					}
				}
			};
		}

		[SecurityRole("view_dev")]
		[Method("Dev/Test/Monk/IndexUserProfiles")]
		public static ReturnObject IndexUserProfiles(HttpContext context)
		{
			var uts = new List<Lib.Data.UserType>();
			uts.Add(Lib.Data.UserType.FindByName("admin"));
			uts.Add(Lib.Data.UserType.FindByName("dev"));

			var items = Lib.Data.UserProfile.FindByUserTypes(uts);

			foreach (var item in items)
			{
				Lib.Search.Manager.UpdateDocument(item);
			}

			return new ReturnObject()
			{
				Result = null,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "Indexed User Profiles.",
						title = "Monk wants Bananas"
					}
				}
			};
		}

		[SecurityRole("view_dev")]
		[Method("Dev/Test/Monk/ClearIndexes")]
		public static ReturnObject ClearIndexes(HttpContext context)
		{
			Lib.Search.Manager.RemoveDocuments();

			return new ReturnObject()
			{
				Result = null,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "Indexes Cleared.",
						title = "Monk wants Bananas"
					}
				}
			};
		}

		[SecurityRole("view_dev")]
		[Method("Dev/Test/Monk/RefreshDrugIcons")]
		public static ReturnObject RefreshDrugIcons(HttpContext context)
		{
			var drugs = Lib.Data.Drug.FindAll(false);

			foreach (var drug in drugs)
			{
				drug.DetermineEOC();
			}

			return new ReturnObject()
			{
				Result = null,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "Drugs Refreshed.",
						title = "Yay Bananas"
					}
				}
			};
		}
	}
}
