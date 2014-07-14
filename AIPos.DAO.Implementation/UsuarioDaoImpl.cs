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
    public class UsuarioDaoImpl : UsuarioDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Usuario Insert(Usuario entity)
        {
            object[] parameters = new object[] { entity.DireccionId, entity.NivelId, entity.Nombre, entity.Paterno, entity.Materno,
                entity.Password, entity.Telefono, entity.Celular, entity.Observaciones, entity.Eliminado, entity.UserName};
            return cm.Database.SqlQuery<Usuario>("dbo.usp_UsuariosInsert @DireccionId={0}, @NivelId={1}, @Nombre={2}, @Paterno={3}, @Materno={4}, "+
                "@Password={5}, @Telefono={6}, @Celular={7}, @Observaciones={8}, @Eliminado={9}, @UserName={10}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_UsuariosDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Usuario Update(Usuario entity)
        {
            object[] parameters = new object[] { entity.Id, entity.DireccionId, entity.NivelId, entity.Nombre, entity.Paterno, entity.Materno,
                entity.Password, entity.Telefono, entity.Celular, entity.Observaciones, entity.Eliminado, entity.UserName };
            return cm.Database.SqlQuery<Usuario>("dbo.usp_UsuariosUpdate @Id={0}, @DireccionId={1}, @NivelId={2}, @Nombre={3}, @Paterno={4}, @Materno={5}, " +
                "@Password={6}, @Telefono={7}, @Celular={8}, @Observaciones={9}, @Eliminado={10}, @UserName={11}", parameters).FirstOrDefault();
        }

        public List<Usuario> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Usuario>("dbo.usp_UsuariosSelect @Id={0}", parameters).ToList();
        }

        public Usuario SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Usuario>("dbo.usp_UsuariosSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
