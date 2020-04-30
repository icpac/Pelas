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

namespace Negocio
{
    public class PolizaItem : PObject
    {
        public int Partida { get; set; }
        public string Cuenta { get; set; }
        public string Concepto { get; set; }
        public string DebeHaber { get; set; }
        public double Monto { get; set; }
        public short Departamento { get; set; }
        public double TipoCambio { get; set; }
        public int CentroCosto { get; set; }
        public int Proyecto { get; set; }
    }
}
