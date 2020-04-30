#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using PlsProGui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Cuenta // : PObject
    {
        public Cuenta()
        {
            Numero = string.Empty;
            Nombre = string.Empty;
            PapaNumero = string.Empty;
            SaldoFinal = 0;
        }

        public int ID { get; set; }
        public int ParentID { get; set; }

        public string Numero { get; set; }
        public string CuentaFrmt { get; set; }
        public string Nombre { get; set; }

        [VisibleView(false)]
        public string PapaNumero { get; set; }
        public double SaldoFinal { get; set; }
    }
}
