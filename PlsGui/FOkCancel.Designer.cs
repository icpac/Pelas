namespace PlsGui
{
    partial class FOkCancel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelAbj = new System.Windows.Forms.Panel();
            this.panelDrch = new System.Windows.Forms.Panel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCnclr = new System.Windows.Forms.Button();
            this.panelAbj.SuspendLayout();
            this.panelDrch.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAbj
            // 
            this.panelAbj.Controls.Add(this.panelDrch);
            this.panelAbj.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAbj.Location = new System.Drawing.Point(0, 151);
            this.panelAbj.Name = "panelAbj";
            this.panelAbj.Size = new System.Drawing.Size(366, 40);
            this.panelAbj.TabIndex = 0;
            // 
            // panelDrch
            // 
            this.panelDrch.Controls.Add(this.buttonCnclr);
            this.panelDrch.Controls.Add(this.buttonOk);
            this.panelDrch.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDrch.Location = new System.Drawing.Point(194, 0);
            this.panelDrch.Name = "panelDrch";
            this.panelDrch.Size = new System.Drawing.Size(172, 40);
            this.panelDrch.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(3, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Aceptar";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCnclr
            // 
            this.buttonCnclr.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCnclr.Location = new System.Drawing.Point(84, 3);
            this.buttonCnclr.Name = "buttonCnclr";
            this.buttonCnclr.Size = new System.Drawing.Size(75, 23);
            this.buttonCnclr.TabIndex = 1;
            this.buttonCnclr.Text = "Cancelar";
            this.buttonCnclr.UseVisualStyleBackColor = true;
            // 
            // FOkCancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 191);
            this.Controls.Add(this.panelAbj);
            this.Name = "FOkCancel";
            this.Text = "FOkCancel";
            this.panelAbj.ResumeLayout(false);
            this.panelDrch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAbj;
        private System.Windows.Forms.Panel panelDrch;
        private System.Windows.Forms.Button buttonCnclr;
        private System.Windows.Forms.Button buttonOk;
    }
}