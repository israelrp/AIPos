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
    public class DireccionFacturacionDaoImpl : DireccionFacturacionDao
    {

        ConnectionManager cm = new ConnectionManager();

        public DireccionFacturacion Insert(DireccionFacturacion entity)
        {
            object[] parameters = new object[] { entity.ClienteId, entity.DireccionId };
            return cm.Database.SqlQuery<DireccionFacturacion>("dbo.usp_DireccionesFacturacionInsert @ClienteId={0}, @DireccionId={1}", parameters).FirstOrDefault();
        }

        public bool Delete(int DireccionId, int ClienteId)
        {
            bool retorno = false;
            object[] parameters = new object[] { DireccionId, ClienteId };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_DireccionesFacturacionDelete @DireccionId={0}, @ClienteId={1}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public List<DireccionFacturacion> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<DireccionFacturacion>("dbo.usp_DireccionesFacturacionSelect @DireccionId={0}, @ClienteId={0}", parameters).ToList();
        }

        public DireccionFacturacion SelectByKey(int key)
        {
            throw new NotImplementedException("Este método no funciona porque tiene llave primaria compuesta");
        }

        public DireccionFacturacion SelectByKeys(int DireccionId, int ClienteId)
        {
            object[] parameters = new object[] { DireccionId, ClienteId };
            return cm.Database.SqlQuery<DireccionFacturacion>("dbo.usp_DireccionesFacturacionSelect @DireccionId={0}, @ClienteId={1}", parameters).FirstOrDefault();
        }
    }
}
