#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using Negocio;
using PlsGui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pls
{
    public partial class FQueryCOI : FConsulta
    {
        public FQueryCOI()
        {
            InitializeComponent();
        }

        public EQueryCOI Tipo { get; set; }

        protected override void Filtrar()
        {
            Query(Tipo);
        }

        protected override FFiltroQ FiltroDlg => new FFiltroCOI();

        Properties.Settings stgs;
        public void Query(EQueryCOI tp)
        {
            stgs = new Properties.Settings();

            stgs.Reload();
            if (string.IsNullOrEmpty(stgs.RutaCOI))
            {
                MessageBox.Show("Falta la ruta de datos");
            }
            else
            {
                switch (tp)
                {
                    case EQueryCOI.Cuentas:
                        dataGridViewCnst.DataSource = Aspel.CuentasCOI(stgs.RutaCOI, null);
                        break;
                    case EQueryCOI.Polizas:
                        dataGridViewCnst.DataSource = Aspel.Polizas(stgs.RutaCOI);
                        break;
                }
                FormatCells();
            }
        }
    }
}
