#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 *
 * https://stackoverflow.com/questions/15868817/button-inside-a-winforms-textbox
 */
#endregion

using System;
using System.Windows.Forms;
using System.Drawing;

namespace PlsGui
{
	/// <summary>
	/// Description of TextBoxButton.
	/// </summary>
	public class TextBoxButton : TextBox
	{
		public TextBoxButton()
		{
        	_button = new Button {Cursor = Cursors.Default};
        	_button.SizeChanged += (o, e) => OnResize(e);
        	_button.Dock = DockStyle.Right;
        	_button.BackColor = SystemColors.Control;
        	this._button.Width = 21;
        	this.Controls.Add(_button);
    	}

		private readonly Button _button;

   	 	public event EventHandler ButtonClick 
   	 	{ 
   	 		add { _button.Click += value; } 
   	 		remove { _button.Click -= value; }
   	 	}

    	public Button Button 
    	{
        	get { return _button; }
    	}

    	protected override void OnResize(EventArgs e) 
    	{
        	base.OnResize(e);
        	_button.Size = new Size(_button.Width, this.ClientSize.Height + 2);
        	_button.Location = new Point(this.ClientSize.Width - _button.Width, -1);
        	// Send EM_SETMARGINS to prevent text from disappearing underneath the button
        	SendMessage(this.Handle, 0xd3, (IntPtr)2, (IntPtr)(_button.Width << 16));
    	}

    	[System.Runtime.InteropServices.DllImport("user32.dll")]
    	private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    	
		protected override void WndProc(ref Message m)
    	{
        	base.WndProc(ref m);

        	switch (m.Msg)
        	{
            	case 0x30:
            	    int num = this._button.Width + 3;
            	    SendMessage(this.Handle, 0xd3, (IntPtr)2, (IntPtr)(_button.Width << 16));
            	    return;
        	}
    	}	
		
	}
}
