namespace CapaPresentacion
{
    partial class FormReporte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.sistemaVentasDataSet3BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sistema_VentasDataSet3 = new CapaPresentacion.Sistema_VentasDataSet3();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.vistaDetalleVenta = new CapaPresentacion.VistaDetalleVenta();
            this.vistaDetalleVentaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.sistemaVentasDataSet3BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sistema_VentasDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vistaDetalleVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vistaDetalleVentaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // sistemaVentasDataSet3BindingSource
            // 
            this.sistemaVentasDataSet3BindingSource.DataSource = this.sistema_VentasDataSet3;
            this.sistemaVentasDataSet3BindingSource.Position = 0;
            // 
            // sistema_VentasDataSet3
            // 
            this.sistema_VentasDataSet3.DataSetName = "Sistema_VentasDataSet3";
            this.sistema_VentasDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "GAMA";
            reportDataSource1.Value = this.vistaDetalleVentaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CapaPresentacion.Report3.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.FullPage;
            // 
            // vistaDetalleVenta
            // 
            this.vistaDetalleVenta.DataSetName = "VistaDetalleVenta";
            this.vistaDetalleVenta.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vistaDetalleVentaBindingSource
            // 
            this.vistaDetalleVentaBindingSource.DataSource = this.vistaDetalleVenta;
            this.vistaDetalleVentaBindingSource.Position = 0;
            // 
            // FormReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormReporte";
            this.Text = "FormReporte";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormReporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sistemaVentasDataSet3BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sistema_VentasDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vistaDetalleVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vistaDetalleVentaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource sistemaVentasDataSet3BindingSource;
        private Sistema_VentasDataSet3 sistema_VentasDataSet3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource vistaDetalleVentaBindingSource;
        private VistaDetalleVenta vistaDetalleVenta;
    }
}