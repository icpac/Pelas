#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
	/// <summary>
	/// Description of SqlServer.
	/// </summary>
	public class SqlServer : Generic
	{
		public SqlServer()
		{
		}
		
		override public IDbConnection Coneccion 
		{ 
			get 
			{
				if (mConn == null)
					mConn = new SqlConnection();
				return mConn; 
			}
		}
		
		override public IDbCommand Command
		{
			get 
			{ 
				if (mComd == null)
					mComd = new SqlCommand();
				return mComd; 
			}
		}
		
		override public IDbDataAdapter Adpter
		{
			get
			{
				if (mAdpter == null)
					mAdpter = new SqlDataAdapter();
				return mAdpter;
			}
		}

        protected override IDataParameter GetBlobParameter(IDbConnection connection, IDataParameter p)
        {
            SqlParameter prm = new SqlParameter();
            return prm;
        }

        protected override DataTable GetSchema()
        {
            throw new NotImplementedException();
        }
    }
}
