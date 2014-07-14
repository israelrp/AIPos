using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DataContext;
using AIPos.DAO.IGeneric;

namespace AIPos.DAO.Implementation
{
    public class TipoProductoDaoImpl : TipoProductoDao
    {
        ConnectionManager cm = new ConnectionManager();

        public List<TipoProducto> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<TipoProducto>("dbo.usp_TiposProductoSelect @Id={0}", parameters).ToList();
        }

        public TipoProducto SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<TipoProducto>("dbo.usp_TiposProductoSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
