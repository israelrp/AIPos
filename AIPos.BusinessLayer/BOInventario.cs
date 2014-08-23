using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.Implementation;

namespace AIPos.BusinessLayer
{
    public class BOInventario
    {
        InventarioDaoImpl inventarioDaoImpl = new InventarioDaoImpl();

        public Inventario Insert(Inventario entity)
        {
            entity.FechaRegistro = Tools.TimeConverter.GetDateTimeNowMexico();
            return inventarioDaoImpl.Insert(entity);
        }

        public Inventario Update(Inventario entity)
        {
            return inventarioDaoImpl.Update(entity);
        }

        public void Delete(int Id)
        {
            inventarioDaoImpl.Delete(Id);
        }

        public List<Inventario> SelectAll()
        {
            return inventarioDaoImpl.SelectAll();
        }

        public Inventario SelectById(int Id)
        {
            return inventarioDaoImpl.SelectByKey(Id);
        }

        public List<Inventario> SelectBySucursalFecha(int SucursalId, DateTime Fecha)
        {
            int hours = Fecha.Hour;
            int minutes = Fecha.Minute;
            DateTime fechaInicio = Fecha.AddHours(hours * -1).AddMinutes(minutes * -1);
            DateTime fechaFin = Fecha.AddHours(23 - hours).AddMinutes(59 - minutes);
            return inventarioDaoImpl.SelectByFechaSucursal(SucursalId, fechaInicio, fechaFin);
        }

        public List<ReporteInventario> SelectReporteSucursalFecha(int SucursalId, DateTime Fecha)
        {
            int hours = Fecha.Hour;
            int minutes = Fecha.Minute;
            DateTime fechaInicio = Fecha.AddHours(hours * -1).AddMinutes(minutes * -1);
            DateTime fechaFin = Fecha.AddHours(23 - hours).AddMinutes(59 - minutes);
            return inventarioDaoImpl.SelectReporteFechaSucursal(SucursalId, fechaInicio, fechaFin);
        }

        public List<ReporteInventarioUtilidad> SelectReporteUtilidadSucursalFecha(int SucursalId, DateTime Fecha)
        {
            int hours = Fecha.Hour;
            int minutes = Fecha.Minute;
            DateTime fechaInicio = Fecha.AddHours(hours * -1).AddMinutes(minutes * -1);
            DateTime fechaFin = Fecha.AddHours(23 - hours).AddMinutes(59 - minutes);
            return inventarioDaoImpl.SelectReporteUtilidadFechaSucursal(SucursalId, fechaInicio, fechaFin);
        }
    }
}
