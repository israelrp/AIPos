using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.IGeneric;

namespace AIPos.DAO
{
    public interface ServicioApartadoDao : IGenericAdd<ServicioApartado>, IGenericDelete<int>, IGenericUpdate<ServicioApartado>, IGenericSelect<ServicioApartado, int>
    {
    }
}
