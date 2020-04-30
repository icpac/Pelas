#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pls
{
	/// <summary>
	/// Description of FReport.
	/// </summary>
	public partial class FReport : Form
	{
		public FReport()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //

            //reportViewer1.Show();
		}

        private void FReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
    }
}
