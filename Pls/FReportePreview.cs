#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pls
{
    public partial class FReportePreview : Form
    {
        public FReportePreview()
        {
            InitializeComponent();
        }

        private void FReportePreview_Load(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();

            printPreviewControl1.Document = printDocument;

            printDocument.PrintPage += PrintDocument_PrintPage;

            /*
            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {*/
                printDocument.Print();
            //}
        }

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
        }
    }
}
