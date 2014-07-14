using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO;
using AIPos.DAO.Implementation;
using AIPos.Domain;

namespace AIPos.BusinessLayer
{
    public class BONivel
    {
        NivelDaoImpl nivelDaoImpl = new NivelDaoImpl();

        public List<Nivel> SelectAll()
        {
            return nivelDaoImpl.SelectAll();
        }

        public Nivel SelectById(int id)
        {
            return nivelDaoImpl.SelectByKey(id);
        }
    }
}
