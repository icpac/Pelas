#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

namespace iCap
{
	/// <summary>
	/// Description of FInfo.
	/// </summary>
	public partial class FInfo : FDialog
	{
		public FInfo()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ButtonAcptrClick(object sender, EventArgs e)
		{
			Close();
		}
	}
}
