using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO.IGeneric;
using AIPos.DataContext;
using AIPos.Domain;

namespace AIPos.DAO.Implementation
{
    public class SucursalListaPrecioDaoImpl : SucursalListaPrecioDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Domain.SucursalListaPrecio Insert(SucursalListaPrecio entity)
        {
            object[] parameters = new object[] { entity.SucursalId, entity.ListaPrecioId };
            return cm.Database.SqlQuery<SucursalListaPrecio>("dbo.usp_SucursalesListasPrecioInsert @SucursalId={0}, @ListaPrecioId={1}", parameters).FirstOrDefault();
        }

        public bool Delete(SucursalListaPrecio key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key.SucursalId, key.ListaPrecioId };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_SucursalesListasPrecioDelete @SucursalId={0}, @ListaPrecioId={1}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Domain.SucursalListaPrecio Update(SucursalListaPrecio entity)
        {
            object[] parameters = new object[] { entity.SucursalId, entity.ListaPrecioId };
            return cm.Database.SqlQuery<SucursalListaPrecio>("dbo.usp_SucursalesListasPrecioUpdate @SucursalId={0}, @ListaPrecioId={1}", parameters).FirstOrDefault();
        }

        public List<SucursalListaPrecio> SelectAll()
        {
            object[] parameters = new object[] { null, null };
            return cm.Database.SqlQuery<SucursalListaPrecio>("dbo.usp_SucursalesListasPrecioSelect @SucursalId={0}, @ListaPrecioId={1}", parameters).ToList();
        }

        /// <summary>
        /// No implementado
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Domain.SucursalListaPrecio SelectByKey(int key)
        {
            throw new NotImplementedException();
        }

        public SucursalListaPrecio SelectByKey(int SucursalId, int? ListaPrecioId)
        {
            object[] parameters = new object[] { SucursalId, ListaPrecioId };
            return cm.Database.SqlQuery<SucursalListaPrecio>("dbo.usp_SucursalesListasPrecioSelect @SucursalId={0}, @ListaPrecioId={1}", parameters).FirstOrDefault();
        }

        public List<SucursalListaPrecio> SelectByListaPrecio(int ListaPrecioId)
        {
            object[] parameters = new object[] { null, ListaPrecioId };
            return cm.Database.SqlQuery<SucursalListaPrecio>("dbo.usp_SucursalesListasPrecioSelect @SucursalId={0}, @ListaPrecioId={1}", parameters).ToList();
        }
    }
}
