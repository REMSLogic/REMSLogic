using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Framework.Data
{
	public abstract class RowView
	{
		public RowView()
		{
		}

		public RowView( IDataRecord row )
		{
			LoadData( row );
		}

		protected virtual void LoadData( IDataRecord row )
		{
			Manager.LoadColumns( this, row );
		}
	}
}
