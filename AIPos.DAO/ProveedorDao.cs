using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO.IGeneric;
using AIPos.Domain;

namespace AIPos.DAO
{
    public interface ProveedorDao : IGenericAdd<Proveedor>, IGenericDelete<int>, IGenericUpdate<Proveedor>, IGenericSelect<Proveedor, int>
    {
    }
}
