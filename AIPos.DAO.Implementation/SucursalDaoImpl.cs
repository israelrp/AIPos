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
    public class SucursalDaoImpl : SucursalDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Sucursal Insert(Sucursal entity)
        {
            object[] parameters = new object[] { entity.DireccionId, entity.Nombre, entity.Telefono };
            return cm.Database.SqlQuery<Sucursal>("dbo.usp_SucursalesInsert @DireccionId={0}, @Nombre={1}, @Telefono={2}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_SucursalesDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Sucursal Update(Sucursal entity)
        {
            object[] parameters = new object[] { entity.Id, entity.DireccionId, entity.Nombre, entity.Telefono };
            return cm.Database.SqlQuery<Sucursal>("dbo.usp_SucursalesUpdate @Id={0}, @DireccionId={1}, @Nombre={2}, @Telefono={3}", parameters).FirstOrDefault();
        }

        public List<Sucursal> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Sucursal>("dbo.usp_SucursalesSelect @Id={0}", parameters).ToList();
        }

        public Sucursal SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Sucursal>("dbo.usp_SucursalesSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
