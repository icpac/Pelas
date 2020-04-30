/*
 * javier1604@gmail.com
 * Carlos Javier Lopez Cruz
 *
 * User: Tlacaelel
 * Date: 06/03/2018
 * Time: 07:31 p.m.
 * 
 */
using System;

namespace iCap
{
	/// <summary>
	/// Description of FAbc.
	/// </summary>
	public partial class FAbc : FDialog
	{
		public FAbc()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        protected virtual void Guardar()
        {
        }
    }
}
