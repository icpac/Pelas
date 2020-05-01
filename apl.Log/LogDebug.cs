#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

/*
 * LogDebug     Escribimos a archivo tipo texto lo que ha ido ocurriendo. 
 *              Tal vez podamos hacer una clase que escriba a base de datos estos eventos.
 */

using System;

namespace apl.Log
{
	using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public enum CajaMensaje
    {
        /// <summary>
        /// Con este par�metro no aparece el Message box
        /// </summary>
        Sin,
        /// <summary>
        /// Muestra el message box en el log debug
        /// </summary>
        Con
    }

    /// <summary>
    /// 
    /// </summary>
    public enum DetalleMensaje
    {
        /// <summary>
        /// Si se quiere el detalle del mensaje: fecha, hora
        /// </summary>
        Con,
        /// <summary>
        /// Sin detalle en el mensaje
        /// </summary>
        Sin
    }

	#region LogDebug
	/// <summary>
	/// Clase para dejar un registro de las operaciones que hace el sistema. 
	/// La idea es ayudar a encontrar los errores que puedan ocurrir.
	/// </summary>
	public class LogDebug
	{
        #region +_ true log, false no log
        private static bool FConLog = true;
        /// <summary>
        /// Se crea o no el archivo de 'succesos'
        /// </summary>
        public static bool ConLog
        {
            get { return FConLog; }
            set { FConLog = value; }
        }
        #endregion

        #region +_ Escribe mensaje
        private static StreamWriter log;
        /// <summary>
		/// Escribe el mensaje al archivo, tambi�n coloca informaci�n de la fecha y la hora.
		/// </summary>
		/// <param name="msg">Texto del mensaje</param>
        public static void Escribe(string msg)
		{
            Escribe(msg, false);
		}
		#endregion

        #region +_ Escribe
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">Texto del mensaje</param>
        /// <param name="box">Con o sin MessageBox</param>
        public static void Escribe(string msg, bool box)
        {
            Escribe(msg, box ? CajaMensaje.Con : CajaMensaje.Sin, DetalleMensaje.Con);
        }
        #endregion

        #region +_ Escribe
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">Texto del mensaje</param>
        /// <param name="mensaje">Muestra o no MessageBox</param>
        /// <param name="detalle">Con detalle: hora y fecha</param>
        public static void Escribe(string msg, CajaMensaje mensaje, DetalleMensaje detalle)
        {
            if (!ConLog)
                return;

            using (log = new StreamWriter(Nombre(), true))
            {
                if (detalle == DetalleMensaje.Con)
                {
                    log.WriteLine("-------------------------------");
                    log.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());
                    log.WriteLine("  :");
                }
                log.WriteLine("  :{0}", msg);

                // Update the underlying file.
                log.Flush();
                log.Close();
            }

            if (mensaje == CajaMensaje.Con)
                MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region -_ Nombre del log file
        private static string Nombre()
		{
			string nf = string.Empty;
			DateTime hoy = DateTime.Now;


			nf = "ic" + hoy.Day.ToString("00");
			nf = nf + hoy.Month.ToString("00");
			nf = nf + hoy.Year;
			nf = nf + ".log";
            nf = Path.Combine(Application.StartupPath, nf);
			return nf;
		}
		#endregion
	}
	#endregion
}
