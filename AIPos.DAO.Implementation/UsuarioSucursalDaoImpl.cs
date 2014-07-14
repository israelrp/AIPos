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
    public class UsuarioSucursalDaoImpl : UsuarioSucursalDao
    {
        ConnectionManager cm = new ConnectionManager();

        public UsuarioSucursal Insert(UsuarioSucursal entity)
        {
            object[] parameters = new object[] { entity.SucursalId, entity.UsuarioId };
            return cm.Database.SqlQuery<UsuarioSucursal>("dbo.usp_UsuariosSurcusalInsert @SucursalId={0}, @UsuarioId={1}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int SucursalId, int UsuarioId)
        {
            bool retorno = false;
            object[] parameters = new object[] { SucursalId, UsuarioId };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_UsuariosSurcusalDelete @SucursalId={0}, @UsuarioId={1}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public UsuarioSucursal Update(UsuarioSucursal entity)
        {
            object[] parameters = new object[] { entity.SucursalId, entity.UsuarioId };
            return cm.Database.SqlQuery<UsuarioSucursal>("dbo.usp_UsuariosSucursalUpdate @SucursalId={0}, @UsuarioId={1}", parameters).FirstOrDefault();
        }

        public List<UsuarioSucursal> SelectAll()
        {
            object[] parameters = new object[] { null, null };
            return cm.Database.SqlQuery<UsuarioSucursal>("dbo.usp_UsuariosSurcusalSelect @SucursalId={0}, @UsuarioId={1}", parameters).ToList();
        }

        public UsuarioSucursal SelectByKey(int key)
        {
            throw new NotImplementedException();
        }

        public UsuarioSucursal SelectByKey(int SucursalId, int UsuarioId)
        {
            object[] parameters = new object[] { SucursalId, UsuarioId };
            return cm.Database.SqlQuery<UsuarioSucursal>("dbo.usp_UsuariosSurcusalSelect @SucursalId={0}, @UsuarioId={1}", parameters).FirstOrDefault();
        }
    }
}
