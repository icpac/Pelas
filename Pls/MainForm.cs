#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using iCap;
using Pls.Properties;
using System.Drawing.Printing;
using Negocio;

namespace Pls
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

        void MainFormLoad(object sender, EventArgs e)
        {
            Settings cfgSettings = Properties.Settings.Default;

            toolStripStatusLabelRt.Text = cfgSettings.RutaSAE;
            toolStripStatusLabelRtCoi.Text = cfgSettings.RutaCOI;
            toolStripStatusLabelNmrE.Text = cfgSettings.NmrEmpresa.ToString();
        }

        // Acerca de ...
        void AcercaDeToolStripMenuItemClick(object sender, EventArgs e)
		{
			new FAbout().ShowDialog();
		}
		
        // Salir 
		void SalirToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		

        // Configuracion
		void ConecciónToolStripMenuItemClick(object sender, EventArgs e)
		{
			new FConfiguracion().ShowDialog();
		}

        // Consultas 
        private void productosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FQuerySAE prds = new FQuerySAE() { Tipo = EQuerySAE.Productos };

            prds.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FQuerySAE vtas = new FQuerySAE() { Tipo = EQuerySAE.Ventas };

            vtas.Show();
        }

        private void cOIToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FQueryCOI cnst = new FQueryCOI() { Tipo = EQueryCOI.Polizas };

            cnst.Show();
        }

        private void sAEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FQueryCOI cnst = new FQueryCOI() { Tipo = EQueryCOI.Cuentas };

            cnst.Show();
        }

       
		
        // Reportes
		// Declare the PrintPreviewControl object and the 
		// PrintDocument object.
		internal PrintPreviewControl PrintPreviewControl1;
		private PrintDocument docToPrint = 
		new PrintDocument();		
		void CuentasToolStripMenuItemClick(object sender, EventArgs e)
		{
			/*
			PrintDocument pd = new PrintDocument();
			
			pd.Print();*/
			/*
			InitializePrintPreviewControl();*/
			new FReport().Show();
		}
		
		private void InitializePrintPreviewControl()
		{

			// Construct the PrintPreviewControl.
			this.PrintPreviewControl1 = new PrintPreviewControl();

			// Set location, name, and dock style for PrintPreviewControl1.
			this.PrintPreviewControl1.Location = new Point(88, 80);
			this.PrintPreviewControl1.Name = "PrintPreviewControl1";
			this.PrintPreviewControl1.Dock = DockStyle.Fill;

			// Set the Document property to the PrintDocument 
			// for which the PrintPage event has been handled.
			this.PrintPreviewControl1.Document = docToPrint;

			// Set the zoom to 25 percent.
			this.PrintPreviewControl1.Zoom = 0.50;

			// Set the document name. This will show be displayed when 
			// the document is loading into the control.
			this.PrintPreviewControl1.Document.DocumentName = "d:\\Lodehoy.txt";

			// Set the UseAntiAlias property to true so fonts are smoothed
			// by the operating system.
			this.PrintPreviewControl1.UseAntiAlias = true;

			// Add the control to the form.
			this.Controls.Add(this.PrintPreviewControl1);

			// Associate the event-handling method with the
			// document's PrintPage event.
			this.docToPrint.PrintPage += 
				new System.Drawing.Printing.PrintPageEventHandler(
				docToPrint_PrintPage);
		}
		
		// The PrintPreviewControl will display the document
		// by handling the documents PrintPage event
		private void docToPrint_PrintPage(object sender, PrintPageEventArgs e)
		{
			// Insert code to render the page here.
			// This code will be called when the control is drawn.

			// The following code will render a simple
			// message on the document in the control.
			string text = "In docToPrint_PrintPage method.";
            Font printFont = new Font("Arial", 35, FontStyle.Regular);

			e.Graphics.DrawString(text, printFont,
				Brushes.Black, 10, 10);
        }

        // https://www.youtube.com/watch?v=ToRXCw91r-c
        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FReportePreview().Show();
            /*
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();

            printDialog.Document = printDocument;

            printDocument.PrintPage += PrintDocument_PrintPage;

            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument.Print();
            }*/
        }

        private void vendedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FQuerySAE vtas = new FQuerySAE() { Tipo = EQuerySAE.Vendedores };

            vtas.Show();
        }

        private void antiguedadSaldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FQuerySAE vtas = new FQuerySAE() { Tipo = EQuerySAE.VendedoresSaldos };

            vtas.Show();
        }


        /*
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 12);
            float fontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int offset = 40;

            graphics.DrawString("Reporte de Productos",
                new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY);

            for (int i = 0; i < 10; i++)
            {
                string productDescription = "product.Description.PadRight(30);";
                string productTotal = "string.Format('0:c', product.Price)";

                string productLine = productDescription + productTotal;

                graphics.DrawString(productLine, font,
                    new SolidBrush(Color.Black), startX, startY + offset);

                offset = offset + (int)fontHeight + 5;
            }
            offset = offset + 20;
            graphics.DrawString("Total de productos".PadRight(30)
                + string.Format("0:c", 100.00), font, new SolidBrush(Color.Black), 
                startX, startY + offset);
        }*/
    }
}
