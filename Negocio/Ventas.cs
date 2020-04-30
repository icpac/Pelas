using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Ventas : PObject
    {
        public string Cliente { get; set; }
        public string ClienteNombre { get; set; }

        public string Vendedor { get; set; }

        public string Envio { get; set; }

        public string FolioDocumento { get; set; }

        public DateTime FechaDocumento { get; set; }

        public double SubTotal { get; set; }

        public double ImpuestoTotal1 { get; set; }
        public double ImpuestoTotal2 { get; set; }
        public double ImpuestoTotal3 { get; set; }
        public double ImpuestoTotal4 { get; set; }

        public double Porcentaje { get; set; }
        public double Total { get; set; }
    }
}
