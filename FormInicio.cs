using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormInicio : Form
    {
        private Timer reloj;
        private Label lblHora;

        public FormInicio()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CrearMensajeBienvenida();
            CrearLineaDecorativa();
            CrearSubtitulo();
            CrearBotonEntrar();
            CrearPieDePagina();
            CrearReloj();
        }

        // 🎨 CONFIGURACIÓN DEL FORMULARIO (PANTALLA COMPLETA)
        private void ConfigurarFormulario()
        {
            this.BackColor = Color.FromArgb(18, 18, 22);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized; // 🔥 Pantalla completa
            this.Text = "Bienvenido al Sistema de Ventas";
        }

        // 🌟 MENSAJE PRINCIPAL
        private void CrearMensajeBienvenida()
        {
            Label lblBienvenida = new Label();
            lblBienvenida.Text = "BIENVENIDO AL";
            lblBienvenida.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblBienvenida.ForeColor = Color.White;
            lblBienvenida.AutoSize = false;
            lblBienvenida.Width = 900;
            lblBienvenida.Height = 60;
            lblBienvenida.TextAlign = ContentAlignment.MiddleCenter;
            lblBienvenida.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblBienvenida.Left = (this.ClientSize.Width / 2) - 450;
            lblBienvenida.Top = 120;

            this.Controls.Add(lblBienvenida);

            Label lblSistema = new Label();
            lblSistema.Text = "SISTEMA DE VENTAS";
            lblSistema.Font = new Font("Segoe UI", 32, FontStyle.Bold);
            lblSistema.ForeColor = Color.FromArgb(255, 140, 0);
            lblSistema.AutoSize = false;
            lblSistema.Width = 900;
            lblSistema.Height = 90;
            lblSistema.TextAlign = ContentAlignment.MiddleCenter;
            lblSistema.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSistema.Left = (this.ClientSize.Width / 2) - 450;
            lblSistema.Top = 170;

            this.Controls.Add(lblSistema);
        }

        // ✨ LÍNEA DECORATIVA
        private void CrearLineaDecorativa()
        {
            Panel linea = new Panel();
            linea.Width = 300;
            linea.Height = 4;
            linea.BackColor = Color.FromArgb(255, 140, 0);
            linea.Left = (this.ClientSize.Width / 2) - 150;
            linea.Top = 270;
            linea.Anchor = AnchorStyles.Top;

            this.Controls.Add(linea);
        }

        // 📌 SUBTÍTULO
        private void CrearSubtitulo()
        {
            Label lblSubtitulo = new Label();
            lblSubtitulo.Text = "Gestión inteligente de productos y ventas";
            lblSubtitulo.Font = new Font("Segoe UI", 11, FontStyle.Italic);
            lblSubtitulo.ForeColor = Color.LightGray;
            lblSubtitulo.AutoSize = false;
            lblSubtitulo.Width = 900;
            lblSubtitulo.Height = 40;
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblSubtitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSubtitulo.Left = (this.ClientSize.Width / 2) - 450;
            lblSubtitulo.Top = 290;

            this.Controls.Add(lblSubtitulo);
        }

        // 🔘 BOTÓN PARA ENTRAR (CENTRADO)
        private void CrearBotonEntrar()
        {
            Button btnEntrar = new Button();
            btnEntrar.Text = "ENTRAR AL SISTEMA";
            btnEntrar.Width = 320;
            btnEntrar.Height = 80;
            btnEntrar.Left = (this.ClientSize.Width / 2) - 160;
            btnEntrar.Top = 350;
            btnEntrar.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnEntrar.BackColor = Color.FromArgb(255, 140, 0);
            btnEntrar.ForeColor = Color.Black;
            btnEntrar.FlatStyle = FlatStyle.Flat;
            btnEntrar.FlatAppearance.BorderSize = 1;
            btnEntrar.FlatAppearance.BorderColor = Color.White;
            btnEntrar.Cursor = Cursors.Hand;
            btnEntrar.Anchor = AnchorStyles.Top;

            // ✨ Efecto hover
            btnEntrar.MouseEnter += (s, e) =>
            {
                btnEntrar.BackColor = Color.White;
                btnEntrar.ForeColor = Color.Black;
            };

            btnEntrar.MouseLeave += (s, e) =>
            {
                btnEntrar.BackColor = Color.FromArgb(255, 140, 0);
                btnEntrar.ForeColor = Color.Black;
            };

            btnEntrar.Click += BtnEntrar_Click;

            this.Controls.Add(btnEntrar);
        }

        // ⏰ RELOJ EN TIEMPO REAL (ESQUINA INFERIOR IZQUIERDA)
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

        // 🔽 ABRIR SISTEMA DE VENTAS
        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            Form4 sistema = new Form4();
            sistema.Show();
            this.Hide();
        }
    }
}
