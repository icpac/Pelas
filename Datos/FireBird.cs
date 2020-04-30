#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;

namespace Datos
{
    /// <summary>
    /// Description of FireBird.
    /// </summary>
    public class FireBird : Generic
    {
        public FireBird()
        {
        }

        override public IDbConnection Coneccion
        {
            get
            {
                if (mConn == null)
                    mConn = new FbConnection();
                return mConn;
            }
        }

        override public IDbCommand Command
        {
            get
            {
                if (mComd == null)
                    mComd = new FbCommand();
                return mComd;
            }
        }

        override public IDbDataAdapter Adpter
        {
            get
            {
                if (mAdpter == null)
                    mAdpter = new FbDataAdapter();
                return mAdpter;
            }
        }

        public override DbConnectionStringBuilder CadenaConn
        { 
            get
            {
                if (mCadenaConn == null)
                {
                    mCadenaConn = new FbConnectionStringBuilder();
		           	(mCadenaConn as FbConnectionStringBuilder).ServerType = FbServerType.Default;
            		(mCadenaConn as FbConnectionStringBuilder).UserID = "sysdba";
            		(mCadenaConn as FbConnectionStringBuilder).Password = "masterkey";
            		(mCadenaConn as FbConnectionStringBuilder).Dialect = 3;
            		(mCadenaConn as FbConnectionStringBuilder).Pooling = false;
                }
                return mCadenaConn;
            }
        }

        protected override IDataParameter GetBlobParameter(IDbConnection connection, IDataParameter p)
        {
            FbParameter prm = new FbParameter();
            return prm;
        }

        protected override DataTable GetSchema()
        {
            return (Coneccion as FbConnection).GetSchema("Tables", new string[] { null, null, null, "TABLE" });
        }
    }
}
