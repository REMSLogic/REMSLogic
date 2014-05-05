﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RemsLogic.Model;
using RemsLogic.Repositories;
using RemsLogic.Utilities;
using StructureMap;

namespace Lib.Web.Controls
{
	[ToolboxData("<{0}:DSQView runat=\"server\"></{0}:DSQView>")]
	public class DSQView : WebControl
	{
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string GeneralInfoControlPath
		{
			get
			{
				String s = (String)ViewState["GeneralInfoControlPath"];
				return ((s == null) ? "~/IgnoreMe.ascx" : s);
			}

			set
			{
				ViewState["GeneralInfoControlPath"] = value;
			}
		}

		[Bindable(true)]
		[Category("Data")]
		[DefaultValue("")]
		[Localizable(true)]
		public long? DrugID
		{
			get
			{
				var s = (string)ViewState["DrugID"];
				long id;
				if (!long.TryParse(s, out id))
					id = -1;

				return ((id == -1) ? null : (long?)id);
			}

			set
			{
				ViewState["DrugID"] = (value.HasValue) ? value.Value.ToString() : null;
			}
		}

		protected Lib.Data.Drug Drug = null;

		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (DrugID == null)
			{
				long id;
				if( long.TryParse(Page.Request["drugid"], out id) || 
					long.TryParse(Page.Request["drug_id"], out id) || 
					long.TryParse(Page.Request["drug-id"], out id) ||
					long.TryParse(Page.Request["id"], out id) )
					DrugID = id;
			}

			Drug = new Data.Drug( DrugID.Value );

			writer.AddAttribute("class", "dsq-accordion");
			writer.RenderBeginTag("section");

			foreach (var s in Lib.Data.DSQ.Section.FindAll())
				RenderSection(writer, s);

			writer.RenderEndTag();
		}

