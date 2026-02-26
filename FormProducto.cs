using Capa.Negocio;
using CapaEntidades;
using CapaNegocio;
using CapaPresentacion; // Para que reconozca Form3
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace CapaPresentacion
{
    public partial class FormProducto : Form

    {
        ProductoBL blProducto = new ProductoBL();


        private int idProductoSeleccionado = 0;
        public FormProducto()
        {
            InitializeComponent();
            CrearPieDePagina();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form2_Load(object sender, EventArgs e)
        {



            try
            {
                // Traer lista de categorías desde la BL
                CategoriaBL blCategoria = new CategoriaBL();
                List<Categorias> categorias = blCategoria.Listar();

                // Limpiar antes de asignar
                comboBox1.DataSource = null;

                if (categorias != null && categorias.Count > 0)
                {
                    // Primero DisplayMember y ValueMember
                    comboBox1.DisplayMember = "Nombre_Categoria";
                    comboBox1.ValueMember = "Id_Categoria";

                    // Luego asignar DataSource
                    comboBox1.DataSource = categorias;

                    comboBox1.SelectedIndex = -1;
                }

                // Llenar DataGridView con productos
                dataGridView1.DataSource = blProducto.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Asegurar que DataGridView se cargue aunque el ComboBox falle
                try
                {
                    dataGridView1.DataSource = blProducto.Listar();
                }
                catch { }
            }

        }




        private void button2_Click(object sender, EventArgs e)
        {

            LimpiarCampos();
            MessageBox.Show("Campos limpiados.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
        string.IsNullOrWhiteSpace(textBox2.Text) ||
        string.IsNullOrWhiteSpace(textBox3.Text) ||
        comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Debe completar todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBox2.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox3.Text, out int stock))
            {
                MessageBox.Show("El stock debe ser un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Productos prod = new Productos
                {
                    Nombre_Producto = textBox1.Text,
                    Precio_Producto = precio,
                    Stock = stock,
                    Id_Categoria = Convert.ToInt32(comboBox1.SelectedValue) // <- aquí va el ID correcto
                };

                blProducto.Insertar(prod);

                MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                comboBox1.SelectedIndex = -1;

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = blProducto.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
            idProductoSeleccionado = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar este producto?",
                "Confirmar eliminación",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                try
                {
                    // Usar índice 0 de la fila (ID del producto)
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    blProducto.Eliminar(id);

                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refrescar DataGridView
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = blProducto.Listar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void GenerarReporte()
        {
            try
            {
                List<Productos> productos = blProducto.Listar();
                List<Categorias> categorias = new CategoriaBL().Listar();

                if (productos.Count == 0)
                {
                    MessageBox.Show("No hay productos para generar reporte.", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string reporte = "REPORTE DE PRODUCTOS\n\n";

                foreach (var p in productos)
                {
                    string nombreCat = categorias.Find(c => c.Id_Categoria == p.Id_Categoria)?.Nombre_Categoria ?? "Sin categoría";

                    reporte += $"ID: {p.Id_Producto} | Nombre: {p.Nombre_Producto} | Precio: {p.Precio_Producto:C} | Stock: {p.Stock} | Categoría: {nombreCat}\n";
                }

                MessageBox.Show(reporte, "Reporte de Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Producto guardado (lógica pendiente)");
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            GenerarReporte();

        }

        // 📝 PIE DE PÁGINA PROFESIONAL (CENTRADO ABAJO)
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



        private void button4_Click(object sender, EventArgs e)
        {
            // Crear una instancia de Form3 (Ventas)
            FormVenta frmVentas = new FormVenta();

            // Abrir Form3
            frmVentas.Show(); // Se abre sin cerrar Form2
                              // Si quieres que Form3 sea modal (no se pueda usar Form2 hasta cerrarlo), usa:
                              // frmVentas.ShowDialog();

            // Opcional: cerrar Form2 si quieres que solo quede Form3
            // this.Hide();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            FrmCategorias frm = new FrmCategorias();
            frm.ShowDialog();
        }
    }
}
 

