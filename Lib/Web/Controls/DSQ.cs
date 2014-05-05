using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lib.Web.Controls
{
	[ToolboxData("<{0}:DSQ runat=\"server\"></{0}:DSQ>")]
	public class DSQ : WebControl
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

		protected int? version;

		protected override void RenderContents(HtmlTextWriter writer)
		{
			version = null;

			if (DrugID == null)
			{
				long id;
				if( long.TryParse(Page.Request["drugid"], out id) || long.TryParse(Page.Request["drug_id"], out id) || 
					long.TryParse(Page.Request["drug-id"], out id) || long.TryParse(Page.Request["id"], out id) )
				{
					DrugID = id;

					var v = Lib.Data.DrugVersion.FindLatestByDrug(id);
					if( v != null && v.Status == "Pending" )
						version = v.Version;
				}
			}

			writer.AddAttribute("class", "dsq-accordion");
			writer.RenderBeginTag("section");

			foreach (var s in Lib.Data.DSQ.Section.FindAll())
				RenderSection(writer, s);

			writer.RenderEndTag();
		}

		protected void RenderSection(HtmlTextWriter writer, Lib.Data.DSQ.Section s)
		{
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

			string a = null;
			bool pending_changes = false;
			if (DrugID != null)
			{
				var answer = Lib.Data.DSQ.Answer.FindByDrug(DrugID.Value, q.ID.Value);
				if (answer != null && answer.ID.HasValue)
				{
					a = answer.Value;
					if( version.HasValue )
					{
						var av = Lib.Data.DSQ.AnswerVersion.FindByDrugVersion(DrugID.Value, version.Value, answer.ID.Value);
						if( av != null && av.ID.HasValue )
						{
							a = av.Value;
							pending_changes = true;
						}
					}
				}
			}

			if( !q.ShouldShowQuestion( true, a ) )
				return;


			if( q.FieldType == "Link List" )
			{
				RenderQuestion_LinkList(writer, q, a, pending_changes);
				return;
			}
			else if( q.FieldType == "EOC" )
			{
				RenderQuestion_EOC( writer, q, a, pending_changes );
				return;
			}

			var children = Lib.Data.DSQ.Question.FindByParent(q);
			bool has_children = false;

			if (children.Count > 0)
				has_children = true;

			string cssClass = "clearfix form-row";
			if( has_children )
				cssClass += " has-children";
			if( pending_changes )
				cssClass += " pending-changes";

			if( q.ParentID.HasValue )
			{
				writer.AddAttribute( "data-parent-id", q.ParentID.Value.ToString() );
				writer.AddAttribute( "data-parent-checks", q.ParentChecks );
			}

			writer.AddAttribute( "data-id", q.ID.Value.ToString() );
			writer.AddAttribute( "class", cssClass );
			writer.RenderBeginTag( "div" );

			writer.AddAttribute( "class", "form-label" );
			writer.AddAttribute( "for", "form-q-" + q.ID.Value.ToString() );
			writer.RenderBeginTag( "label" );

			writer.WriteEncodedText( q.Text );

			if( q.Required )
			{
				writer.WriteEncodedText( " " );
				writer.RenderBeginTag( "em" );
				writer.WriteEncodedText( "*" );
				writer.RenderEndTag();
			}

			writer.RenderEndTag();

			switch( q.FieldType )
			{
			case "TextBox":
				RenderQuestion_TextBox( writer, q, a );
				break;
			case "TextArea":
				RenderQuestion_TextArea( writer, q, a );
				break;
			case "Yes/No":
				RenderQuestion_YesNo( writer, q, a );
				break;
			case "Checkbox":
				RenderQuestion_Checkbox( writer, q, a );
				break;
			case "Checkbox List":
				RenderQuestion_CheckboxList( writer, q, a );
				break;
			case "Radio Buttons":
				RenderQuestion_RadioButtons( writer, q, a );
				break;
			case "DropDown":
				RenderQuestion_DropDown( writer, q, a );
				break;
			default:
				throw new Exception( "Unknown FieldType for Lib.Data.Question with ID " + q.ID.Value.ToString() );
			}

			writer.RenderEndTag();

			if (has_children)
			{
				writer.AddAttribute("id", "form-q-" + q.ID.Value.ToString()+"-children");
				writer.AddAttribute("class", "to-be-hidden contains-children");
				writer.RenderBeginTag("div");

				foreach (var cq in children)
					RenderQuestion(writer, cq);

				writer.RenderEndTag();
			}
		}

		protected void RenderQuestion_EOC( HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null, bool pending_changes = false )
		{
			long eoc_id;
			if( !long.TryParse( q.Answers, out eoc_id ) )
				throw new InvalidOperationException( "[" + q.Answers + "] is not a valid EOC ID." );

			var eoc = new Lib.Data.Eoc( eoc_id );

			var children = Lib.Data.DSQ.Question.FindByParent( q );
			bool has_children = false;

			if( children.Count > 0 )
				has_children = true;

			string cssClass = "clearfix form-row eoc-row has-children";
			if( pending_changes )
				cssClass += " pending-changes";

			if( q.ParentID.HasValue )
			{
				writer.AddAttribute( "data-parent-id", q.ParentID.Value.ToString() );
				writer.AddAttribute( "data-parent-checks", q.ParentChecks );
			}

			writer.AddAttribute( "data-id", q.ID.Value.ToString() );
			writer.AddAttribute( "class", cssClass );
			writer.RenderBeginTag( "div" );
			{
				writer.AddAttribute( "class", "form-label" );
				writer.AddAttribute( "for", "form-q-" + q.ID.Value.ToString() );
				writer.RenderBeginTag( "label" );
				{
					writer.WriteEncodedText( q.Text );

					if( q.Required )
					{
						writer.WriteEncodedText( " " );
						writer.RenderBeginTag( "em" );
						{
							writer.WriteEncodedText( "*" );
						}
						writer.RenderEndTag();
					}
				}
				writer.RenderEndTag();

				writer.AddAttribute( "class", "form-input" );
				writer.RenderBeginTag( "div" );
				{
					writer.AddAttribute( "id", "form-q-" + q.ID.Value.ToString() );
					writer.AddAttribute( "name", "q-" + q.ID.Value.ToString() );
					if( q.Required )
						writer.AddAttribute( "required", "required" );
					writer.RenderBeginTag( "select" );

					var answers = new [] {"","Required","Optional","No"};

					foreach( var ans in answers )
					{
						writer.AddAttribute( "value", ans.Trim() );
						if( !string.IsNullOrEmpty( a ) && ans.Trim() == a )
							writer.AddAttribute( "selected", "selected" );
						writer.RenderBeginTag( "option" );

						writer.WriteEncodedText( ans.Trim() );

						writer.RenderEndTag();
					}

					writer.RenderEndTag();
				}

				writer.RenderEndTag();
			}
			writer.RenderEndTag();


			writer.AddAttribute( "id", "form-q-" + q.ID.Value.ToString() + "-children" );
			writer.AddAttribute( "class", "to-be-hidden contains-children" );
			writer.RenderBeginTag( "div" );
			{
				// Render a Link List
				RenderQuestion_LinkList( writer, q, null, pending_changes, true );

				if( has_children )
					foreach( var cq in children )
						RenderQuestion( writer, cq );
			}
			writer.RenderEndTag();

		}

		protected void RenderQuestion_TextBox(HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null)
		{
			writer.AddAttribute("class", "form-input");
			writer.RenderBeginTag("div");

				writer.AddAttribute("type", "text");
				writer.AddAttribute("id", "form-q-" + q.ID.Value.ToString());
				writer.AddAttribute("name", "q-" + q.ID.Value.ToString());
				if( q.Required )
					writer.AddAttribute("required", "required");
				if( !string.IsNullOrEmpty(q.SubText) )
					writer.AddAttribute("placeholder", q.SubText);
				if (!string.IsNullOrEmpty(a))
					writer.AddAttribute("value", a);
				writer.RenderBeginTag("input");
				writer.RenderEndTag();

			writer.RenderEndTag();
		}

		protected void RenderQuestion_TextArea(HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null)
		{
			writer.AddAttribute("class", "form-input form-textarea");
			writer.RenderBeginTag("div");

				writer.AddAttribute("id", "form-q-" + q.ID.Value.ToString());
				writer.AddAttribute("name", "q-" + q.ID.Value.ToString());
				writer.AddAttribute("class", "auto-grow");
				writer.AddAttribute("rows", "5");
				writer.AddAttribute( "cols", "40" );
				if (q.Required)
					writer.AddAttribute("required", "required");
				if (!string.IsNullOrEmpty(q.SubText))
					writer.AddAttribute("placeholder", q.SubText);
				writer.RenderBeginTag("textarea");
				if (!string.IsNullOrEmpty(a))
					writer.WriteEncodedText(a);
				writer.RenderEndTag();

			writer.RenderEndTag();
		}

		protected void RenderQuestion_YesNo(HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null)
		{
			writer.AddAttribute("class", "form-input");
			writer.RenderBeginTag("div");

				writer.AddAttribute("class", "radiogroup");
				writer.RenderBeginTag("div");

					writer.RenderBeginTag("label");

						writer.AddAttribute("type", "radio");
						writer.AddAttribute("name", "q-" + q.ID.Value.ToString());
						if (!string.IsNullOrEmpty(a) && a == "Yes")
							writer.AddAttribute("checked", "checked");
						writer.AddAttribute("value", "Yes");
						writer.RenderBeginTag("input");
						writer.RenderEndTag();

						writer.WriteEncodedText(" Yes");

					writer.RenderEndTag();

					writer.WriteEncodedText(" ");

					writer.RenderBeginTag("label");

						writer.AddAttribute("type", "radio");
						writer.AddAttribute("name", "q-" + q.ID.Value.ToString());
						if (!string.IsNullOrEmpty(a) && a == "No")
							writer.AddAttribute("checked", "checked");
						writer.AddAttribute("value", "No");
						writer.RenderBeginTag("input");
						writer.RenderEndTag();

					writer.WriteEncodedText(" No");

					writer.RenderEndTag();

				writer.RenderEndTag();

			writer.RenderEndTag();
		}

		protected void RenderQuestion_Checkbox(HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null)
		{
			writer.AddAttribute("class", "form-input");
			writer.RenderBeginTag("div");

				writer.AddAttribute("class", "checkgroup");
				writer.RenderBeginTag("div");

					writer.RenderBeginTag("label");

						writer.AddAttribute("type", "checkbox");
						writer.AddAttribute("id", "form-q-" + q.ID.Value.ToString());
						writer.AddAttribute("name", "q-" + q.ID.Value.ToString());
						if (q.Required)
							writer.AddAttribute("required", "required");
						if (!string.IsNullOrEmpty(a) && a == "True")
							writer.AddAttribute("checked", "checked");
						writer.AddAttribute("value", "True");
						writer.RenderBeginTag("input");
						writer.RenderEndTag();

						if (!string.IsNullOrEmpty(q.SubText))
							writer.WriteEncodedText(" " + q.SubText);

					writer.RenderEndTag();

				writer.RenderEndTag();

			writer.RenderEndTag();
		}

		protected void RenderQuestion_CheckboxList(HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null)
		{
			writer.AddAttribute("class", "form-input");
			writer.RenderBeginTag("div");

				writer.AddAttribute("class", "checkgroup");
				writer.RenderBeginTag("div");

					var answers = q.Answers.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
					int idx = 0;
					string[] ans_parts = new string[] { };
					if( !string.IsNullOrEmpty(a) )
						ans_parts = a.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

					foreach (var ans in answers)
					{
						writer.RenderBeginTag("label");

						writer.AddAttribute("type", "checkbox");
						writer.AddAttribute("id", "form-q-" + q.ID.Value.ToString() + (++idx).ToString());
						writer.AddAttribute("name", "q-" + q.ID.Value.ToString() + "[]");
						if( ans_parts.Contains( ans.Trim() ) )
							writer.AddAttribute("checked", "checked");
						writer.AddAttribute("value", ans.Trim());
						writer.RenderBeginTag("input");
						writer.RenderEndTag();

						writer.WriteEncodedText( " " + ans.Trim() );

						writer.RenderEndTag();
					}

				writer.RenderEndTag();

			writer.RenderEndTag();
		}

		protected void RenderQuestion_RadioButtons(HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null)
		{
			writer.AddAttribute("class", "form-input");
			writer.RenderBeginTag("div");

				writer.AddAttribute("class", "radiogroup");
				writer.RenderBeginTag("div");

					var answers = q.Answers.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
					int idx = 0;

					foreach (var ans in answers)
					{
						writer.RenderBeginTag("label");

						writer.AddAttribute("type", "radio");
						writer.AddAttribute("id", "form-q-" + q.ID.Value.ToString() + "-" + (++idx).ToString());
						writer.AddAttribute("name", "q-" + q.ID.Value.ToString());
						if( !string.IsNullOrEmpty( a ) && ans.Trim() == a )
							writer.AddAttribute("checked", "checked");
						writer.AddAttribute( "value", ans.Trim() );
						writer.RenderBeginTag("input");
						writer.RenderEndTag();

						writer.WriteEncodedText( " " + ans.Trim() );

						writer.RenderEndTag();
					}

				writer.RenderEndTag();

			writer.RenderEndTag();
		}

		protected void RenderQuestion_DropDown(HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null)
		{
			writer.AddAttribute("class", "form-input");
			writer.RenderBeginTag("div");

				writer.AddAttribute("id", "form-q-" + q.ID.Value.ToString());
				writer.AddAttribute("name", "q-" + q.ID.Value.ToString());
				if (q.Required)
					writer.AddAttribute("required", "required");
				writer.RenderBeginTag("select");

					var answers = q.Answers.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

					foreach (var ans in answers)
					{
						writer.AddAttribute( "value", ans.Trim() );
						if( !string.IsNullOrEmpty( a ) && ans.Trim() == a )
							writer.AddAttribute("selected", "selected");
						writer.RenderBeginTag("option");

						writer.WriteEncodedText( ans.Trim() );

						writer.RenderEndTag();
					}

				writer.RenderEndTag();

			writer.RenderEndTag();
		}

		protected void RenderQuestion_LinkList( HtmlTextWriter writer, Lib.Data.DSQ.Question q, string a = null, bool pending_changes = false, bool for_eoc = false )
		{
			if( for_eoc )
			{
				writer.AddAttribute( "data-parent-id", q.ID.Value.ToString() );
				writer.AddAttribute( "data-parent-checks", "Required|Optional" );
			}
			else if (q.ParentID.HasValue)
			{
				writer.AddAttribute("data-parent-id", q.ParentID.Value.ToString());
				writer.AddAttribute("data-parent-checks", q.ParentChecks);
			}

			writer.AddAttribute("data-id", q.ID.Value.ToString() + ((for_eoc) ? "-ll" : "" ));
			string cssClass = "clearfix form-row form-padding dsq-links-table-holder";
			if (pending_changes)
				cssClass += " pending-changes";
			writer.AddAttribute("class", cssClass);
			writer.RenderBeginTag("div");
			{
				writer.AddAttribute("href", "#admin/dsq/link?drug-id=" + this.DrugID + "&question-id=" + q.ID.Value);
				writer.AddAttribute("class", "button");
				writer.AddStyleAttribute("float", "right");
				writer.AddStyleAttribute("margin-top", "10px");
				writer.AddStyleAttribute("margin-right", "10px");
				writer.RenderBeginTag("a");
				{
					writer.WriteEncodedText("Add Item");
				}
				writer.RenderEndTag();

				writer.RenderBeginTag("h2");
				{
					writer.WriteEncodedText((for_eoc ? "Information" : q.Text));
				}
				writer.RenderEndTag();

				writer.AddAttribute("id", "form-q-" + q.ID.Value);
				writer.AddAttribute("class", "display dsq-links-table");
				writer.RenderBeginTag("table");
				{
					writer.RenderBeginTag("thead");
					{
						writer.RenderBeginTag("tr");
						{
							writer.RenderBeginTag("th");
							writer.RenderEndTag();

							writer.RenderBeginTag("th");
							{
								writer.WriteEncodedText("Label");
							}
							writer.RenderEndTag();

							writer.RenderBeginTag( "th" );
							{
								writer.WriteEncodedText( "Updated" );
							}
							writer.RenderEndTag();

							writer.RenderBeginTag("th");
							{
								writer.WriteEncodedText("Information");
							}
							writer.RenderEndTag();

							writer.RenderBeginTag("th");
							{
								writer.WriteEncodedText("EOC");
							}
							writer.RenderEndTag();

							writer.RenderBeginTag("th");
							{
								writer.WriteEncodedText("Required");
							}
							writer.RenderEndTag();
						}
						writer.RenderEndTag();
					}
					writer.RenderEndTag();

					writer.RenderBeginTag("tbody");
					{
						var d = new Lib.Data.Drug(this.DrugID);

						if (d.ID.HasValue && d.ID.Value == this.DrugID)
						{
							var links = Lib.Data.DSQ.Link.FindByDrug(d, q);
							foreach (var link in links)
							{
								writer.AddAttribute("data-id", link.ID.Value.ToString());
								writer.RenderBeginTag("tr");
								{
									writer.RenderBeginTag("td");
									{
										writer.AddAttribute("href", "#admin/dsq/link?id=" + link.ID.Value + "&drug-id=" + this.DrugID + "&question-id=" + q.ID.Value);
										writer.AddAttribute("class", "button");
										writer.RenderBeginTag("a");
										{
											writer.WriteEncodedText("Edit");
										}
										writer.RenderEndTag();

										writer.WriteEncodedText(" ");

										writer.AddAttribute("href", "/api/Dev/DSQ/Link/Delete?id=" + link.ID.Value);
										writer.AddAttribute("class", "ajax-button button");
										writer.AddAttribute("data-confirmtext", "Are you sure you want to delete this item?");
										writer.RenderBeginTag("a");
										{
											writer.WriteEncodedText("Delete");
										}
										writer.RenderEndTag();
									}
									writer.RenderEndTag();

									writer.RenderBeginTag("td");
									{
										writer.WriteEncodedText(link.Label);
									}
									writer.RenderEndTag();

									writer.RenderBeginTag( "td" );
									{
										if( link.Date.HasValue )
											writer.WriteEncodedText( link.Date.Value.ToShortDateString() );
									}
									writer.RenderEndTag();

									writer.RenderBeginTag("td");
									{
										writer.WriteEncodedText(link.Value);
									}
									writer.RenderEndTag();

									writer.RenderBeginTag("td");
									{
										writer.WriteEncodedText(link.HasEoc? "Yes" : "No");
									}
									writer.RenderEndTag();

									writer.RenderBeginTag("td");
									{
										writer.WriteEncodedText(link.IsRequired? "Yes" : "No");
									}
									writer.RenderEndTag();
								}
								writer.RenderEndTag();
							}
						}
					}
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}
	}
}
