#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

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
