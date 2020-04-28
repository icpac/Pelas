using Negocio.EAS;
using System;

using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace Negocio
{
    public static class Admin
    {
        private static int NmrEmprs;

        public static List<Cliente> Productos(string ruta, int nmEm)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                NmrEmprs = nmEm;

                FireBird datos = new FireBird();
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("select * From {0} order by cve_art asc", ProductoNombre);
                FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;
                List<Inve> ListaInve = new List<Inve>();

                using (new CWaitCursor())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ListaInve.Add(new Inve
                            {
                                Clave = reader.GetString(0),
                                Descripcion = reader.GetString(1)
                            });
                        }
                    }
                    reader.Close();
                }
                return ListaInve;

                #region
                /*
                FireBird datos = new FireBird();

                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta; // stgs.RutaSAE;

                string sql = "select * From inve01 order by cve_art asc";

                DataSet datset = datos.LeeDatos(sql);

                if (datset != null && datset.Tables != null && datset.Tables.Count > 0)
                {

                    foreach (DataRow rw in datset.Tables[0].Rows)
                    {
                        Lista.Add(new Inve
                        {
                            Cve_art = rw.Field<string>("CVE_ART"),
                            Descr = rw.Field<string>("DESCR")
                        });
                    }
                }
                return Lista;*/
                #endregion
            }
        }

    }
}