		protected void RenderSection(HtmlTextWriter writer, Lib.Data.DSQ.Section s)
		{
			var user = Framework.Security.Manager.GetUser();
			if (user != null)
			{
				var profile = Lib.Data.UserProfile.FindByUser(user);
				if (profile != null && profile.UserTypeID == Lib.Data.UserType.FindByName("prescriber").ID && s.Name == "Pharmacy Requirements")
					return;
			}

			// Header
			writer.RenderBeginTag("header");
			writer.RenderBeginTag("h2");
			writer.WriteEncodedText(s.Name);
			writer.RenderEndTag();
			writer.RenderEndTag();

			// Body
			writer.AddAttribute("class", "clearfix");
			writer.RenderBeginTag("section");

			writer.AddAttribute("class", "form");
			writer.RenderBeginTag("div");

			if (s.ID.Value == 1)
			{
				var control = Page.LoadControl(this.GeneralInfoControlPath);
				control.RenderControl(writer);
			}
			else if (DrugID.HasValue)
			{
				foreach (var q in Lib.Data.DSQ.Question.FindBySection(s, false))
					RenderQuestion(writer, q);
			}
			else
			{
				writer.AddStyleAttribute("text-align", "center");
				writer.AddStyleAttribute("padding", "1em");
				writer.RenderBeginTag("div");

				writer.WriteEncodedText("Please save the drug before continuing.");

				writer.RenderEndTag();
			}

			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		protected void RenderQuestion(HtmlTextWriter writer, Lib.Data.DSQ.Question q)
		{
			if (q==null || !q.ID.HasValue)
				return;

            IMarkdownService markdownSvc = new MarkdownService(enableSyntaxHighlighting: true);
            IComplianceRepository complianceRepo = ObjectFactory.GetInstance<IComplianceRepository>();
            IDsqRepository dsqRepo = ObjectFactory.GetInstance<IDsqRepository>();

			string a = null;
			if (DrugID != null)
			{
				var answer = Lib.Data.DSQ.Answer.FindByDrug(DrugID.Value, q.ID.Value);
				if (answer != null && answer.ID.HasValue)
					a = answer.Value;
			}

			if (!q.ShouldShowQuestion(false, a))
				return;

			var children = Lib.Data.DSQ.Question.FindByParent(q);
			bool has_children = false;

			if (children != null && children.Count > 0)
				has_children = true;

            if(q.HideFromView)
            {
                // if the answere was no, we don't care if it has any
                // children
                if(q.FieldType == "Yes/No" && a == "No")
                    return;

                if( has_children )
                {
                    foreach( var cq in children )
                        RenderQuestion( writer, cq );
                }

                return;
            }

            Eoc eocForQ = complianceRepo.GetEoc(DrugID.Value, q.ID.Value);
            bool for_eoc = false;

            if(eocForQ != null)
            {
                if(eocForQ.AppliesTo.Any(r => Framework.Security.Manager.HasRole(r)))
                {
                    for_eoc = true;
                }
            }

			string cssClass = "clearfix form-row";
			if (has_children)
				cssClass += " has-children";

			if (q.ParentID.HasValue)
			{
				writer.AddAttribute("data-parent-id", q.ParentID.Value.ToString());
				writer.AddAttribute("data-parent-checks", q.ParentChecks);
				cssClass += " has-parent";
			}

			writer.AddAttribute("data-id", q.ID.Value.ToString());
			writer.AddAttribute("class", cssClass);
			writer.RenderBeginTag("div");

			writer.AddAttribute("class", "form-label");
			writer.AddAttribute("for", "form-q-"+q.ID.Value.ToString());
			writer.RenderBeginTag("label");

			if( for_eoc )
			{
				writer.AddAttribute( "class", "form-label-text" );
				writer.RenderBeginTag( "span" );
			}

			writer.WriteEncodedText(q.ViewText);

			if( for_eoc )
			{
				writer.RenderEndTag();

				writer.AddAttribute( "class", "form-label-required" );
				writer.RenderBeginTag( "span" );

				if( !string.IsNullOrEmpty( a ) )
					writer.WriteEncodedText( a );

				writer.RenderEndTag();
			}

			writer.RenderEndTag();

			writer.AddAttribute("class", "form-input");
			writer.RenderBeginTag("div");

			writer.AddAttribute("class", "form-info");
			writer.AddAttribute("name", "q-" + q.ID.Value.ToString());
			writer.RenderBeginTag("span");

			switch (q.FieldType)
			{
			    case "Checkbox List":
				    RenderQuestion_CheckboxList(writer, q, a);
				    break;
			    case "Link List":
			    case "EOC":
				    RenderQuestion_LinkList(writer, q, a);
				    break;
                case "TextArea":
                    // MJL - This is not the most efficient solution.  Saving the 
                    // converted text would be more efficient.  This approach keeps
                    // Markdown available for editing.
                    writer.Write(markdownSvc.ToHtml(a));
                    break;
			    default:
				    if( !string.IsNullOrEmpty(a) )
					    writer.WriteEncodedText(a);
				    break;
			}

			writer.RenderEndTag();

			writer.RenderEndTag();

			writer.RenderEndTag();

			bool eoc_applies = false;
			Lib.Data.Eoc eoc = null;

			if( for_eoc )
			{
                /*
				eoc = new Lib.Data.Eoc( long.Parse(q.Answers) );
				if( eoc != null )
				{
					var profile = Systems.Security.GetCurrentProfile();
					if( profile != null && eoc.HasUserType( profile.UserTypeID ) )
						eoc_applies = true;
				}
                */

                // code has been changed to only display as an eoc if the
                // eoc applies to the current user.
                eoc_applies = for_eoc;
			}

			if( has_children || eoc_applies )
			{
				writer.AddAttribute("id", "form-q-" + q.ID.Value.ToString()+"-children");
				writer.AddAttribute("class", "contains-children");
				writer.RenderBeginTag("div");

				var drugs = Lib.Systems.Lists.GetMyDrugList().GetItems<Lib.Data.Drug>();
				bool has_drug = false;

				foreach( var d in drugs )
					if( d.ID == Drug.ID )
						has_drug = true;

				if( eoc_applies && has_drug )
				{
                    PrescriberEoc prescriberEoc = complianceRepo.Find(Systems.Security.GetCurrentProfile().ID ?? 0, Drug.ID ?? 0, eocForQ.Id);
                    bool is_certified = prescriberEoc != null 
                        ? prescriberEoc.CompletedAt != null
                        : false;

                    DateTime? date_certified = prescriberEoc != null
                        ? prescriberEoc.CompletedAt
                        : null;

					writer.AddAttribute( "data-parent-id", q.ID.Value.ToString() );
					writer.AddAttribute( "data-parent-checks", "Required|Optional" );
					writer.AddAttribute( "data-id", q.ID.Value.ToString() + "-certify" );
					writer.AddAttribute( "class", "clearfix form-row has-parent eoc-certify-row" + ((is_certified) ? " eoc-certified" : "") );
					writer.RenderBeginTag( "div" );
					{
						writer.AddAttribute( "class", "label-wrapper clearfix" );
						writer.RenderBeginTag( "div" );
						{
							writer.AddAttribute( "class", "form-label" );
							writer.AddAttribute( "for", "form-q-" + q.ID.Value.ToString() );
							writer.RenderBeginTag( "label" );
							{
								writer.WriteEncodedText( "Yes, I acknowledge that I am aware of the " + q.ViewText + " requirement for this REMS medication" );
							}
							writer.RenderEndTag();
						}
						writer.RenderEndTag();

						writer.AddAttribute( "class", "form-input" );
						writer.RenderBeginTag( "div" );
						{
							if( is_certified )
							{
								writer.AddAttribute( "alt", "Certified" );
								writer.AddAttribute( "class", "eoc-certified-icon" );
								writer.AddAttribute( "src", "/App/images/Warning_Green_Check.png" );
								writer.RenderBeginTag( "img" );
								writer.RenderEndTag();

								writer.RenderBeginTag( "span" );
								{
									writer.WriteEncodedText( "Complete as of " + date_certified.Value.ToShortDateString() + " " + date_certified.Value.ToShortTimeString() );
								}
								writer.RenderEndTag();
							}
							else
							{
								writer.AddAttribute( "alt", "Not Certified" );
								writer.AddAttribute( "class", "eoc-certified-icon" );
								writer.AddAttribute( "src", "/App/images/Warning_Yellow_Exclimation.png" );
								writer.RenderBeginTag( "img" );
								writer.RenderEndTag();

								writer.AddAttribute( "class", "button ajax-button" );
								writer.AddAttribute( "href", "/api/App/Users/Certified?drug_id=" + Drug.ID.Value + "&eoc_name=" + eocForQ.Name );
								writer.RenderBeginTag( "a" );
								{
									writer.WriteEncodedText( "YES" );
								}
								writer.RenderEndTag();

								writer.RenderBeginTag( "span" );
								{
									writer.WriteEncodedText( "Reviewed Requirements?" );
								}
								writer.RenderEndTag();
							}
						}
						writer.RenderEndTag();
					}
					writer.RenderEndTag();
				}

				if( has_children )
				{
					foreach( var cq in children )
						RenderQuestion( writer, cq );
				}

				writer.RenderEndTag();
			}
		}

		protected void RenderQuestion_CheckboxList(HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null)
		{
			if (string.IsNullOrEmpty(a))
				return;

			string[] ans_parts = a.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var ap in ans_parts)
			{
				writer.WriteEncodedText(ap);
				writer.WriteBreak();
			}
		}

