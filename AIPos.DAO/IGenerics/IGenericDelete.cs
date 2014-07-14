using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPos.DAO.IGeneric
{
    public interface IGenericDelete<TKey>
    {
        bool Delete(TKey key);
    }
}
