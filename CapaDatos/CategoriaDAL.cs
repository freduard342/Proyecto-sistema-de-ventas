using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CategoriaDAL
    {
        public List<Categorias> Listar()
        {
            List<Categorias> lista = new List<Categorias>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_LeerCategorias", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Categorias
                    {
                        Id_Categoria = Convert.ToInt32(dr["ID_CATEGORIA"]),
                        Nombre_Categoria = dr["NOMBRE_CAT"].ToString(),
                        Estado = Convert.ToBoolean(dr["ESTADO"])
                    });
                }
            }
            return lista;
        }

        public void Insertar(Categorias categoria)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_CrearCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NOMBRE_CAT", categoria.Nombre_Categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Categorias categoria)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_Actualizar_Categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_CATEGORIA", categoria.Id_Categoria);
                cmd.Parameters.AddWithValue("@NOMBRE_CAT", categoria.Nombre_Categoria);
                cmd.Parameters.AddWithValue("@ESTADO", categoria.Estado);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_CATEGORIA", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
