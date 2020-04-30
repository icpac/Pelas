using PlsProGui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BalanceGeneral
    {
        public BalanceGeneral()
        {
            ActivoNoCirculante = new List<CuentaBalanceGeneral>();
            ActivoCirculante = new List<CuentaBalanceGeneral>();
            PasivoACortoPlazo = new List<CuentaBalanceGeneral>();
            PasivoALargoPlazo = new List<CuentaBalanceGeneral>();
            CapitalContable = new List<CuentaBalanceGeneral>();
        }   

        public List<CuentaBalanceGeneral> ActivoNoCirculante { get; set; }
        public List<CuentaBalanceGeneral> ActivoCirculante { get; set; }
        public List<CuentaBalanceGeneral> PasivoACortoPlazo { get; set; }
        public List<CuentaBalanceGeneral> PasivoALargoPlazo { get; set; }
        public List<CuentaBalanceGeneral> CapitalContable { get; set; }

        // Tenemos dos opciones 
        // Una hacer estos 4 grupos con objetos
        // o con el tipo Enum y agrupar la vista con ellos.
        // Algo asi quería hacer en el Estado de Resultados
        // Pero ahi lo hice usando un group
    }

    public class CuentaBalanceGeneral
    {
        public CuentaBalanceGeneral()
            : this(string.Empty, 0, 0, 0)
        {
        }

        public CuentaBalanceGeneral(string nombre, double saldoMes, decimal mesAnterior, float porcentaje)
        {
            Nombre = nombre;
            SaldoMes = saldoMes;
            MesAnterior = mesAnterior;
            Porcentaje = porcentaje;
        }

        [VisibleView(false)]
        public int ID { get; set; }

        [VisibleView(false)]
        public int ParentID { get; set; }
        [VisibleView(false)]
        public string Numero { get; set; }
        public string Nombre { get; set; }
        [VisibleView(false)]
        public string PapaNumero { get; set; }
        public double SaldoMes { get; set; }
        public decimal MesAnterior { get; set; }
        public float Porcentaje { get; set; }

        public EAPC TpAPC { get; set; }
    }

    public enum EAPC
    {
        Activo,
        PasivoCapitalContable
    }
}
