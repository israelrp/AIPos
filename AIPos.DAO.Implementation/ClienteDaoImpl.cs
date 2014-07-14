using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.IGeneric;
using AIPos.DataContext;

namespace AIPos.DAO.Implementation
{
    public class ClienteDaoImpl : ClienteDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Cliente Insert(Cliente entity)
        {  
 	        object[] parameters = new object[] { entity.SucursalRegistroId, entity.Nombre, entity.Rfc, entity.Telefono, 
                entity.Celular, entity.Codigo, entity.Eliminado, entity.Descuento, entity.RazonSocial };
            return cm.Database.SqlQuery<Cliente>("dbo.usp_ClientesInsert @SucursalRegistroId={0}, @Nombre={1}, @Rfc={2}, "+
                "@Telefono={3}, @Celular={4}, @Codigo={5}, @Eliminado={6}, @Descuento={7}, @RazonSocial={8}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_ClientesDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Cliente Update(Cliente entity)
        {
            object[] parameters = new object[] { entity.Id, entity.SucursalRegistroId, entity.Nombre, entity.Rfc, entity.Telefono, 
                entity.Celular, entity.Codigo, entity.Eliminado, entity.Descuento, entity.RazonSocial };
            return cm.Database.SqlQuery<Cliente>("dbo.usp_ClientesUpdate @Id={0}, @SucursalRegistroId={1}, @Nombre={2}, @Rfc={3}, " +
                "@Telefono={4}, @Celular={5}, @Codigo={6}, @Eliminado={7}, @Descuento={8}, @RazonSocial={9}", parameters).FirstOrDefault();
        }

        public List<Cliente> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Cliente>("dbo.usp_ClientesSelect @Id={0}", parameters).ToList();
        }

        public Cliente SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Cliente>("dbo.usp_ClientesSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
