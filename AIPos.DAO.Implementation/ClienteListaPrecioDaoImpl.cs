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
    public class ClienteListaPrecioDaoImpl : ClienteListaPrecioDao
    {
        ConnectionManager cm = new ConnectionManager();

        public ClienteListaPrecio Insert(ClienteListaPrecio entity)
        {
            object[] parameters = new object[] { entity.ClienteId, entity.ListaPrecioId };
            return cm.Database.SqlQuery<ClienteListaPrecio>("dbo.usp_ClientesListasPrecioInsert @ClienteId={0}, @ListaPrecioId={1}", parameters).FirstOrDefault();
        }

        public bool Delete(ClienteListaPrecio key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key.ClienteId, key.ListaPrecioId };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_ClientesListasPrecioDelete @ClienteId={0}, @ListaPrecioId={1}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public List<ClienteListaPrecio> SelectAll()
        {
            object[] parameters = new object[] { null,null };
            return cm.Database.SqlQuery<ClienteListaPrecio>("dbo.usp_ClientesListasPrecioSelect @ClienteId={0}, @ListaPrecioId={1}", parameters).ToList();
        }

        /// <summary>
        /// No implementado
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ClienteListaPrecio SelectByKey(int key)
        {
            throw new NotImplementedException();
        }

        public ClienteListaPrecio SelectByKey(int ClienteId, int? ListaPrecioId)
        {
            object[] parameters = new object[] { ClienteId, ListaPrecioId };
            return cm.Database.SqlQuery<ClienteListaPrecio>("dbo.usp_ClientesListasPrecioSelect @ClienteId={0}, @ListaPrecioId={1}", parameters).FirstOrDefault();
        }

        public List<ClienteListaPrecio> SelectByKey(int? ClienteId, int? ListaPrecioId)
        {
            object[] parameters = new object[] { ClienteId, ListaPrecioId };
            return cm.Database.SqlQuery<ClienteListaPrecio>("dbo.usp_ClientesListasPrecioSelect @ClienteId={0}, @ListaPrecioId={1}", parameters).ToList();
        }

        public ClienteListaPrecio Update(ClienteListaPrecio entity)
        {
            object[] parameters = new object[] { entity.ClienteId, entity.ListaPrecioId };
            return cm.Database.SqlQuery<ClienteListaPrecio>("dbo.usp_TiposUpdate @ClienteId={0}, @ListaPrecioId={1}", parameters).FirstOrDefault();
        }

        public List<ClienteListaPrecioModel> SelectAllGrid()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<ClienteListaPrecioModel>("dbo.usp_ClientesListasPrecioSelectGrid", parameters).ToList();
        }
    }
}
