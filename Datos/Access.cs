/*
 * javier1604@gmail.com
 * Carlos Javier Lopez Cruz
 *
 * User: Tlacaelel
 * Date: 22/02/2018
 * Time: 10:31 a.m.
 * 
 */
using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;


namespace Datos
{
	/// <summary>
	/// Description of Access.
	/// </summary>
	public class Access : Generic
	{
		public Access()
		{
		}
		
		override public IDbConnection Coneccion 
		{ 
			get 
			{
				if (mConn == null)
					mConn = new OleDbConnection();
				return mConn; 
			}
		}
		
		override public IDbCommand Command
		{
			get 
			{ 
				if (mComd == null)
					mComd = new OleDbCommand();
				return mComd; 
			}
		}
		
		override public IDbDataAdapter Adpter
		{
			get
			{
				if (mAdpter == null)
					mAdpter = new OleDbDataAdapter();
				return mAdpter;
			}
		}

        protected override IDataParameter GetBlobParameter(IDbConnection connection, IDataParameter p)
        {
            OleDbParameter prm = new OleDbParameter();
            return prm;
        }

        protected override DataTable GetSchema()
        {
            return (Coneccion as OleDbConnection).GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
        }
    }
}
