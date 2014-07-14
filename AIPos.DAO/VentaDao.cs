using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.IGeneric;

namespace AIPos.DAO
{
    public interface VentaDao : IGenericAdd<Venta>, IGenericDelete<int>, IGenericUpdate<Venta>, IGenericSelect<Venta, int>
    {
    }
}
