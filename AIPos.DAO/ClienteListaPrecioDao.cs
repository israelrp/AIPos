using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO.IGeneric;
using AIPos.Domain;

namespace AIPos.DAO
{
    public interface ClienteListaPrecioDao : IGenericAdd<ClienteListaPrecio>, IGenericDelete<ClienteListaPrecio>, IGenericSelect<ClienteListaPrecio, int>, IGenericUpdate<ClienteListaPrecio>
    {
    }
}
