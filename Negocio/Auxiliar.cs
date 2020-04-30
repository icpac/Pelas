/*
 * javier1604@gmail.com
 * Carlos Javier Lopez Cruz
 *
 * User: Tlacaelel
 * Date: 26 / 07/ 2018
 * Time: 12:09 p. m.
 * 
 */

using Negocio.COI;
using PlsProGui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Auxiliar
    {
        public Auxiliar()
        {
            Partidas = new List<AuxiliarPoliza>();
            ConMovs = false;
            IdProyecto = 0;
            Child = new List<Auxiliar>();
        }

        [VisibleView(false)]
        public int ID { get; set; }
        [VisibleView(false)]
        public int ParentID { get; set; }
        [VisibleView(false)]
        public string PapaNumero { get; set; }

        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }

        [VisibleView(false)]
        public string Cuenta { get; set; }
        public string Tipo { get; set; }
        public string CuentaFrmt { get; set; }
        public string Descripcion { get; set; }
        public double SaldoInicial { get; set; }
        public double Cargos { get; set; }
        public double Abonos { get; set; }
        public double SaldoFinal { get; set; }
        public bool ConMovs { get; set; }
        public List<Auxiliar> Child { get; set; }
        public List<AuxiliarPoliza> Partidas { get; set; }

        public int Grupo { get; set; }
        public string NombreEmpresa { get; set; }
    }

    public class AuxiliarPoliza
    {
        public string Tipo { get; set; }
        public string Numero { get; set; }

        public DateTime Fecha { get; set; }
        public string Concepto { get; set; }
        [VisibleView(false)]
        public double SaldoInicial { get; set; }
        public double Cargos { get; set; }
        public double Abonos { get; set; }
        public double SaldoFinal { get; set; }
    }
}
