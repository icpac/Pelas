#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using Pls.Properties;
using FirebirdSql.Data.FirebirdClient;
using Datos;
using System.Data;

namespace Pls
{
	/// <summary>
	/// Description of FVntsMstrdr.
	/// </summary>
	public partial class FVntsMstrdr : Form
	{
		public FVntsMstrdr()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Button2Click(object sender, EventArgs e)
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

            	string fchIni = dateTimePicker1.Value.ToString("MM/dd/yyyy");
	        	string fchFin = dateTimePicker2.Value.ToString("MM/dd/yyyy");

        		string sql = "select factf01.cve_clpv" +
            		", clie01.nombre" +
            		", factf01.cve_vend" +
					", infenvio01.calle as DatEnvio " + 
					", factf01.cve_doc" + 
					", factf01.fecha_doc" + 
					", factf01.can_tot as Importe" + 
					", factf01.imp_tot1" + 
					", factf01.imp_tot2" + 
					", factf01.imp_tot3" + 
					", factf01.imp_tot4" +
					", (factf01.can_tot + factf01.imp_tot1 + factf01.imp_tot2 + " +
            		"factf01.imp_tot3 + factf01.imp_tot4) as Total " +
            		" From factf01" +
            		" left join clie01  on clie01.clave = factf01.cve_clpv" +
            		" left join infenvio01 on infenvio01.cve_info = factf01.dat_envio " +
					
            		" Where trim(factf01.cve_clpv) between '1' and 'MOSTR' And " +
            			" trim(factf01.cve_vend) between '1' And '99999' And " +
     					" factf01.fecha_doc between '" + fchIni + "' And '" + fchFin + 
            			"' And  factf01.status <> 'C'";
            	
            	DataSet datset = datos.LeeDatos(sql);
                        
            	dataGridView1.DataSource = datset.Tables[0];
            	dataGridView1.AutoGenerateColumns = true;
        	}	
		}
	}
}
