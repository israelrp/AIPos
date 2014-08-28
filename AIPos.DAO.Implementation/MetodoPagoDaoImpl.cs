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
    public class MetodoPagoDaoImpl : MetodoPagoDao
    {
        ConnectionManager cm = new ConnectionManager();
        public MetodoPago Insert(MetodoPago entity)
        {
            object[] parameters = new object[] { entity.Nombre};
            return cm.Database.SqlQuery<MetodoPago>("dbo.usp_MetodosPagoInsert @Nombre={0}", parameters).FirstOrDefault();
        }

        public bool Delete(MetodoPago key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_MetodosPagoDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public MetodoPago Update(MetodoPago entity)
        {
            object[] parameters = new object[] { entity.Id, entity.Nombre};
            return cm.Database.SqlQuery<MetodoPago>("dbo.usp_MetodosPagoUpdate @Id={0}, @Nombre={1}", parameters).FirstOrDefault();
        }

        public List<MetodoPago> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<MetodoPago>("dbo.usp_MetodosPagoSelect @Id={0}", parameters).ToList();
        }

        public MetodoPago SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<MetodoPago>("dbo.usp_MetodosPagoSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
