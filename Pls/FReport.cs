﻿/*
 * javier1604@gmail.com
 * Carlos Javier Lopez Cruz
 *
 * User: Tlacaelel
 * Date: 17/06/2018
 * Time: 07:17 p. m.
 * 
 */
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
