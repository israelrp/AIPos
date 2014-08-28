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
    public class AbonoCuentaPorCobrarDaoImpl  : AbonoCuentaPorCobrarDao
    {
        ConnectionManager cm = new ConnectionManager();

        public AbonoCuentaPorCobrar Insert(AbonoCuentaPorCobrar entity)
        {
            object[] parameters = new object[] { entity.CuentaPorCobrarId, entity.UsuarioId, entity.MetodoPagoId, entity.Fecha, entity.Importe, entity.Observaciones };
            return cm.Database.SqlQuery<AbonoCuentaPorCobrar>("dbo.usp_AbonosCuentaPorCobrarInsert @CuentaPorCobrarId={0}, @UsuarioId={1}, @MetodoPagoId={2}, @Fecha={3}, @Importe={4}, @Observaciones={5}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_AbonosCuentaPorCobrarDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public AbonoCuentaPorCobrar Update(AbonoCuentaPorCobrar entity)
        {
            object[] parameters = new object[] { entity.Id, entity.CuentaPorCobrarId, entity.UsuarioId, entity.MetodoPagoId, entity.Fecha, entity.Importe, entity.Observaciones };
            return cm.Database.SqlQuery<AbonoCuentaPorCobrar>("dbo.usp_AbonosCuentaPorCobrarUpdate @Id={0}, @CuentaPorCobrarId={1}, @UsuarioId={2}, @MetodoPagoId={3}, @Fecha={4}, @Importe={5}, @Observaciones={6}", parameters).FirstOrDefault();
        }

        public List<AbonoCuentaPorCobrar> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<AbonoCuentaPorCobrar>("dbo.usp_AbonosCuentaPorCobrarSelect @Id={0}", parameters).ToList();
        }

        public AbonoCuentaPorCobrar SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<AbonoCuentaPorCobrar>("dbo.usp_AbonosCuentaPorCobrarSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
