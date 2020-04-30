/*
 * javier1604@gmail.com
 * Carlos Javier Lopez Cruz
 *
 * User: Tlacaelel
 * Date: 12 /jul /2018
 * Time: 07:21 p. m.
 * 
 */

using Negocio.COI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FiltroCOI
    {
        public FiltroCOI()
        {
            Ejercicio = DateTime.Today.Year;
            SoloConMovs = true;
            Emprss = "19";
        }

        public int Ejercicio { get; set; }
        public EMeses MesD { get; set; }
        public EMeses MesH { get; set; }
        public CentroCosto CentroC { get; set; }
        public Proyecto CProyecto { get; set; }
        public int CentroCId { get; set; }
        public int CProyectoId { get; set; }
        public bool SoloConMovs { get; set; }
        public string Emprss { get; set; }
        public Cuenta Desde { get; set; }
        public Cuenta Hasta { get; set; }



        public override string ToString()
        {
            string aux = string.Format("Ejercicio: {0} / Periodo: {1}",
                Ejercicio, MesD);
            
            if (CentroC != null)
                aux = string.Format("{0} / Unidad d Negocio: {1}", 
                    aux, CentroC.ToString());
            if (CProyecto != null)
                aux = string.Format("{0} / Proyecto: {1}", 
                    aux, CProyecto.ToString());
            return aux;
        }
    }

    public enum EMeses
    {
        Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre
    }
}
