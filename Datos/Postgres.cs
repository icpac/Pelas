﻿#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Data;

using Npgsql;

namespace Datos
{
	/// <summary>
	/// Description of Postgres.
	/// </summary>
	public class Postgres : Generic
	{
		public Postgres()
		{
		}
	
	
		override public IDbConnection Coneccion 
		{ 
			get 
			{
				if (mConn == null)
					mConn = new NpgsqlConnection();
				return mConn; 
			}
		}
		
		override public IDbCommand Command
		{
			get 
			{ 
				if (mComd == null)
					mComd = new NpgsqlCommand();
				return mComd; 
			}
		}
		
		override public IDbDataAdapter Adpter
		{
			get
			{
				if (mAdpter == null)
					mAdpter = new NpgsqlDataAdapter();
				return mAdpter;
			}
		}
		
		override protected char EspCar
		{
			get { return '\"'; }
		}

        protected override IDataParameter GetBlobParameter(IDbConnection connection, IDataParameter p)
        {
            NpgsqlParameter prm = new NpgsqlParameter();
            return prm;
        }

        protected override DataTable GetSchema()
        {
            throw new NotImplementedException();
        }
    }
}
