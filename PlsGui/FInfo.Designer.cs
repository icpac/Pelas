/*
 * Created by SharpDevelop.
 * User: Tlacaelel
 * Date: 20/02/2018
 * Time: 07:13 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace iCap
{
	partial class FInfo
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.panelAbj = new System.Windows.Forms.Panel();
			this.panelDrch = new System.Windows.Forms.Panel();
			this.buttonAcptr = new System.Windows.Forms.Button();
			this.panelAbj.SuspendLayout();
			this.panelDrch.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelAbj
			// 
			this.panelAbj.Controls.Add(this.panelDrch);
			this.panelAbj.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelAbj.Location = new System.Drawing.Point(0, 222);
			this.panelAbj.Name = "panelAbj";
			this.panelAbj.Size = new System.Drawing.Size(292, 44);
			this.panelAbj.TabIndex = 0;
			// 
			// panelDrch
			// 
			this.panelDrch.Controls.Add(this.buttonAcptr);
			this.panelDrch.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelDrch.Location = new System.Drawing.Point(196, 0);
			this.panelDrch.Name = "panelDrch";
			this.panelDrch.Size = new System.Drawing.Size(96, 44);
			this.panelDrch.TabIndex = 0;
			// 
			// buttonAcptr
			// 
			this.buttonAcptr.Location = new System.Drawing.Point(3, 3);
			this.buttonAcptr.Name = "buttonAcptr";
			this.buttonAcptr.Size = new System.Drawing.Size(75, 23);
			this.buttonAcptr.TabIndex = 0;
			this.buttonAcptr.Text = "Aceptar";
			this.buttonAcptr.UseVisualStyleBackColor = true;
			this.buttonAcptr.Click += new System.EventHandler(this.ButtonAcptrClick);
			// 
			// FInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.panelAbj);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FInfo";
			this.ShowInTaskbar = false;
			this.Text = "FInfo";
			this.panelAbj.ResumeLayout(false);
			this.panelDrch.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button buttonAcptr;
		private System.Windows.Forms.Panel panelDrch;
		private System.Windows.Forms.Panel panelAbj;
	}
}
