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
    public class DireccionEnvioDaoImpl : DireccionEnvioDao
    {
        ConnectionManager cm = new ConnectionManager();

        public DireccionEnvio Insert(DireccionEnvio entity)
        {
            object[] parameters = new object[] { entity.ClienteId, entity.DireccionId };
            return cm.Database.SqlQuery<DireccionEnvio>("dbo.usp_DireccionesEnvioInsert @ClienteId={0}, @DireccionId={1}", parameters).FirstOrDefault();
        }

        public bool Delete(int DireccionId, int ClienteId)
        {
            bool retorno = false;
            object[] parameters = new object[] { DireccionId, ClienteId };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_DireccionesEnvioDelete @DireccionId={0}, @ClienteId={1}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public List<DireccionEnvio> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<DireccionEnvio>("dbo.usp_DireccionesEnvioSelect @DireccionId={0}, @ClienteId={0}", parameters).ToList();
        }

        public DireccionEnvio SelectByKey(int key)
        {
            throw new NotImplementedException("Este método no funciona porque tiene llave primaria compuesta");
        }

        public DireccionEnvio SelectByKeys(int DireccionId, int ClienteId)
        {
            object[] parameters = new object[] { DireccionId, ClienteId };
            return cm.Database.SqlQuery<DireccionEnvio>("dbo.usp_DireccionesEnvioSelect @DireccionId={0}, @ClienteId={1}", parameters).FirstOrDefault();
        }
    }
}
