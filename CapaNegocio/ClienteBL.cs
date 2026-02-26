using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class ClienteBL
    {
        ClienteDAL dal = new ClienteDAL();

        public List<Clientes> Listar()
        {
            return dal.Listar();
        }
    }
}
