/*
 * javier1604@gmail.com
 * Carlos Javier Lopez Cruz
 *
 * User: Tlacaelel
 * Date: 22/02/2018
 * Time: 10:10 a.m.
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Linq;

namespace Datos
{
    /// <summary>
    /// Comentario a este codigo que no me acuerdo que hace
    /// </summary>
    abstract public class Generic
    {
        protected IDbConnection mConn;
        public virtual IDbConnection Coneccion
        {
            get { return null; }
        }

        protected IDbCommand mComd;
        public virtual IDbCommand Command
        {
            get { return null; }
        }

        protected IDbDataAdapter mAdpter;
        public virtual IDbDataAdapter Adpter
        {
            get { return null; }
        }

        protected DbConnectionStringBuilder mCadenaConn;
        public virtual DbConnectionStringBuilder CadenaConn
        {
            get { return null; }
        }

        protected IDataReader mReader;
        public virtual IDataReader Reader
        {
            get { return mReader; }
            set { mReader = value; }
        }

        public DataSet LeeDatos(string Selec)
        {
            DataSet dts = new DataSet();

            Coneccion.ConnectionString = CadenaConn.ConnectionString;

            Command.Connection = Coneccion;
            Command.CommandText = Selec;

            Adpter.SelectCommand = Command;

            try
            {
                Adpter.Fill(dts);
            }
            catch (Exception e)
            {
                throw new Exception(Selec + e.Message);
            }

            return dts;
        }

        public DataSet LeeDatos(List<string> Cmpos, string Tipo)
        {
            bool adCmp = false;
            string selec = string.Empty;

            selec = "SELECT ";
            foreach (string cmp in Cmpos)
            {
                if (adCmp)
                    selec += ", ";
                selec = string.Format("{0} {1}{2}{3}", selec,
                                      EspCar, cmp, EspCar);
                adCmp = true;
            }

            selec = string.Format("{0} FROM {1}{2}{3}", selec,
                                  EspCar, Tipo, EspCar);

            return LeeDatos(selec);
        }

        /// <summary>
        /// Esta rutina la hice hace mucho, gracias sharpdevelop
        /// </summary>
        /// <param name="typo"></param>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public DataSet LeeDatos(Type typo, string Tipo)
        {
            List<string> Cmpos = new List<string>();

            foreach (MemberInfo mi in typo.GetMembers())
            {
                if (mi.MemberType == MemberTypes.Property
                    || mi.MemberType == MemberTypes.Field)
                    Cmpos.Add(mi.Name);
            }
            return LeeDatos(Cmpos, Tipo);
        }

        public IDataReader LeeDatosOnly(string selec)
        {
            Coneccion.ConnectionString = CadenaConn.ConnectionString;

            Command.Connection = Coneccion;
            Command.CommandText = selec;

            if (Coneccion.State != ConnectionState.Open)
                Coneccion.Open();

            try
            {
                Reader = Command.ExecuteReader();
            }
            catch (Exception e)
            {
                throw new Exception(selec + e.Message);
            }

            return Reader;
        }

        public bool PruebaConexion()
        {
            try
            {
                Coneccion.ConnectionString = CadenaConn.ConnectionString;

                Coneccion.Open();
                Coneccion.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region + Crea base de datos
        /// <summary>
        /// Creo que jala en MySQL, pero en Access se hace de otra manera.
        /// </summary>
        public virtual void CreaBaseDatos()
        {
            string sentencia = "CREATE DATABASE icpac";

            EjecutaSentencia(sentencia, null);
        }
        #endregion

        public virtual bool ExisteTabla(string nameTable)
        {
            bool existe = false;

            Coneccion.ConnectionString = CadenaConn.ConnectionString;
            foreach (DataRow row in SchemaDataBase().Rows)
            {
                existe = row["TABLE_NAME"].ToString().ToUpper() == nameTable.ToUpper();
                if (existe)
                    break;
            }
            return existe;
        }

        public virtual bool CreaTabla(string nameTable, List<Campo> Campos)
        {
            bool ok = false;

            if (!ExisteTabla(nameTable))
            {
                string sqlTabla;
                IDataParameter[] parames = null;

                sqlTabla = SentenciaTabla(nameTable, Campos);
                EjecutaSentencia(sqlTabla, parames);

                var cmpsKey = from Campo c in Campos
                              where c.Indice
                              select c;

                    foreach (Campo cmp in cmpsKey)
                    {
                        sqlTabla = "CREATE INDEX Indice" + cmp.Nombre;
                        sqlTabla += " ON " + nameTable + " ( " + cmp.Nombre + " )";
                        EjecutaSentencia(sqlTabla, parames);
                    }

                ok = true;
            }
            else
            {
                VerificaTabla(nameTable, Campos);
            }
            return ok;
        }

        private void VerificaTabla(string nameTable, List<Campo> Campos)
        {
            /* Nov 2018 Todavía no lo voy a usar
            ConnectionState localState;
            DataTable schemaTable;

            localState = Coneccion.State;
            if (Coneccion.State == ConnectionState.Closed)
                Coneccion.Open();

            // schemaTable = (Coneccion as OleDbConnection).GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, nameTable, null });
            schemaTable = GetSchema();

            for (int i = 0; i < Campos.Count; ++i)
            {
                bool exist = false;

                foreach (DataRow row in schemaTable.Rows)
                    if (row["COLUMN_NAME"].ToString() == campos[i])
                    {
                        exist = true;
                        break;
                    }

                if (!exist)
                    AlteraTabla(nameTable, campos[i], tipos[i], tamanos[i]);
            }
            if (localState == ConnectionState.Closed)
                Coneccion.Close();
            */
        }

        private void AlteraTabla(string nameTabla, string campo, string tipo, string size)
        {
            IDataParameter[] parames = null;
            string sql = "ALTER TABLE ";

            sql += nameTabla;
            sql += " ADD ";
            sql += campo;
            sql += " ";
            sql += tipo;
            if (tipo == "char")
            {
                sql += "(";
                sql += size;
                sql += ")";
            }
            sql = sql.Replace("autoinc", "counter");
            sql = sql.Replace("numeric", "double");

            EjecutaSentencia(sql, parames);


            if (!sql.Contains("counter"))
            {
                sql = " UPDATE ";
                sql += nameTabla;
                sql += " SET ";
                sql += campo;
                sql += " = ";

                if (tipo == "char")
                    sql += "' '";
                else if (tipo == "money"
                    || tipo == "numeric")
                    sql += "0.0";
                else if (tipo == "integer")
                    sql += "0";
                else if (tipo == "boolean")
                    sql += "false";
                else if (tipo == "smallint")
                    sql += "0";

                EjecutaSentencia(sql, parames);
            }
        }

        /// <summary>
        /// Crea la sentencia para crear la tabla.
        /// </summary>
        /// <param name="nameTable"></param>
        /// <param name="campos"></param>
        /// <param name="tipos"></param>
        /// <param name="tamanos"></param>
        /// <param name="keys"></param>
        /// <param name="indexs"></param>
        /// <returns></returns>
        protected string SentenciaTabla(string nameTable, List<Campo> Campos)
        {
            string sqlTabla = string.Empty;
            string indexSql = string.Empty;


            foreach (Campo cmp in Campos)
            {
                if (sqlTabla.Length > 1)
                    sqlTabla += ", ";

                sqlTabla += cmp.Nombre;

                if (cmp.Tipo == "char")
                {
                    sqlTabla += " varchar (";   // char
                    sqlTabla += cmp.Tamano;
                    sqlTabla += ")";
                }
                else if (cmp.Tipo == "smallint")
                {
                    sqlTabla += " smallint";  // short
                    if (cmp.Value != null)
                        sqlTabla += " default " + cmp.Value.ToString();
                }
                else if (cmp.Tipo == "int")
                    sqlTabla += " integer";
                else if (cmp.Tipo == "boolean")
                    sqlTabla += " bit";
                else if (cmp.Tipo == "numeric")
                {
                    sqlTabla += " double precision";
                    if (cmp.Value != null)
                        sqlTabla += " default " + cmp.Value.ToString();
                }
                else if (cmp.Tipo == "money")
                    sqlTabla += " currency";
                else if (cmp.Tipo == "time")
                    sqlTabla += " datetime";
                else if (cmp.Tipo == "blob (100, 1)")
                    sqlTabla += " memo";
                else if (cmp.Tipo == "blob")
                    sqlTabla += " memo";
                else if (cmp.Tipo == "autoinc")
                    sqlTabla += " UNIQUEIDENTIFIER"; // " counter";
                else
                {
                    sqlTabla += " ";
                    sqlTabla += cmp.Tipo;
                }


                if (cmp.Requerido)
                    sqlTabla += " Not null";

                /*
                if (indexs.Contains(campos[i]))
                {
                    sqlTabla += " CONSTRAINT Indice";
                    sqlTabla += campos[i];
                }
                else*/
                if (cmp.Unico /*uniques.Contains(campos[i])*/)
                {
                    sqlTabla += " CONSTRAINT Indice";
                    sqlTabla += cmp.Nombre; // campos[i];
                    sqlTabla += " UNIQUE";
                }
            }

            sqlTabla = string.Format("CREATE TABLE {0} ( {1}", nameTable, sqlTabla);


            var cmpsKey = from Campo c in Campos
                          where c.Indice
                          select c;
            if (cmpsKey.Count() > 0)
            {
                foreach (Campo c in cmpsKey)
                {
                    if (indexSql.Length > 1)
                        indexSql += ", ";
                    indexSql += c.Nombre;
                }
                indexSql = string.Format(" CONSTRAINT z PRIMARY KEY ({0}", indexSql);

                sqlTabla += ", ";
                sqlTabla += indexSql;
                sqlTabla += ")";
            }

            sqlTabla += ")";
            return sqlTabla;
        }

        /// <summary>
        /// la primera vez obtenemos el schema de tablas, para usarlo las siguientes veces
        /// Asi era antes, pero no funciona si borramos y luego creamos una tabla, pues el esquema
        /// ha cambiado, pero en la memoria no ha cambiado.
        /// </summary>
        protected virtual DataTable SchemaDataBase()
        {
            DataTable dtSchema;
            ConnectionState localState;

            localState = Coneccion.State;
            if (Coneccion.State == ConnectionState.Closed)
                Coneccion.Open();

            dtSchema = GetSchema();
                //(Coneccion as OleDbConnection).GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            if (localState == ConnectionState.Closed)
                Coneccion.Close();

            return dtSchema;
        }

        protected abstract DataTable GetSchema();

        public int EjecutaSentencia(string sqlSentencia, params IDataParameter[] arrParam)
        {
            IDbConnection conn = Coneccion;
            IDbCommand cmd = conn.CreateCommand();
            bool mustCloseConnection = false;

            PrepareCommand(cmd, conn, (IDbTransaction)null, CommandType.Text, sqlSentencia, arrParam, out mustCloseConnection);

            int retval = cmd.ExecuteNonQuery();

            // Detach the IDataParameters from the command object, so they can be used again
            // don't do this...screws up output parameters -- cjbreisch
            // cmd.Parameters.Clear();
            if (mustCloseConnection)
                conn.Close();

            return retval;
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command
        /// </summary>
        /// <param name="command">The IDbCommand to be prepared</param>
        /// <param name="connection">A valid IDbConnection, on which to execute this command</param>
        /// <param name="transaction">A valid IDbTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or SQL command</param>
        /// <param name="commandParameters">An array of IDataParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
        private void PrepareCommand(IDbCommand command, IDbConnection connection,
            IDbTransaction transaction, CommandType commandType,
            string commandText, IDataParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        protected virtual void AttachParameters(IDbCommand command, IDataParameter[] commandParameters)
        {
            if (commandParameters != null)
            {
                foreach (IDataParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput
                            || p.Direction == ParameterDirection.Input)
                            && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }

                        if (p.DbType == DbType.Binary)
                        {
                            // special handling for BLOBs
                            command.Parameters.Add(GetBlobParameter(command.Connection, p));
                        }
                        else
                        {
                            command.Parameters.Add(p);
                        }
                    }
                }
            }
        }

        protected abstract IDataParameter GetBlobParameter(IDbConnection connection, IDataParameter p);

        virtual protected char EspCar
        {
            get { return ' '; }
        }

        public void ConeccClose()
        {
            Coneccion.Close();
        }
    }
}