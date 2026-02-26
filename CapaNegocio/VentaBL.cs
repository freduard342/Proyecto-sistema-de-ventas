using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;
using System.Data;

namespace CapaNegocio
{
    public class VentaBL
    {
        VentaDAL dal = new VentaDAL();

        public DataTable ObtenerVenta(int idVenta)
        {
            return dal.ObtenerVenta(idVenta);
        }


        public List<Venta> ListarVentas()
        {
            return dal.ListarVentas();
        }
    }
}
