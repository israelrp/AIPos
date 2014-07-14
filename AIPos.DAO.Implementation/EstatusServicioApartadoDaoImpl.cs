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
    public class EstatusServicioApartadoDaoImpl : EstatusServicioApartadoDao
    {
        ConnectionManager cm = new ConnectionManager();

        public EstatusServicioApartado Insert(EstatusServicioApartado entity)
        {
            object[] parameters = new object[] { entity.Nombre };
            return cm.Database.SqlQuery<EstatusServicioApartado>("dbo.usp_EstatusServicioApartadoInsert @Nombre={0}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_EstatusServicioApartadoDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public EstatusServicioApartado Update(EstatusServicioApartado entity)
        {
            object[] parameters = new object[] { entity.Id, entity.Nombre };
            return cm.Database.SqlQuery<EstatusServicioApartado>("dbo.usp_EstatusServicioApartadoUpdate @Id={0}, @Nombre={1}", parameters).FirstOrDefault();
        }

        public List<EstatusServicioApartado> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<EstatusServicioApartado>("dbo.usp_EstatusServicioApartadoSelect @Id={0}", parameters).ToList();
        }

        public EstatusServicioApartado SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<EstatusServicioApartado>("dbo.usp_EstatusServicioApartadoSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
