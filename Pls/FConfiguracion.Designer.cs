/*
 * javier1604@gmail.com
 * Carlos Javier Lopez Cruz
 *
 * User: Tlacaelel
 * Date: 06/06/2018
 * Time: 03:15 p. m.
 * 
 */
namespace Pls
{
	partial class FConfiguracion
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private PlsGui.TextBoxButton textBoxButtonRt;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label labelPth;
		private System.Windows.Forms.OpenFileDialog openFileDialogBsDts;
		private System.Windows.Forms.ToolTip toolTipPth;
		private System.Windows.Forms.Label labelNmrEmprs;
		private System.Windows.Forms.NumericUpDown numericUpDownNmrEmprs;
		private System.Windows.Forms.GroupBox groupBoxCnxn;
		private System.Windows.Forms.RadioButton radioButtonRmt;
		private System.Windows.Forms.RadioButton radioButtonLcl;
		
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
            this.textBoxButtonRt = new PlsGui.TextBoxButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelPth = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxButtonRtCOI = new PlsGui.TextBoxButton();
            this.labelNmrEmprs = new System.Windows.Forms.Label();
            this.numericUpDownNmrEmprs = new System.Windows.Forms.NumericUpDown();
            this.groupBoxCnxn = new System.Windows.Forms.GroupBox();
            this.radioButtonRmt = new System.Windows.Forms.RadioButton();
            this.radioButtonLcl = new System.Windows.Forms.RadioButton();
            this.openFileDialogBsDts = new System.Windows.Forms.OpenFileDialog();
            this.toolTipPth = new System.Windows.Forms.ToolTip(this.components);
            this.panelDrch.SuspendLayout();
            this.panelAbj.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNmrEmprs)).BeginInit();
            this.groupBoxCnxn.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDrch
            // 
            this.panelDrch.Location = new System.Drawing.Point(272, 0);
            // 
            // panelAbj
            // 
            this.panelAbj.Location = new System.Drawing.Point(0, 229);
            this.panelAbj.Size = new System.Drawing.Size(450, 44);
            // 
            // textBoxButtonRt
            // 
            this.textBoxButtonRt.Location = new System.Drawing.Point(127, 8);
            this.textBoxButtonRt.Name = "textBoxButtonRt";
            this.textBoxButtonRt.Size = new System.Drawing.Size(291, 20);
            this.textBoxButtonRt.TabIndex = 1;
            this.textBoxButtonRt.ButtonClick += new System.EventHandler(this.TextBoxButton1ButtonClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelPth);
            this.flowLayoutPanel1.Controls.Add(this.textBoxButtonRt);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.textBoxButtonRtCOI);
            this.flowLayoutPanel1.Controls.Add(this.labelNmrEmprs);
            this.flowLayoutPanel1.Controls.Add(this.numericUpDownNmrEmprs);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxCnxn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(450, 229);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // labelPth
            // 
            this.labelPth.Location = new System.Drawing.Point(8, 5);
            this.labelPth.Name = "labelPth";
            this.labelPth.Size = new System.Drawing.Size(113, 23);
            this.labelPth.TabIndex = 2;
            this.labelPth.Text = "Ruta de Datos SAE";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ruta de Datos COI";
            // 
            // textBoxButtonRtCOI
            // 
            this.textBoxButtonRtCOI.Location = new System.Drawing.Point(127, 34);
            this.textBoxButtonRtCOI.Name = "textBoxButtonRtCOI";
            this.textBoxButtonRtCOI.Size = new System.Drawing.Size(291, 20);
            this.textBoxButtonRtCOI.TabIndex = 6;
            this.textBoxButtonRtCOI.ButtonClick += new System.EventHandler(this.TextBoxButton1ButtonClick);
            // 
            // labelNmrEmprs
            // 
            this.labelNmrEmprs.Location = new System.Drawing.Point(8, 57);
            this.labelNmrEmprs.Name = "labelNmrEmprs";
            this.labelNmrEmprs.Size = new System.Drawing.Size(113, 23);
            this.labelNmrEmprs.TabIndex = 3;
            this.labelNmrEmprs.Text = "Número de Empresa";
            // 
            // numericUpDownNmrEmprs
            // 
            this.numericUpDownNmrEmprs.Location = new System.Drawing.Point(127, 60);
            this.numericUpDownNmrEmprs.Name = "numericUpDownNmrEmprs";
            this.numericUpDownNmrEmprs.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownNmrEmprs.TabIndex = 4;
            // 
            // groupBoxCnxn
            // 
            this.groupBoxCnxn.Controls.Add(this.radioButtonRmt);
            this.groupBoxCnxn.Controls.Add(this.radioButtonLcl);
            this.groupBoxCnxn.Location = new System.Drawing.Point(8, 86);
            this.groupBoxCnxn.Name = "groupBoxCnxn";
            this.groupBoxCnxn.Size = new System.Drawing.Size(200, 100);
            this.groupBoxCnxn.TabIndex = 5;
            this.groupBoxCnxn.TabStop = false;
            this.groupBoxCnxn.Text = "Conexión";
            this.groupBoxCnxn.Visible = false;
            // 
            // radioButtonRmt
            // 
            this.radioButtonRmt.Location = new System.Drawing.Point(9, 49);
            this.radioButtonRmt.Name = "radioButtonRmt";
            this.radioButtonRmt.Size = new System.Drawing.Size(104, 24);
            this.radioButtonRmt.TabIndex = 1;
            this.radioButtonRmt.Text = "Remota";
            this.radioButtonRmt.UseVisualStyleBackColor = true;
            // 
            // radioButtonLcl
            // 
            this.radioButtonLcl.Checked = true;
            this.radioButtonLcl.Location = new System.Drawing.Point(9, 19);
            this.radioButtonLcl.Name = "radioButtonLcl";
            this.radioButtonLcl.Size = new System.Drawing.Size(104, 24);
            this.radioButtonLcl.TabIndex = 0;
            this.radioButtonLcl.TabStop = true;
            this.radioButtonLcl.Text = "Local";
            this.radioButtonLcl.UseVisualStyleBackColor = true;
            // 
            // openFileDialogBsDts
            // 
            this.openFileDialogBsDts.FileName = "openFileDialog1";
            // 
            // FConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 273);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "FConfiguracion";
            this.Text = "FConfiguracion";
            this.Load += new System.EventHandler(this.FConfiguracionLoad);
            this.Controls.SetChildIndex(this.panelAbj, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.panelDrch.ResumeLayout(false);
            this.panelAbj.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNmrEmprs)).EndInit();
            this.groupBoxCnxn.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        private System.Windows.Forms.Label label1;
        private PlsGui.TextBoxButton textBoxButtonRtCOI;
    }
}
