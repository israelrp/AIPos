using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO.IGeneric;
using AIPos.Domain;

namespace AIPos.DAO
{
    public interface SeguimientoServicioApartadoDao : IGenericAdd<SeguimientoServicioApartado>, IGenericDelete<int>, IGenericUpdate<SeguimientoServicioApartado>, IGenericSelect<SeguimientoServicioApartado, int>
    {
    }
}
