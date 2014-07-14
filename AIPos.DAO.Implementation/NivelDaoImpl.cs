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
    public class NivelDaoImpl : NivelDao
    {
        ConnectionManager cm = new ConnectionManager();

        public List<Nivel> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<Nivel>("dbo.usp_NivelesSelect @Id={0}", parameters).ToList();
        }

        public Nivel SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<Nivel>("dbo.usp_NivelesSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
