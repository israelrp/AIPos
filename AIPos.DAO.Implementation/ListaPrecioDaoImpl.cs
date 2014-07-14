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
    public class ListaPrecioDaoImpl : ListaPrecioDao
    {
        ConnectionManager cm = new ConnectionManager();

        public ListaPrecio Insert(ListaPrecio entity)
        {
            object[] parameters = new object[] { entity.Nombre };
            return cm.Database.SqlQuery<ListaPrecio>("dbo.usp_ListasPrecioInsert @Nombre={0}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_ListasPrecioDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public ListaPrecio Update(ListaPrecio entity)
        {
            object[] parameters = new object[] { entity.Id, entity.Nombre};
            return cm.Database.SqlQuery<ListaPrecio>("dbo.usp_ListasPrecioUpdate @Id={0}, @Nombre={1}", parameters).FirstOrDefault();
        }

        public List<ListaPrecio> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<ListaPrecio>("dbo.usp_ListasPrecioSelect @Id={0}", parameters).ToList();
        }

        public ListaPrecio SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<ListaPrecio>("dbo.usp_ListasPrecioSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
