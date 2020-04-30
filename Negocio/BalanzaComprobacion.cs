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
    public class BalanzaComprobacion : PObject
    {
        public BalanzaComprobacion()
        {
            Numero = string.Empty;
            Nombre = string.Empty;
            PapaNumero = string.Empty;
            SaldoInicial = 0;
            Debe = 0;
            Haber = 0;
            SaldoFinal = 0;
        }

        public int ID { get; set; }
        public int ParentID { get; set; }

        public string Numero { get; set; }
        public string CuentaFrmt { get; set; }
        public string Nombre { get; set; }
        [VisibleView(false)]
        public int Naturaleza { get; set; }
        [VisibleView(false)]
        public int Nivel { get; set; }

        [VisibleView(false)]
        public string PapaNumero { get; set; }
        public double SaldoInicial { get; set; }
        public double Debe { get; set; }
        public double Haber { get; set; }
        public double SaldoFinal { get; set; }
        public string NombreEmpresa { get; set; }
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
    }
}
