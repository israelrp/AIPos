using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPos.DAO.IGeneric
{
    public interface IGenericUpdate<TDomain>
        where TDomain : class
    {
        TDomain Update(TDomain entity);
    }
}
