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
/* Negocio AES
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apl.Log;
using Datos;
using FirebirdSql.Data.FirebirdClient;
using Negocio.EAS;

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

                string sql = string.Format("select * From {0} order by cve_art asc", ClienteNombre);
                FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;
                List<Cliente> ListaClies = new List<Cliente>();

                using (new CWaitCursor())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ListaClies.Add(new Cliente
                            {
                                Clave = reader.GetString(0),
                            });
                        }
                    }
                    reader.Close();
                }
                return ListaClies;
            }
        }



        static private string ClienteNombre
        {
            get { return string.Format("clie{0}", NmrEmprs.ToString().PadLeft(2, '0')); }
        }

    }
}
