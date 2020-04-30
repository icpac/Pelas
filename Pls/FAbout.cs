/*
 * javier1604@gmail.com
 * Carlos Javier Lopez Cruz
 *
 * User: Tlacaelel
 * Date: 20/02/2018
 * Time: 07:13 a.m.
 * 
 */
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
