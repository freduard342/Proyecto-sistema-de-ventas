using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa.Negocio;
using CapaEntidades;

namespace CapaPresentacion
{
   
    public partial class FrmCategorias : Form
    {
        private CategoriaBL bl = new CategoriaBL();
        private int idSeleccionado = 0;

        public FrmCategorias()
        {
            InitializeComponent();
            Listar();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Listar()
        {
          dgvCategoria.DataSource = bl.Listar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Categorias c = new Categorias
            {
                Id_Categoria = idSeleccionado,
                Nombre_Categoria = txtNombre1.Text,
            };

            bl.Agregar(c);
            Limpiar();
            Listar();
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idSeleccionado = Convert.ToInt32(
                dgvCategoria.CurrentRow.Cells["Id_Categoria"].Value);

            txtNombre1.Text =
                dgvCategoria.CurrentRow.Cells["Nombre_Categoria"].Value.ToString();
            txtNombre2.Text =
             dgvCategoria.CurrentRow.Cells["Nombre_Categoria"].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione una categoría primero.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea eliminar esta categoría?",
                "Confirmar eliminación",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                bl.Eliminar(idSeleccionado);
                MessageBox.Show("Categoría eliminada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Limpiar();
                Listar();
            }

        }

        private void Limpiar()
        {
            idSeleccionado = 0;
            txtNombre1.Clear();
            txtNombre2.Clear();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormProducto frm = new FormProducto();
            frm.Show();


        }

        private void dgvCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
