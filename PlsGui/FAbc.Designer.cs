/*
 * Created by SharpDevelop.
 * User: Tlacaelel
 * Date: 06/03/2018
 * Time: 07:31 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace iCap
{
	partial class FAbc
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
            this.components = new System.ComponentModel.Container();
            this.panelAbj = new System.Windows.Forms.Panel();
            this.panelDrch = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelAbj.SuspendLayout();
            this.panelDrch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            this.panelDrch.Controls.Add(this.buttonSave);
            this.panelDrch.Controls.Add(this.buttonCancel);
            this.panelDrch.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDrch.Location = new System.Drawing.Point(114, 0);
            this.panelDrch.Name = "panelDrch";
            this.panelDrch.Size = new System.Drawing.Size(178, 44);
            this.panelDrch.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(3, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Guardar";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(84, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FAbc
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.panelAbj);
            this.Name = "FAbc";
            this.Text = "FAbc";
            this.panelAbj.ResumeLayout(false);
            this.panelDrch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

		}
		protected System.Windows.Forms.Button buttonCancel;
		protected System.Windows.Forms.Button buttonSave;
		protected System.Windows.Forms.Panel panelDrch;
		protected System.Windows.Forms.Panel panelAbj;
		protected System.Windows.Forms.ErrorProvider errorProvider1;
	}
}
