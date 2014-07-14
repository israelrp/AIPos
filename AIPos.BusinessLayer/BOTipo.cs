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
    public class BOTipo
    {
        TipoDaoImpl tipoDaoImpl = new TipoDaoImpl();

        public Tipo Insert(Tipo tipo)
        {
            tipo = tipoDaoImpl.Insert(tipo);
            return tipo;
        }

        public void Delete(int id)
        {
            if (!tipoDaoImpl.Delete(id))
            {
                throw new Exception("No fue posible eliminar la unidad");
            }
        }

        public Tipo Update(Tipo tipo)
        {
            tipo = tipoDaoImpl.Update(tipo);
            return tipo;
        }

        public List<Tipo> SelectAll()
        {
            return tipoDaoImpl.SelectAll();
        }

        public Tipo SelectById(int id)
        {
            return tipoDaoImpl.SelectByKey(id);
        }
    }
}