		protected void RenderQuestion_LinkList(HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null)
		{
			var answers = Lib.Data.DSQ.Link.FindByDrug(this.DrugID.Value, q.ID.Value);
			
			foreach (var answer in answers)
			{
				var ans = answer.Value;
				if( string.IsNullOrEmpty( ans ) )
					continue;

				writer.AddAttribute( "class", "dsq-list-row clearfix" );
				writer.RenderBeginTag( "div" );
				{
					if( ans.Trim().ToLower().StartsWith( "http://" ) || ans.Trim().ToLower().StartsWith( "https://" ) )
					{
						string css_class = "link-list-icon-a";

						if( Lib.Systems.Security.GetCurrentPrescriber() != null )
						{
							writer.AddAttribute( "href", "/api/App/List/FormsAndDocuments/AddItem?id=" + answer.ID.Value );
							writer.AddAttribute( "class", "ajax-button link-list-icon-a" );
							writer.RenderBeginTag( "a" );
							{
								writer.AddAttribute( "src", "/App/images/navicons/101.png" );
								writer.AddAttribute( "class", "link-list-icon" );
								writer.RenderBeginTag( "img" );
								writer.RenderEndTag();
							}
							writer.RenderEndTag();
						}
						else
							css_class += " pad-right";
						
						writer.AddAttribute( "href", ans );
						writer.AddAttribute( "target", "_blank" );
						writer.AddAttribute( "class", css_class );
						writer.RenderBeginTag( "a" );
						{
							writer.AddAttribute( "src", "/App/images/navicons/100.png" );
							writer.AddAttribute( "class", "link-list-icon" );
							writer.RenderBeginTag( "img" );
							writer.RenderEndTag();
						}
						writer.RenderEndTag();

						writer.AddAttribute( "href", ans );
						writer.AddAttribute( "target", "_blank" );
						writer.AddAttribute( "class", "link-list-text" );
						writer.RenderBeginTag( "a" );
						{
							writer.WriteEncodedText( answer.Label );
						}
						writer.RenderEndTag();
					}
					else
					{
						writer.AddAttribute( "src", "/App/images/navicons/75.png" );
						writer.AddAttribute( "class", "link-list-icon pad-right" );
						writer.RenderBeginTag( "img" );
						writer.RenderEndTag();

						writer.AddAttribute( "class", "link-list-text" );
						writer.RenderBeginTag( "span" );
						{
							writer.WriteEncodedText( ans );
						}
						writer.RenderEndTag();
					}
				}
				writer.RenderEndTag();
			}
		}
	}
}
