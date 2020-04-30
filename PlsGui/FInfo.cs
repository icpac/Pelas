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
