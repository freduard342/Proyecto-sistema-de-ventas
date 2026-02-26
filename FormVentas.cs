using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;




namespace CapaPresentacion
{
    public partial class FormVentas : Form
    {
        VentaBL bl = new VentaBL();
        public FormVentas()
        {
            InitializeComponent();
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            CrearPieDePagina();

            this.WindowState = FormWindowState.Maximized;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bl.ListarVentas();


            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;


            // Ocultar columna Estado_venta
            if (dataGridView1.Columns["Estado_venta"] != null)
                dataGridView1.Columns["Estado_venta"].Visible = false;

            // Ocultar columna Id_cliente
            if (dataGridView1.Columns.Contains("Id_cliente"))
            {
                dataGridView1.Columns["Id_cliente"].Visible = false;
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idVenta = Convert.ToInt32(
                    dataGridView1.Rows[e.RowIndex].Cells["Id_Venta"].Value);

                FormReporte frm = new FormReporte(idVenta);
                frm.ShowDialog();
            }
        }





        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int idVenta = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id_Venta"].Value);
                FormReporte frm = new FormReporte(idVenta); // ✅ Pasando el Id
                frm.ShowDialog();
            }
        }
        // 📝 PIE DE PÁGINA PROFESIONAL (CENTRADO ABAJO)
        private void CrearPieDePagina()
        {
            Label lblPie = new Label();
            lblPie.Text = "Desarrollado por: Freduard Rojas | Sistema de Ventas v1.0";
            lblPie.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblPie.ForeColor = Color.Gray;
            lblPie.AutoSize = false;
            lblPie.Width = this.ClientSize.Width;
            lblPie.Height = 30;
            lblPie.TextAlign = ContentAlignment.MiddleCenter;
            lblPie.Left = 0;
            lblPie.Top = this.ClientSize.Height - 30;
            lblPie.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            this.Controls.Add(lblPie);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            FormVenta frm = new FormVenta();
            frm.Show();
            this.Close();
        }
       
        }

       
            }
        
    
