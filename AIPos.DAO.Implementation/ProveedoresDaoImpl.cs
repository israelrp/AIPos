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
    public class ProveedoresDaoImpl : ProveedorDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Proveedor Insert(Proveedor entity)
        {
            object[] parameters = new object[] { entity.DireccionId, entity.RazonSocial, entity.Rfc, entity.Contacto, entity.Telefono,
                entity.Codigo, entity.Email, entity.Eliminado };
            return cm.Database.SqlQuery<Proveedor>("dbo.usp_ProveedoresInsert @DireccionId={0}, @RazonSocial={1}, @Rfc={2}, "+
                "@Contacto={3}, @Telefono={4}, @Codigo={5}, @Email={6}, @Eliminado={7}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_ProveedoresDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Proveedor Update(Proveedor entity)
        {
            object[] parameters = new object[] { entity.Id, entity.DireccionId, entity.RazonSocial, entity.Rfc, entity.Contacto, entity.Telefono,
                entity.Codigo, entity.Email, entity.Eliminado };
            return cm.Database.SqlQuery<Proveedor>("dbo.usp_ProveedoresUpdate @Id={0}, @DireccionId={1}, @RazonSocial={2}, @Rfc={3}, " +
                "@Contacto={4}, @Telefono={5}, @Codigo={6}, @Email={7}, @Eliminado={8}", parameters).FirstOrDefault();
        }

        public List<Proveedor> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Proveedor>("dbo.usp_ProveedoresSelect @Id={0}", parameters).ToList();
        }

        public Proveedor SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Proveedor>("dbo.usp_ProveedoresSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
