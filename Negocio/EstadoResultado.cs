/*
 * javier1604@gmail.com
 * Carlos Javier Lopez Cruz
 *
 * User: Tlacaelel
 * Date: 19 / 07/ 2018
 * Time:  1:35 p. m.
 * 
 */

using DevExpress.Xpo;
using Negocio.COI;
using PlsProGui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EstadoResultado
    {
        public EstadoResultado()
        {
            Ingresos = new List<ConceptoResultado>();
            Costos = new List<ConceptoResultado>();
            GastosGenerales = new List<ConceptoResultado>();
            OtrosIngresosGastos = new List<ConceptoResultado>();
            Impuestos = new List<ConceptoResultado>();

            UtilidadBruta = new List<Utilidad>();
            UtilidadOperacion = new List<Utilidad>();
            UtilidadNeta = new List<Utilidad>();
        }

        public List<ConceptoResultado> Ingresos { get; set; }
        public List<ConceptoResultado> Costos { get; set; }
        public List<Utilidad> UtilidadBruta { get; set; }
        public List<ConceptoResultado> GastosGenerales { get; set; }
        public List<Utilidad> UtilidadOperacion { get; set; }
        public List<ConceptoResultado> OtrosIngresosGastos { get; set; }
        public List<ConceptoResultado> Impuestos { get; set; }
        public List<Utilidad> UtilidadNeta { get; set; }
        public string NombreEmpresa { get; set; }
    }

    public class ConceptoResultado
    {
        public ConceptoResultado()
        {
            Numero = string.Empty;
            Nombre = string.Empty;
            PapaNumero = string.Empty;
            SaldoMes = 0;
            Porcentaje = 0;
            Acumulado = 0;
            PorcentajeAcumulado = 0;
            Suma = false;
        }

        [VisibleView(false)]
        public int ID { get; set; }

        [VisibleView(false)]
        public int ParentID { get; set; }

        public string Numero { get; set; }
        public string Nombre { get; set; }
        [VisibleView(false)]
        public short TipoAD { get; set; }
        [VisibleView(false)]
        public bool Suma { get; set; }
        [VisibleView(false)]
        public string PapaNumero { get; set; }
        public double SaldoMes { get; set; }
        public float Porcentaje { get; set; }
        public double Acumulado { get; set; }
        public float PorcentajeAcumulado { get; set; }
        public ETipoConceptoER TipoC { get; set; }
        public ETipoUtilidad TipoU { get; set; }
        public int IdProyecto { get; set; }
        public Proyecto Proyecto { get; set; }
        public CentroCosto Centro { get; set; }


        public double Ingresos { get; set; }
        public double Costos { get; set; }
    }

    public class Utilidad
    {
        public Proyecto Proyecto { get; set; }
        public CentroCosto Centro { get; set; }

        public double SaldoMes { get; set; }
        public float Porcentaje { get; set; }
        public double Acumulado { get; set; }
        public float PorcentajeAcumulado { get; set; }
    }

    public enum ETipoConceptoER
    {
        // No jala
        [DisplayName("1 Ingreso")]
        aIngreso,
        bCosto,
        cGastoGeneral,
        dOtrosIngresosGastos,
        eImpuestos
    }

    public enum ETipoUtilidad
    {
        aBruta,
        bOperacion,
        cNeta
    }
}
