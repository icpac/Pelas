#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using Datos;
using FirebirdSql.Data.FirebirdClient;
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
    public partial class FQuerySAE : FConsulta
    {
        public FQuerySAE()
        {
            InitializeComponent();
        }

        public EQuerySAE Tipo { get; set; }

        protected override void Filtrar()
        {
            Query(Tipo/*Undo und*/);
        }

        protected override FFiltroQ FiltroDlg => new FFiltroSae();

        Properties.Settings stgs;
        public void Query(EQuerySAE tp)
        {
            stgs = new Properties.Settings();

            stgs.Reload();
            if (string.IsNullOrEmpty(stgs.RutaSAE))
            {
                MessageBox.Show("Falta la ruta de datos");
            }
            else
            {
                switch (tp)
                {
                    case EQuerySAE.Productos:
                        dataGridViewCnst.DataSource = Pelas.Productos(stgs.RutaSAE, stgs.NmrEmpresa/*.ToString()*/);
                        break;
                    case EQuerySAE.Ventas:
                        dataGridViewCnst.DataSource = Pelas.Ventas(stgs.RutaSAE, stgs.NmrEmpresa/*.ToString()*/);
                        break;
                    case EQuerySAE.VendedoresSaldos:
                        dataGridViewCnst.DataSource = Pelas.VendedoresSaldos(stgs.RutaSAE, stgs.NmrEmpresa);
                        break;
                }
                FormatCells();
            }
        }

        protected override void Ordenar(int nmCol)
        {
            // base.Ordenar(nmCol);
            switch (Tipo)
            {
                case EQuerySAE.Ventas:
                    (dataGridViewCnst.DataSource as List<Ventas>).Sort((x, y) => x.ClienteNombre.CompareTo(y.ClienteNombre));
                    break;
                case EQuerySAE.Productos:
                    if (nmCol == 0)
                        (dataGridViewCnst.DataSource as List<Inve>).Sort((x, y) => x.Clave.CompareTo(y.Clave));
                    else
                        (dataGridViewCnst.DataSource as List<Inve>).Sort((x, y) => x.Descripcion.CompareTo(y.Descripcion));
                    break;
            }
            dataGridViewCnst.Refresh();
        }
    }
}
