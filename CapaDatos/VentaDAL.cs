using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class VentaDAL
    {
        // ===============================
        // LISTAR TODAS LAS VENTAS
        // ===============================
        public List<Venta> ListarVentas()
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(
                    @"SELECT v.Id_Venta, 
                             v.Id_cliente, 
                             v.Total_general, 
                             v.Estado_venta, 
                             c.Nombre AS ClienteNombre
                      FROM Venta v
                      INNER JOIN Cliente c 
                          ON v.Id_cliente = c.Id_cliente
                      ORDER BY v.Id_Venta", cn);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Venta
                        {
                            Id_Venta = Convert.ToInt32(dr["Id_Venta"]),
                            Id_cliente = Convert.ToInt32(dr["Id_cliente"]),
                            Total_general = Convert.ToDecimal(dr["Total_general"]),
                            Estado_venta = dr["Estado_venta"].ToString(),
                            NombreCliente = dr["ClienteNombre"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        // ===============================
        // INSERTAR VENTA
        // ===============================
        public int InsertarVenta(Venta venta)
        {
            int idVenta = 0;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();

                try
                {
                    // Insertar venta
                    SqlCommand cmd = new SqlCommand(
                        @"INSERT INTO Venta (Id_cliente, Total_general, Estado_venta)
                          OUTPUT INSERTED.Id_Venta
                          VALUES (@Id_cliente, @Total_general, @Estado_venta)", cn, tran);

                    cmd.Parameters.AddWithValue("@Id_cliente", venta.Id_cliente);
                    cmd.Parameters.AddWithValue("@Total_general", venta.Total_general);
                    cmd.Parameters.AddWithValue("@Estado_venta", venta.Estado_venta);

                    idVenta = (int)cmd.ExecuteScalar();

                    // Insertar detalle
                    foreach (var det in venta.Detalle)
                    {
                        SqlCommand cmdDet = new SqlCommand(
                            @"INSERT INTO Detalle_venta 
                              (Id_venta, Id_producto, Cant, Precio, Estado_detalle)
                              VALUES (@Id_venta, @Id_producto, @Cant, @Precio, @Estado_detalle)", cn, tran);

                        cmdDet.Parameters.AddWithValue("@Id_venta", idVenta);
                        cmdDet.Parameters.AddWithValue("@Id_producto", det.Id_producto);
                        cmdDet.Parameters.AddWithValue("@Cant", det.Cantidad);
                        cmdDet.Parameters.AddWithValue("@Precio", det.Precio);
                        cmdDet.Parameters.AddWithValue("@Estado_detalle", "Activo");

                        cmdDet.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception("Error al insertar la venta: " + ex.Message);
                }
            }

            return idVenta;
        }

        // ===============================
        // OBTENER VENTA PARA REPORTE
        // ===============================
        public DataTable ObtenerVenta(int idVenta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_MostrarVenta", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_VENTA", idVenta);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}
