#region TIT
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     Tlacaelel Icpac                                               }
{     tlacaelel.icpac@gmail.com                                     }
{                                                                   }
{*******************************************************************}
 */
#endregion
 /*Para leer los datos en Firebird
  * 
  */

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
            		(mCadenaConn as FbConnectionStringBuilder).UserID = Usuario;
            		(mCadenaConn as FbConnectionStringBuilder).Password = Contraseña;
            		(mCadenaConn as FbConnectionStringBuilder).Dialect = Dialecto;
            		(mCadenaConn as FbConnectionStringBuilder).Pooling = Agrupacion;
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

        private string Usuario
        { get { return "sysdba"; } }

        private string Contraseña
        { get { return "masterkey"; } }

        private int Dialecto
        { get { return 3; } }

        private bool Agrupacion
        { get { return false; } }
    }
}
