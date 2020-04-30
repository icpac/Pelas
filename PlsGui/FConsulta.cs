using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlsGui
{
    public partial class FConsulta : Form
    {
        public FConsulta()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Filtro();
        }

        virtual protected void Filtro()
        {
            if (FiltroDlg != null && FiltroDlg.ShowDialog() == DialogResult.OK)
                Filtrar();
        }

        virtual protected void Filtrar()
        { }

        virtual protected FFiltroQ FiltroDlg
        { get { return null; } }

        virtual protected void FormatCells()
        {
            foreach (DataGridViewColumn col in dataGridViewCnst.Columns)
            {
                if (col.ValueType == typeof(double))
                    col.DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight };
                else if (col.ValueType == typeof(DateTime))
                    col.DefaultCellStyle = new DataGridViewCellStyle() { Format = "dd/MMM/yy" };

                dataGridViewCnst.ColumnHeaderMouseClick += DataGridViewCnst_ColumnHeaderMouseClick;
            }
        }

        private void DataGridViewCnst_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewColumn col in dataGridViewCnst.Columns)
                dataGridViewCnst.ColumnHeaderMouseClick -= DataGridViewCnst_ColumnHeaderMouseClick;
            Ordenar(e.ColumnIndex);

            foreach (DataGridViewColumn col in dataGridViewCnst.Columns)
                dataGridViewCnst.ColumnHeaderMouseClick += DataGridViewCnst_ColumnHeaderMouseClick;
        }

        protected virtual void Ordenar(int nmCol)
        {
        }
    }
}
