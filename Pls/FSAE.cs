#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using Datos;
using PlsGui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using Pls.Properties;

namespace Pls
{
    public partial class FSAE : FConsulta
    {
        public FSAE()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        	Settings stgs = new Settings();
        	
        	stgs.Reload();
        	if (string.IsNullOrEmpty(stgs.RutaSAE))
        	{
        		MessageBox.Show("Falta la ruta de datos");
        	}
        	else
        	{
            	FireBird datos = new FireBird();

            	(datos.CadenaConn as FbConnectionStringBuilder).Database = 
            		new Settings().RutaSAE;

            	string sql = "select * From inve01 order by cve_art asc";
            
            	DataSet datset = datos.LeeDatos(sql);
                        
            	AddColumns(datset.Tables[0]);
            	dataGridViewCnst.DataSource = datset.Tables[0];
            	dataGridViewCnst.AutoGenerateColumns = true;
            	/*
            	if (datos.PruebaConexion())
                	MessageBox.Show("bien");*/
        	}
        }
        
        private void AddColumns(DataTable tabla)
        {
        	((ISupportInitialize)(dataGridViewCnst)).BeginInit();

        	DataGridViewTextBoxColumn vcol = null;
        	foreach (DataColumn col in tabla.Columns)
        	{
        		vcol = new DataGridViewTextBoxColumn();
        		
        		vcol.DataPropertyName = col.ColumnName;
        		vcol.HeaderText = col.ColumnName;
        		vcol.Name = col.ColumnName;
        		vcol.ReadOnly = true;
        		
        		dataGridViewCnst.Columns.Add(vcol);
        		break;
        	}
        	((ISupportInitialize)(dataGridViewCnst)).EndInit();
        }
    }
}
