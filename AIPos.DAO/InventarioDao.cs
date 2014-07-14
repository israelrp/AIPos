using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.IGeneric;

namespace AIPos.DAO
{
    public interface InventarioDao : IGenericAdd<Inventario>, IGenericDelete<int>, IGenericUpdate<Inventario>, IGenericSelect<Inventario, int>
    {
    }
}
