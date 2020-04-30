#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using DevExpress.Spreadsheet;

namespace Negocio
{
    public class XML
    {
        public void ExportExcel(string path)
        {
            // Recorrer los archivos XML
            // Leer los datos
            // Crear el archivo Excel
            // Guardar la información en el archivo
            Workbook book = new Workbook();
            var sheet = book.Worksheets.ActiveWorksheet;
            string val = string.Empty;

            foreach (string filen in Directory.GetFiles(path, "*.xml"))
            {
                XmlDocument doc = new XmlDocument();

                doc.Load(filen);
                XmlNodeList elementList = doc.GetElementsByTagName("cfdi:Comprobante");

                if (elementList != null && elementList.Count > 0)
                {
                    XmlAttribute att = elementList[0].Attributes["fecha"];
                    if (att != null)
                        val = att.Value;

                    sheet.Cells[0, 0].Value = val;
                }
            }
            string nameExport = $"XML_{DateTime.Today.ToString("dd-MMM-yyyy")}.xlsx";
            book.SaveDocument(nameExport);
            book.Dispose();
        }
    }
}
