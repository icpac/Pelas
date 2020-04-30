using Negocio.EAS;
using PlsGui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Pelas
{
    public partial class FClientes : FConsulta
    {
        public FClientes()
        {
            InitializeComponent();
        }

        public void Datos (List<Cliente> lista)
        {
            // bindingSource1.DataSource = lista;
        }
    }
}
