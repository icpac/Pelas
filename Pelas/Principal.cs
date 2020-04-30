#region TIT
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     Tlacaelel Icpac                                               }
{     tlacaelel.icpac@gmail.com                                     }
{                                                                   }
{*******************************************************************}
 */
#endregion
/* Utilerias en General
 */
using apl.Log;
using Negocio;
using Negocio.EAS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pelas
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDe frm = new AcercaDe();

            frm.ShowDialog();
        }

        List<Cliente> Lista;
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (new CWaitCursor())
            {
                Lista = Admin.Clientes(@"D:\Proyectos\Pelas\SAE60CENTRO01.FDB", 1);

                FClientes consulta = new FClientes();

                consulta.Datos(Lista);
                consulta.Show();
            }
        }
    }
}
