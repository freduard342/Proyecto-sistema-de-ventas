using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Form4 : Form
    {
        private Timer reloj;
        private Label lblHora;

        public Form4()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CrearBarraSuperior();
            CrearEncabezado();
            CrearSubtitulo();
            CrearLineaDecorativa();
            CrearTarjetas();
            CrearReloj();
            CrearPieDePagina();
        }

        // 🎨 ESTILO GENERAL DEL FORMULARIO (PANTALLA COMPLETA)
        private void ConfigurarFormulario()
        {
            this.BackColor = Color.FromArgb(15, 15, 18);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized; // 🔥 Pantalla completa
            this.Text = "Sistema de Ventas - Panel Principal";
        }

        // 🟧 BARRA SUPERIOR TIPO HEADER
        private void CrearBarraSuperior()
        {
            Panel barra = new Panel();
            barra.Width = this.ClientSize.Width;
            barra.Height = 50;
            barra.Top = 0;
            barra.Left = 0;
            barra.BackColor = Color.FromArgb(255, 120, 0);
            barra.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label lblBarra = new Label();
            lblBarra.Text = "PANEL PRINCIPAL - SISTEMA DE VENTAS";
            lblBarra.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblBarra.ForeColor = Color.Black;
            lblBarra.AutoSize = true;
            lblBarra.Left = 20;
            lblBarra.Top = 15;

            barra.Controls.Add(lblBarra);
            this.Controls.Add(barra);
        }

        // 🏷️ TÍTULO GRANDE Y MODERNO (CENTRADO)
        private void CrearEncabezado()
        {
            Label lblTitulo = new Label();
            lblTitulo.Text = "SISTEMA DE VENTAS";
            lblTitulo.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(255, 140, 0);
            lblTitulo.AutoSize = true;
            lblTitulo.Top = 80;
            lblTitulo.Left = (this.ClientSize.Width / 2) - 240;
            lblTitulo.Anchor = AnchorStyles.Top;

            this.Controls.Add(lblTitulo);
        }

        // 📌 SUBTÍTULO (CENTRADO)
        private void CrearSubtitulo()
        {
            Label lblSubtitulo = new Label();
            lblSubtitulo.Text = "Panel de administración y gestión comercial";
            lblSubtitulo.Font = new Font("Segoe UI", 11, FontStyle.Italic);
            lblSubtitulo.ForeColor = Color.LightGray;
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Top = 130;
            lblSubtitulo.Left = (this.ClientSize.Width / 2) - 180;
            lblSubtitulo.Anchor = AnchorStyles.Top;

            this.Controls.Add(lblSubtitulo);
        }

        // ✨ LÍNEA DECORATIVA (CENTRADA)
        private void CrearLineaDecorativa()
        {
            Panel linea = new Panel();
            linea.Width = 300;
            linea.Height = 4;
            linea.Left = (this.ClientSize.Width / 2) - 150;
            linea.Top = 160;
            linea.BackColor = Color.FromArgb(255, 140, 0);
            linea.Anchor = AnchorStyles.Top;

            this.Controls.Add(linea);
        }

        // 🟦 TARJETAS (DASHBOARD)
        private void CrearTarjetas()
        {
            Panel cardCategorias = CrearTarjeta(
                "Categorías",
                "📂",
                (this.ClientSize.Width / 2) - 360, 220,
                Color.FromArgb(25, 25, 30),
                Color.White
            );
            cardCategorias.Anchor = AnchorStyles.Top;
            cardCategorias.Click += BtnCategorias_Click;
            this.Controls.Add(cardCategorias);

            Panel cardProductos = CrearTarjeta(
                "Productos",
                "📦",
                (this.ClientSize.Width / 2) - 110, 220,
                Color.FromArgb(255, 140, 0),
                Color.Black
            );
            cardProductos.Anchor = AnchorStyles.Top;
            cardProductos.Click += BtnProductos_Click;
            this.Controls.Add(cardProductos);

            Panel cardVentas = CrearTarjeta(
                "Ventas",
                "🧾",
                (this.ClientSize.Width / 2) + 140, 220,
                Color.FromArgb(25, 25, 30),
                Color.White
            );
            cardVentas.Anchor = AnchorStyles.Top;
            cardVentas.Click += BtnVentas_Click;
            this.Controls.Add(cardVentas);
        }

        // 🔧 MÉTODO PARA CREAR TARJETAS CON ÍCONOS
        private Panel CrearTarjeta(string titulo, string icono, int left, int top, Color fondo, Color textoColor)
        {
            Panel panel = new Panel();
            panel.Width = 220;
            panel.Height = 180;
            panel.Left = left;
            panel.Top = top;
            panel.BackColor = fondo;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Cursor = Cursors.Hand;

            Label lblIcono = new Label();
            lblIcono.Text = icono;
            lblIcono.Font = new Font("Segoe UI Emoji", 50, FontStyle.Bold);
            lblIcono.ForeColor = textoColor;
            lblIcono.AutoSize = true;
            lblIcono.Left = 70;
            lblIcono.Top = 10;

            Label lblTexto = new Label();
            lblTexto.Text = titulo;
            lblTexto.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTexto.ForeColor = textoColor;
            lblTexto.AutoSize = true;
            lblTexto.Left = 70;
            lblTexto.Top = 120;

            panel.Controls.Add(lblIcono);
            panel.Controls.Add(lblTexto);

            // ✨ EFECTO HOVER
            panel.MouseEnter += (s, e) =>
            {
                panel.BackColor = Color.White;
                lblTexto.ForeColor = Color.Black;
                lblIcono.ForeColor = Color.Black;
            };

            panel.MouseLeave += (s, e) =>
            {
                panel.BackColor = fondo;
                lblTexto.ForeColor = textoColor;
                lblIcono.ForeColor = textoColor;
            };

            return panel;
        }

        // ⏰ RELOJ EN TIEMPO REAL (ABAJO IZQUIERDA)
        private void CrearReloj()
        {
            lblHora = new Label();
            lblHora.Name = "lblHora";
            lblHora.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblHora.ForeColor = Color.LightGray;
            lblHora.AutoSize = true;
            lblHora.Left = 20;
            lblHora.Top = this.ClientSize.Height - 60;
            lblHora.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

            this.Controls.Add(lblHora);

            reloj = new Timer();
            reloj.Interval = 1000;
            reloj.Tick += (s, e) =>
            {
                lblHora.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy - hh:mm:ss tt");
            };

            reloj.Start();
        }

        // 📝 PIE DE PÁGINA PROFESIONAL (ABAJO Y CENTRADO)
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

        // 🔽 EVENTOS DE LOS BOTONES
        private void BtnCategorias_Click(object sender, EventArgs e)
        {
            FrmCategorias frm = new FrmCategorias();
            frm.ShowDialog();
        }

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            FormProducto frm = new FormProducto();
            frm.ShowDialog();
        }

        private void BtnVentas_Click(object sender, EventArgs e)
        {
            FormVenta frm = new FormVenta();
            frm.ShowDialog();
        }
    }
}
