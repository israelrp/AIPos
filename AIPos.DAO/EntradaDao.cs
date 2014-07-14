using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.IGeneric;

namespace AIPos.DAO
{
    public interface EntradaDao : IGenericAdd<Entrada>, IGenericDelete<int>, IGenericUpdate<Entrada>, IGenericSelect<Entrada, int>
    {
    }
}
