


using Capa.Negocio;
using CapaEntidades;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CapaDatos;



namespace CapaPresentacion
{
    public partial class FormVenta : Form
    {
        public FormVenta()
        {

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            CrearPieDePagina();
            // ✅ Suscribimos Load
            this.Load += Form3_Load;

            // Campos solo lectura
            textBoxPrecio.ReadOnly = true;
            textBoxTotal.ReadOnly = true;
            textBoxPrecio.Clear();
            textBoxTotal.Clear();
           
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            ConfigurarDataGrid();
            CargarComboBoxes();

        }



        private void ConfigurarDataGrid()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
        


            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("colProducto", "Producto");  // aquí guardaremos Tag con Id
            dataGridView1.Columns.Add("colCategoria", "Categoría");
            dataGridView1.Columns.Add("colCantidad", "Cantidad");
            dataGridView1.Columns.Add("colPrecio", "Precio");      // precio unitario

        }

        private void CargarComboBoxes()
        {
           
        {
            try
            {
                // --- Productos ---
                ProductoBL blProducto = new ProductoBL();
                var productos = blProducto.Listar();
                comboBox1.DataSource = productos;           // comboBox1 = PRODUCTOS
                    comboBox1.DisplayMember = "NombreConStock";
                    comboBox1.ValueMember = "Id_Producto";
                comboBox1.SelectedIndex = -1;

                    // --- Clientes ---
                    ClienteBL blCliente = new ClienteBL();
                    var clientes = blCliente.Listar();

                    comboBox2.DataSource = clientes;   // comboBox2 = CLIENTES
                    comboBox2.DisplayMember = "Nombre"; // propiedad de Cliente
                    comboBox2.ValueMember = "Id_Cliente";
                    comboBox2.SelectedIndex = -1;

                    // --- Categorías ---
                    CategoriaBL blCategoria = new CategoriaBL();
                var categorias = blCategoria.Listar();
                comboBox3.DataSource = categorias;          // comboBox3 = CATEGORÍAS
                comboBox3.DisplayMember = "Nombre_Categoria";
                comboBox3.ValueMember = "Id_Categoria";
                comboBox3.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ComboBoxes: " + ex.Message);
            }
        }
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 ||
         comboBox2.SelectedIndex == -1 ||
         comboBox3.SelectedIndex == -1 ||
         textBox2.Text == "")
            {
                MessageBox.Show("Complete todos los campos");
                return;
            }
          

            var productoSeleccionado = (Productos)comboBox1.SelectedItem;
            int idProducto = productoSeleccionado.Id_Producto;
            int cantidad = Convert.ToInt32(textBox2.Text);

            // 🔹 VALIDAR STOCK ANTES DE AGREGAR
            ProductoDAL dalProducto = new ProductoDAL();
            int stockDisponible = dalProducto.ValidarStock(idProducto, cantidad);


            if (stockDisponible == 0)
            {
                MessageBox.Show("❌ No hay stock disponible para este producto.");
                return;
            }

            if (stockDisponible < cantidad)
            {
                MessageBox.Show($"❌ Solo quedan {stockDisponible} en stock. No puedes vender {cantidad}.");
                return;
            }

            // Si llega aquí → SÍ se puede vender
            decimal precio = productoSeleccionado.Precio_Producto;
            string nombreProducto = productoSeleccionado.Nombre_Producto;
            string categoria = comboBox3.Text;

            int rowIndex = dataGridView1.Rows.Add(nombreProducto, categoria, cantidad, precio);

            // Guardar el ID real del producto en el Tag de la fila
            dataGridView1.Rows[rowIndex].Tag = idProducto;
           
            ActualizarTotalGeneral();

