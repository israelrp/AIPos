using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DataContext;
using AIPos.Domain;

namespace AIPos.DAO.Implementation
{
    public class UnidadDaoImpl : AIPos.DAO.UnidadDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Unidad Insert(Unidad entity)
        {
            object[] parameters = new object[] { entity.Nombre, entity.Eliminado };
            return cm.Database.SqlQuery<Unidad>("dbo.usp_UnidadesInsert @Nombre={0}, @Eliminado={1}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_UnidadesDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Unidad Update(Unidad entity)
        {
            object[] parameters = new object[] { entity.Id, entity.Nombre, entity.Eliminado };
            return cm.Database.SqlQuery<Unidad>("dbo.usp_UnidadesUpdate @Id={0}, @Nombre={1}, @Eliminado={2}", parameters).FirstOrDefault();
        }

        public List<Unidad> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Unidad>("dbo.usp_UnidadesSelect @Id={0}", parameters).ToList();
        }

        public Unidad SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Unidad>("dbo.usp_UnidadesSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
