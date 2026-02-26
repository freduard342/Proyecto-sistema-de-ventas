using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class ProductoDAL
    {
        // Listar todos los productos
        public List<Productos> Listar()
        {
            List<Productos> lista = new List<Productos>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarProductos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Productos
                    {
                        Id_Producto = Convert.ToInt32(dr["Id_Producto"]),
                        Nombre_Producto = dr["Nombre_Producto"].ToString(),
                        Precio_Producto = Convert.ToDecimal(dr["Precio_Producto"]),
                        Stock = Convert.ToInt32(dr["Stock"]),
                        Id_Categoria = Convert.ToInt32(dr["ID_CATEGORIA"])
                    });
                }
            }

            return lista;
        }

        // Insertar un producto
        public void Insertar(Productos prod)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_CrearProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre_Producto", prod.Nombre_Producto);
                cmd.Parameters.AddWithValue("@Precio_Producto", prod.Precio_Producto);
                cmd.Parameters.AddWithValue("@Stock", prod.Stock);
                cmd.Parameters.AddWithValue("@ID_CATEGORIA", prod.Id_Categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int ValidarStock(int idProducto, int cantidadSolicitada)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarStock", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_producto", idProducto);
                cmd.Parameters.AddWithValue("@CantidadSolicitada", cantidadSolicitada);

                cn.Open();

                object resultado = cmd.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                    return 0;

                return Convert.ToInt32(resultado); // ← DEBE DEVOLVER EL STOCK REAL
            }
        }





        public void ActualizarStock(int idProducto, int cantidadVendida)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarStock", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_producto", idProducto);
                cmd.Parameters.AddWithValue("@CantidadVendida", cantidadVendida);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // Eliminar un producto
        public void Eliminar(int id)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Producto", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

        }
    }
}
