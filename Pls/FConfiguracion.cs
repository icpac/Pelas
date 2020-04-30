#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Windows.Forms;
using iCap;
using Pls.Properties;
using PlsGui;

namespace Pls
{
	/// <summary>
	/// Description of FConfiguracion.
	/// </summary>
	public partial class FConfiguracion : FAbc
	{
		public FConfiguracion()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			numericUpDownNmrEmprs.Minimum = 1;
		}

		void TextBoxButton1ButtonClick(object sender, EventArgs e)
		{
            TextBoxButton txt = (sender as Control).Parent as TextBoxButton;

            openFileDialogBsDts.Title = "Base de Datos";
            if (openFileDialogBsDts.ShowDialog() == DialogResult.OK)
			{
				txt.Text = openFileDialogBsDts.FileName;
				toolTipPth.SetToolTip(txt, txt.Text);
			}
		}
		
		private Settings cfgSettings = Properties.Settings.Default;
		void FConfiguracionLoad(object sender, EventArgs e)
		{
//Properties.Settings.Default["SomeProperty"] = "Some Value";
//Properties.Settings.Default.Save(); // Saves settings in application configuration file
//Data bind settings properties with straightforward associations.

        	Binding bndRuta = new Binding("Text", cfgSettings, 
            	"RutaSAE", true, DataSourceUpdateMode.OnPropertyChanged);
        	this.textBoxButtonRt.DataBindings.Add(bndRuta);

            Binding bndRutaCoi = new Binding("Text", cfgSettings,
                "RutaCOI", true, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxButtonRtCOI.DataBindings.Add(bndRutaCoi);

            Binding bndNumE = new Binding("Value", cfgSettings,
        	    "NmrEmpresa", true, DataSourceUpdateMode.OnPropertyChanged);
        	this.numericUpDownNmrEmprs.DataBindings.Add(bndNumE);

        	Binding bndLoc = new Binding("Checked", cfgSettings,
        	    "Local", true, DataSourceUpdateMode.OnPropertyChanged);
        	this.radioButtonLcl.DataBindings.Add(bndLoc);

        	this.radioButtonRmt.Checked = !this.radioButtonLcl.Checked;
		}
		
		protected override void Guardar()
		{
			base.Guardar();
			
			cfgSettings.Remota = !cfgSettings.Local;
			cfgSettings.Save();
			Close();
		}
	}
}
