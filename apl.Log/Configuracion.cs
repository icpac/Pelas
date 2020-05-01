#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;

namespace apl.Log
{
    using System.IO;
    using System.Windows.Forms;

    using IniFile;

    /// <summary>
    /// Configuracion en Xml
    /// </summary>
    public class Configuracion
    {
        /// <summary>
        /// Lee un valor de un xml como un ini
        /// </summary>
        /// <param name="nameXml">Nombre del xml</param>
        /// <param name="seccion">Seccion</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string LeeConfiguracion(string nameXml, string seccion, string key)
        {
            string fileIniLoc;
            IniFileReader loc;
            IniFileReader red;
            bool exsRd = false;


            fileIniLoc = string.IsNullOrEmpty(nameXml) ? NameXML : nameXml;
            apl.Log.LogDebug.Escribe(String.Format("Lee configuracion: s:{0}, k:{1}, f:{2}",
                seccion, key, fileIniLoc));
            if (File.Exists(fileIniLoc))
            {
                string pathRed = LeeConfiguracionOld(nameXml, seccion, "Red");
                string fileIniRed = string.IsNullOrEmpty(nameXml)
                    ? Path.Combine(pathRed ?? string.Empty, Path.GetFileName(NameXML))
                    : Path.Combine(pathRed, nameXml);
                exsRd = File.Exists(fileIniRed);

                red = new IniFileReader(fileIniRed, true);
                loc = new IniFileReader(fileIniLoc, true);

                string valR = exsRd ? red.GetIniValue(seccion, key) : string.Empty;
                string valL = loc.GetIniValue(seccion, key);
                return !string.IsNullOrEmpty(valL) ? valL : valR;
            }
            return string.Empty;
        }

        public static string LeeConfiguracionOld(string nameXml, string seccion, string key)
        {
            string fileIni;
            IniFileReader ini;

            apl.Log.LogDebug.Escribe(String.Format("Lee configuracion: {0}, {1}", seccion, key));

            fileIni = string.IsNullOrEmpty(nameXml) ? NameXML : nameXml;
            if (File.Exists(fileIni))
            {
                ini = new IniFileReader(fileIni, true);

                return ini.GetIniValue(seccion, key);
            }
            return string.Empty;
        }

        /// <summary>
        /// Obten la ruta de datos
        /// </summary>
        /// <param name="nameXml"></param>
        /// <returns></returns>
        public static string ObtenRutaDatos(string nameXml)
        {
            string ruta;

            ruta = LeeConfiguracion(nameXml, "Aplicativo", "ruta");
            if (ruta.Contains(".mdb"))
            {
                FileInfo file = new FileInfo(ruta);

                ruta = file.DirectoryName;
            }
            apl.Log.LogDebug.Escribe("Obten ruta datos");
            return ruta;
        }

        /// <summary>
        /// Guarda la configuracion en el xml
        /// </summary>
        /// <param name="nameXml"></param>
        /// <param name="seccion"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void GuardaConfiguracion(string nameXml, string seccion, string key, string value)
        {
            try
            {
                string fileIni;
                IniFileReader ini;

                fileIni = string.IsNullOrEmpty(nameXml) ? NameXML : nameXml;
                ini = new IniFileReader(fileIni, true);

                ini.OutputFilename = fileIni;
                ini.SetIniValue(seccion, key, value);
                ini.Save();
                apl.Log.LogDebug.Escribe("Guarda configuracion");
            }
            catch (Exception ex)
            {
                apl.Log.LogDebug.Escribe("No pude guardar configuracion" + ex.Message);
            }
        }

        /// <summary>
        /// Da el nombre del xml según el nombre de exe
        /// </summary>
        public static string NameXML
        {
            get
            {
                string aux = Application.ExecutablePath;

                aux = Path.ChangeExtension(Application.ExecutablePath, "xml");
                return aux;
            }
        }

        /// <summary>
        /// Lee del Ini la 'cadena' y nos da el DataSource
        /// </summary>
        /// <returns></returns>
        public static string[] Ruta_Nombre()
        {
            string cadena = LeeConfiguracion(string.Empty, "Aplicativo", "cadena");
            char[] splitter = { ';' };
            char[] split = { '=' };

            string[] tags = cadena.Split(splitter);
            string[] items;
            string[] datas = new string[6];

            if (tags.Length > 0)
            {
                foreach (string tag in tags)
                {
                    items = tag.Split(split);
                    if (items[0].ToUpper().Trim() == "DATA SOURCE" || items[0].ToUpper().Trim() == "DATABASE" || items[0].ToUpper().Trim() == "INITIAL CATALOG" || items[0].ToUpper().Trim() == "ATTACHDBFILENAME")
                    {
                        datas[1] = items[1];
                        if (datas[0] == "MSSqlServer" && items[0].ToUpper().Trim() == "DATA SOURCE")
                            datas[2] = items[1];
                    }
                    // Para Access y SqlServer necesitamos que este item vaya primero
                    else if (items[0].ToUpper().Trim() == "XPOPROVIDER")
                        datas[0] = items[1];
                    else if (items[0].ToUpper().Trim() == "SERVER")
                        datas[2] = items[1];
                    else if (items[0].ToUpper().Trim() == "USER ID")
                        datas[3] = items[1];
                    else if (items[0].ToUpper().Trim() == "PASSWORD"
                        || items[0].ToUpper().Trim() == "JET OLEDB:DATABASE PASSWORD")
                        datas[4] = items[1];
                    else if (items[0].ToUpper().Trim() == "PORT")
                        datas[5] = items[1];
                }
            }
            return datas;
        }
    }
}
