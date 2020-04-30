#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.AES
{
    public class AntiguosSaldos
    {
        // public Cliente Cliente { get; set; }
        public string Cliente { get; set; }
        public string Referencia { get; set; }
        public double ImporteFactura { get; set; }
        public DateTime FechaAplicacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        // public Vendedor Vendedor { get; set; }
        public string Vendedor { get; set; }
        public double Pagos { get; set; }
        public double Saldo { get; set; }
    }
}
