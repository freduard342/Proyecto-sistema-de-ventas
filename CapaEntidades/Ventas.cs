using System.Collections.Generic;

namespace CapaEntidades
{
    public class Venta
    {
        public int Id_Venta { get; set; }        // Necesario para listar
        public int Id_cliente { get; set; }
        public string NombreCliente { get; set; } // Para mostrar en reporte
        public decimal Total_general { get; set; }
        public string Estado_venta { get; set; }
        public List<DetalleVenta> Detalle { get; set; } = new List<DetalleVenta>();
    }

    public class DetalleVenta
    {

            public int Id_producto { get; set; }
            public string NombreProducto { get; set; }   // 👈 AGREGAR ESTA LÍNEA
            public int Cantidad { get; set; }
            public decimal Precio { get; set; }
        }

    }

