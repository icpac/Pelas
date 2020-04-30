using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Poliza : PObject
    {
        public Poliza()
        {
            Partidas = new List<PolizaItem>();
        }

        public string Tipo { get; set; }
        public string Numero { get; set; }
        public short Periodo { get; set; }
        public int Ejercicio { get; set; }
        public DateTime Fecha { get; set; }
        public string Concepto { get; set; }
        public int Cuantas { get; set; }

        public List<PolizaItem> Partidas { get; set; }
    }
}
