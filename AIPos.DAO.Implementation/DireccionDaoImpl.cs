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
    public class DireccionDaoImpl : DireccionDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Direccion Insert(Direccion entity)
        {
            object[] parameters = new object[] { entity.Calle, entity.NoExterior, entity.NoInterior, entity.Colonia, entity.CodigoPostal, entity.Referencia, entity.Ciudad, entity.Estado };
            return cm.Database.SqlQuery<Direccion>("dbo.usp_DireccionesInsert @Calle={0}, @NoExterior={1}, @NoInterior={2}, @Colonia={3}, @CodigoPostal={4}, @Referencia={5}, @Ciudad={6}, @Estado={7}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_DireccionesDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Direccion Update(Direccion entity)
        {
            object[] parameters = new object[] { entity.Id, entity.Calle, entity.NoExterior, entity.NoInterior, entity.Colonia, entity.CodigoPostal, entity.Referencia, entity.Ciudad, entity.Estado };
            return cm.Database.SqlQuery<Direccion>("dbo.usp_DireccionesUpdate @Id={0}, @Calle={1}, @NoExterior={2}, @NoInterior={3}, @Colonia={4}, @CodigoPostal={5}, @Referencia={6}, @Ciudad={7}, @Estado={8}", parameters).FirstOrDefault();
        }

        public List<Direccion> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Direccion>("dbo.usp_DireccionesSelect @Id={0}", parameters).ToList();
        }

        public Direccion SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Direccion>("dbo.usp_DireccionesSelect @Id={0}", parameters).FirstOrDefault();
        }

        public List<Direccion> SelectByCliente(int ClienteId)
        {
            object[] parameters = new object[] { ClienteId };
            return cm.Database.SqlQuery<Direccion>("dbo.usp_DireccionesSelectByCliente @ClienteId={0}", parameters).ToList();
        }
    }
}
