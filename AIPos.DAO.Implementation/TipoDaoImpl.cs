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
    public class TipoDaoImpl : TipoDao
    {
        ConnectionManager cm = new ConnectionManager();

        public Tipo Insert(Tipo entity)
        {
            object[] parameters = new object[] { entity.Nombre, entity.Eliminado };
            return cm.Database.SqlQuery<Tipo>("dbo.usp_TiposInsert @Nombre={0}, @Eliminado={1}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_TiposDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public Tipo Update(Tipo entity)
        {
            object[] parameters = new object[] { entity.Id, entity.Nombre, entity.Eliminado };
            return cm.Database.SqlQuery<Tipo>("dbo.usp_TiposUpdate @Id={0}, @Nombre={1}, @Eliminado={2}", parameters).FirstOrDefault();
        }

        public List<Tipo> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Tipo>("dbo.usp_TiposSelect @Id={0}", parameters).ToList();
        }

        public Tipo SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Tipo>("dbo.usp_TiposSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
