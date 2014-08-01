using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.Implementation;

namespace AIPos.BusinessLayer
{
    public class BORetiroDinero
    {
        RetiroDIneroDaoImpl retiroDineroDao = new RetiroDIneroDaoImpl();

        public RetiroDinero SelectById(int Id)
        {
            return retiroDineroDao.SelectByKey(Id);
        }

        public List<RetiroDinero> SelectAll()
        {
            return retiroDineroDao.SelectAll();
        }

        public List<RetiroDinero> SelectAllByFechaSucursal(int SucursalId, DateTime fecha)
        {
            DateTime fechaInicio = Tools.DateTimeManager.AbsoluteStart(fecha);
            DateTime fechaFin = Tools.DateTimeManager.AbsoluteEnd(fecha);
            return retiroDineroDao.SelectAllByFechaSucursal(fechaInicio,fechaFin,SucursalId);
        }

        public List<ReporteRetiroDinero> SelectReporteByFechaSucursal(int SucursalId, DateTime fecha, bool EsCorteCaja)
        {
            DateTime fechaInicio = Tools.DateTimeManager.AbsoluteStart(fecha);
            DateTime fechaFin = Tools.DateTimeManager.AbsoluteEnd(fecha);
            return retiroDineroDao.SelectReporteByFechaSucursal(fechaInicio, fechaFin, SucursalId, EsCorteCaja);
        }

        public RetiroDinero Insert(RetiroDinero retiroDinero)
        {
            retiroDinero.Fecha = Tools.TimeConverter.GetDateTimeNowMexico();
            return retiroDineroDao.Insert(retiroDinero);
        }

        public RetiroDinero Update(RetiroDinero retiroDinero)
        {
            retiroDinero.Fecha = Tools.TimeConverter.GetDateTimeNowMexico();
            return retiroDineroDao.Update(retiroDinero);
        }

        public void Delete(int Id)
        {
            retiroDineroDao.Delete(Id);
        }
    }
}
