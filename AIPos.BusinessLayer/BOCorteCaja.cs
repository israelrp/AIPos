using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.Implementation;
using AIPos.DAO;

namespace AIPos.BusinessLayer
{
    public class BOCorteCaja
    {
        CorteCajaDaoImpl corteCajaDaoImpl = new CorteCajaDaoImpl();

        public CorteCaja Insert(CorteCaja entity)
        {
            return corteCajaDaoImpl.Insert(entity);
        }

        public CorteCaja Update(CorteCaja entity)
        {
            return corteCajaDaoImpl.Update(entity);
        }

        public List<CorteCaja> SelectAll()
        {
            return corteCajaDaoImpl.SelectAll();
        }

        public List<CorteCaja> SelectByFecha(DateTime Fecha)
        {
            DateTime fechaInicio = Tools.DateTimeManager.AbsoluteStart(Fecha);
            DateTime fechaFin = Tools.DateTimeManager.AbsoluteEnd(Fecha);
            return corteCajaDaoImpl.SelectByFecha(fechaInicio, fechaFin);
        }

        public void Delete(int Id)
        {
            corteCajaDaoImpl.Delete(Id);
        }
    }
}
