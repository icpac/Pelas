#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace iCap
{
	/// <summary>
	/// Description of FAbout.
	/// </summary>
	public partial class FAbout : FInfo
	{
		public FAbout()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			StartPosition = FormStartPosition.CenterParent;
		}
				
		void FAboutDeactivate(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("www.icpac.mx");
		}
	}
}
