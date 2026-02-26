using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class ProductoBL
    {
        ProductoDAL dal = new ProductoDAL();

        public List<Productos> Listar()
        {
            return dal.Listar();
        }

        public void Insertar(Productos prod)
        {
            dal.Insertar(prod);
        }

        public int ValidarStock(int idProducto, int cantidad)
        {
            return dal.ValidarStock(idProducto, cantidad);
        }

        public void ActualizarStock(int idProducto, int cantidadVendida)
        {
            dal.ActualizarStock(idProducto, cantidadVendida);
        }

        public void Eliminar(int id)
        {
            dal.Eliminar(id);
        }
    }
}