            // Limpiar campos
            comboBox1.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            textBox2.Clear();
        


        }
        private void ActualizarTotalGeneral()
        {
            decimal totalGeneral = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int cantidad = Convert.ToInt32(row.Cells["colCantidad"].Value);
                decimal precio = Convert.ToDecimal(row.Cells["colPrecio"].Value);

                totalGeneral += cantidad * precio;
            }

            // Si tienes un Label llamado lblTotalGeneral, úsalo:
            // lblTotalGeneral.Text = totalGeneral.ToString("C");

            MessageBox.Show($"Total de la venta: {totalGeneral:C}", "Total", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1 || dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente y agregue productos.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idCliente = Convert.ToInt32(comboBox2.SelectedValue);

                Venta venta = new Venta();
                venta.Id_cliente = idCliente;
                venta.Estado_venta = "Pendiente";
                venta.Total_general = 0;
                venta.Detalle = new List<DetalleVenta>();

                // Recorrer el carrito (DataGridView)
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int idProducto = Convert.ToInt32(row.Tag);
                    int cantidad = Convert.ToInt32(row.Cells["colCantidad"].Value);
                    decimal precio = Convert.ToDecimal(row.Cells["colPrecio"].Value);

                    venta.Total_general += cantidad * precio;

                    venta.Detalle.Add(new DetalleVenta
                    {
                        Id_producto = idProducto,
                        Cantidad = cantidad,
                        Precio = precio
                    });
                }

                // Guardar venta en BD
                VentaDAL dal = new VentaDAL();
                int idVenta = dal.InsertarVenta(venta);

                // 🔹 AHORA RESTAMOS EL STOCK
                ProductoDAL dalProducto = new ProductoDAL();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int idProducto = Convert.ToInt32(row.Tag);
                    int cantidadVendida = Convert.ToInt32(row.Cells["colCantidad"].Value);

                    dalProducto.ActualizarStock(idProducto, cantidadVendida);
                }

                MessageBox.Show($"Venta realizada correctamente. ID: {idVenta}",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar todo para nueva venta
                dataGridView1.Rows.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                textBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la venta: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 🔄 Refrescar productos después de vender
                CargarComboBoxes();

            }
        }

        private void btnVerReportes_Click(object sender, EventArgs e)
        {
            try
            {
                VentaDAL dal = new VentaDAL();
                var ventas = dal.ListarVentas();

                string reporte = "REPORTE DE VENTAS\n\n";

                foreach (var v in ventas)
                {
                    // Nombre del cliente
                    string cliente = ""; // si quieres sacar el nombre del cliente, necesitas agregarlo a la clase Venta
                    reporte += $"Cliente ID: {v.Id_cliente} | Total: {v.Total_general:C} | Estado: {v.Estado_venta}\n";

                    foreach (var det in v.Detalle)
                    {
                        reporte += $"   Producto ID: {det.Id_producto} | Cantidad: {det.Cantidad} | Precio: {det.Precio:C} | Subtotal: {det.Cantidad * det.Precio:C}\n";
                    }

                    reporte += "\n";
                }

                MessageBox.Show(reporte, "Reporte de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CalcularTotalLinea()
        {
            if (comboBox1.SelectedIndex == -1 || string.IsNullOrWhiteSpace(textBox2.Text))
                return;

            try
            {
                var productoSeleccionado = (Productos)comboBox1.SelectedItem;
                decimal precio = productoSeleccionado.Precio_Producto;
                int cantidad = Convert.ToInt32(textBox2.Text);

                decimal total = precio * cantidad;

                textBoxPrecio.Text = precio.ToString("F2");
                textBoxTotal.Text = total.ToString("F2");
            }
            catch
            {
                textBoxTotal.Clear();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalLinea();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                textBoxPrecio.Clear();
                textBoxTotal.Clear();
                return;
            }

            var productoSeleccionado = (Productos)comboBox1.SelectedItem;
            textBoxPrecio.Text = productoSeleccionado.Precio_Producto.ToString("0.00");

            // Si ya hay cantidad escrita, recalcular total
            CalcularTotalLinea();
        }

        private void button4_Click(object sender, EventArgs e)
        {
        
            // Crear el formulario Form5
            FormVentas frmVentas = new FormVentas();

            // Abrirlo como ventana modal
            frmVentas.ShowDialog();
        }
        private void CrearPieDePagina()
        {
            Panel panelPie = new Panel();
            panelPie.Dock = DockStyle.Bottom;
            panelPie.Height = 35;
            panelPie.BackColor = Color.FromArgb(240, 240, 240);

            Label lblPie = new Label();
            lblPie.Text = "Desarrollado por: Freduard Rojas | Sistema de Ventas v1.0";
            lblPie.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblPie.ForeColor = Color.DimGray;
            lblPie.Dock = DockStyle.Fill;
            lblPie.TextAlign = ContentAlignment.MiddleCenter;

            panelPie.Controls.Add(lblPie);
            this.Controls.Add(panelPie);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            FormProducto frm = new FormProducto();
            frm.Show();
            this.Close();
        }

    }
}





    
