using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data.DSQ
{
	[Table( DatabaseName = "FDARems", TableName = "DSQ_Questions", PrimaryKeyColumn = "ID" )]
	public class Question : Framework.Data.ActiveRecord
	{
		public static IList<string> GetValidFieldTypes()
		{
			var ret = new List<string>();

			ret.Add("TextBox");
			ret.Add("TextArea");
			ret.Add("Yes/No");
			ret.Add("Checkbox");
			ret.Add("Checkbox List");
			ret.Add("Radio Buttons");
			ret.Add("DropDown");
			ret.Add("Link List");
			ret.Add( "EOC" );

			return ret;
		}

		public static IList<Question> FindAll()
		{
			return FindAll<Question>( new[] { "+SectionID", "+Order" } );
		}

		public static IList<Question> FindBySection(Section s, bool include_children = true)
		{
			if (s == null || !s.ID.HasValue)
				return new List<Question>();

			return FindBySection(s.ID.Value, include_children);
		}

		public static IList<Question> FindBySection(long sid, bool include_children = true)
		{
			var ps = new Dictionary<string, object> {
				{ "SectionID", sid }
			};

			if( !include_children )
				ps.Add( "ParentID", SpecialValue.IsNull );

			return FindAllBy<Question>( ps, new[] { "+Order" } );
		}

		public static IList<Question> FindPossibleParents()
		{
			return FindAllBy<Question>( new Dictionary<string, object> {
				{ "FieldType", "Yes/No" }
			}, new[] { "+Order" } );
		}

		public static IList<Question> FindByParent(Question q)
		{
			if (q == null || !q.ID.HasValue)
				return new List<Question>();

			return FindByParent(q.ID.Value);
		}

		public static IList<Question> FindByParent(long qid)
		{
			return FindAllBy<Question>( new Dictionary<string, object> {
				{ "ParentID", qid }
			}, new[] { "+Order" } );
		}

		public static Question FindByDevName(string dev_name)
		{
			return FindFirstBy<Question>( new Dictionary<string, object> {
				{ "DevName", dev_name }
			});
		}

		[Column]
		public long SectionID;
		[Column]
		public long? ParentID;
		[Column]
		public string ParentChecks;
		[Column]
		public string Text;
		[Column]
		public string viewText;
		public string ViewText
		{
			get { return (string.IsNullOrEmpty( viewText )) ? Text : viewText; }
			set { viewText = value; }
		}
		[Column]
		public string SubText;
		[Column]
		public string HelpText;
		[Column]
		public string DevName;
		[Column]
		public bool Required;
		[Column]
		public string FieldType;
		[Column]
		public string Answers;
		[Column]
		public int Order;
		[Column]
		public string ShowForRoles;
		[Column]
		public string HideForRoles;
		[Column]
		public string HideForAnswers;
		[Column]
		public string ShowChildrenForAnswers;
        [Column]
        public bool HideFromView;
        [Column]
        public long? EocId;

		public Question(long? id = null) : base(id)
		{ }

		public Question(IDataRecord row) : base(row)
		{ }

		// TODO: Move a Lib.Systems class ASAP
		public bool ShouldShowQuestion(bool edit, string answer)
		{
			var u = Framework.Security.Manager.GetUser();

			if (u == null)
				return false;

			bool ret = false;

			if (!string.IsNullOrEmpty(ShowForRoles))
			{
				var rs = ShowForRoles.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

				foreach (var r in rs)
				{
					if (Framework.Security.Manager.HasRole(r))
					{
						ret = true;
						break;
					}
				}

				if (!ret)
					return false;
			}

			if (!string.IsNullOrEmpty(HideForRoles))
			{
				var rs = HideForRoles.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

				foreach (var r in rs)
					if (Framework.Security.Manager.HasRole(r))
						return false;
			}

			if (edit)
				return true;

			if (string.IsNullOrEmpty(answer))
				return true;

            if(answer.ToLower() == "n/a" || answer.ToLower() == "n / a")
                return false;

			if (!string.IsNullOrEmpty(HideForAnswers))
			{
				var answers = HideForAnswers.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

				foreach (var a in answers)
					if (a.ToLower() == answer.ToLower())
						return false;
			}

			if( this.FieldType == "EOC" && answer.ToLower() == "no" )
				return false;

			return true;
		}

		public bool ShouldShowChildren( bool showing_this, string answer )
		{
			if( showing_this == false && !string.IsNullOrEmpty( ShowChildrenForAnswers ) )
			{
				var answers = ShowChildrenForAnswers.Split( new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries );

				foreach( var a in answers )
					if( a.ToLower() == answer.ToLower() )
						return true;
			}

			return showing_this;
		}
	}
}
