using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPos.DAO.IGeneric
{
    public interface IGenericSelect<TDomain, TKey>
        where TDomain : class
    {
        List<TDomain> SelectAll();
        TDomain SelectByKey(TKey key);
    }
}
