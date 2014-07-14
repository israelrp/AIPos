using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.Implementation;

namespace AIPos.BusinessLayer
{
    public class BOTipoProducto
    {
        TipoProductoDaoImpl tipoProductoDaoImpl = new TipoProductoDaoImpl();

        public List<TipoProducto> SelectAll()
        {
            return tipoProductoDaoImpl.SelectAll();
        }

        public TipoProducto SeectById(int Id)
        {
            return tipoProductoDaoImpl.SelectByKey(Id);
        }
    }
}
