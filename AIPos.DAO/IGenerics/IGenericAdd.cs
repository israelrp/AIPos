using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPos.DAO.IGeneric
{
    public interface IGenericAdd<TDomain>
        where TDomain : class
    {
        TDomain Insert(TDomain entity);
    }
}
