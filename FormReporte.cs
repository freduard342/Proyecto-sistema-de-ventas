using CapaDatos;
using CapaEntidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormReporte : Form
    {
        private int idVenta; // ← Esto es necesario
        public FormReporte(int id)
        {
            InitializeComponent();
            idVenta = id;

        }



        private void FormReporte_Load(object sender, EventArgs e)
        {
            VentaDAL ventaDAL = new VentaDAL();
            DataTable dt = ventaDAL.ObtenerVenta(idVenta);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("GAMA", dt));

            reportViewer1.RefreshReport();
        }



    }
}
