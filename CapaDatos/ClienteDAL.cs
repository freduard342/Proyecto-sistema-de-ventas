using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class ClienteDAL
    {
        public List<Clientes> Listar()
        {
            List<Clientes> lista = new List<Clientes>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                
                SqlCommand cmd = new SqlCommand("SP_LeerClientes", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Clientes
                    {
                        Id_Cliente = Convert.ToInt32(dr["Id_cliente"]),
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}