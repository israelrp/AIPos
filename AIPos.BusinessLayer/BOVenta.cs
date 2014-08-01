using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO;
using AIPos.DAO.Implementation;
using AIPos.Domain;
using System.Globalization;

namespace AIPos.BusinessLayer
{
    public class BOVenta
    {
        VentaDaoImpl ventaDaoImpl = new VentaDaoImpl();

        public Venta Insert(Venta venta)
        {
            List<VentaDetalle> ventasDetalle = venta.VentasDetalle;
            venta.Fecha = Tools.TimeConverter.GetDateTimeNowMexico();
            Venta ventaInsertada = ventaDaoImpl.Insert(venta);
            foreach (var ventaDetalle in ventasDetalle)
            {
                BOVentaDetalle boVentaDetalle = new BOVentaDetalle();
                ventaDetalle.VentaId = ventaInsertada.Id;
                boVentaDetalle.Insert(ventaDetalle);
            }
            ventaInsertada.VentasDetalle = venta.VentasDetalle;
            ventaInsertada.Usuario = venta.Usuario;
            ventaInsertada.Sucursal = venta.Sucursal;
            ventaInsertada.Cliente = venta.Cliente;
            return ventaInsertada;
        }

        public Venta Update(Venta venta)
        {
            venta.Fecha = Tools.TimeConverter.GetDateTimeNowMexico();
            return ventaDaoImpl.Update(venta);
        }

        public void Delete(int Id)
        {
            ventaDaoImpl.Delete(Id);
        }

        public Venta SelectById(int Id)
        {
            return ventaDaoImpl.SelectByKey(Id);
        }

        public Venta SelectByFolio(int Folio, int SucursalId)
        {
            return ventaDaoImpl.SelectBySucursalFolio(SucursalId, Folio);
        }

        public Venta SelectByFolioCancelado(int FolioCancelado, int SucursalId)
        {
            return ventaDaoImpl.SelectBySucursalFolioCancelado(SucursalId, FolioCancelado);
        }

        public List<Venta> SelectAll()
        {
            return ventaDaoImpl.SelectAll();
        }

        public int GenerarFolioVenta(int SucursalId)
        {
            return ventaDaoImpl.GenerarFolioVenta(SucursalId);
        }

        public int GenerarFolioCancelado(int SucursalId, DateTime Fecha)
        {
            int hours = Fecha.Hour;
            int minutes = Fecha.Minute;
            DateTime fechaInicio = Fecha.AddHours(hours * -1).AddMinutes(minutes * -1);
            DateTime fechaFin = Fecha.AddHours(23 - hours).AddMinutes(59 - minutes);
            return ventaDaoImpl.GenerarFolioCancelado(SucursalId, fechaInicio, fechaFin);
        }

        public int GenerarFolioCanceladoApartado(int SucursalId, DateTime Fecha)
        {
            int hours = Fecha.Hour;
            int minutes = Fecha.Minute;
            DateTime fechaInicio = Fecha.AddHours(hours * -1).AddMinutes(minutes * -1);
            DateTime fechaFin = Fecha.AddHours(23 - hours).AddMinutes(59 - minutes);
            return ventaDaoImpl.GenerarFolioCanceladoApartado(SucursalId, fechaInicio, fechaFin);
        }

        public int GenerarFolioCanceladoDomicilio(int SucursalId, DateTime Fecha)
        {
            int hours = Fecha.Hour;
            int minutes = Fecha.Minute;
            DateTime fechaInicio = Fecha.AddHours(hours * -1).AddMinutes(minutes * -1);
            DateTime fechaFin = Fecha.AddHours(23 - hours).AddMinutes(59 - minutes);
            return ventaDaoImpl.GenerarFolioCanceladoDomicilio(SucursalId, fechaInicio, fechaFin);
        }

        public int GenerarFolioCanceladoServicio(int SucursalId, DateTime Fecha)
        {
            int hours = Fecha.Hour;
            int minutes = Fecha.Minute;
            DateTime fechaInicio = Fecha.AddHours(hours * -1).AddMinutes(minutes * -1);
            DateTime fechaFin = Fecha.AddHours(23 - hours).AddMinutes(59 - minutes);
            return ventaDaoImpl.GenerarFolioCanceladoServicio(SucursalId, fechaInicio, fechaFin);
        }

        public List<ReporteCorteCaja> RecuperarCorteCaja(DateTime FechaInicio, DateTime FechaFin, int? SucursalId)
        {
            int hoursInicio = FechaInicio.Hour;
            int minutesInicio = FechaInicio.Minute;
            int hoursFin = FechaFin.Hour;
            int minutesFin = FechaFin.Minute;
            DateTime fechaInicio = FechaInicio.AddHours(hoursInicio * -1).AddMinutes(minutesInicio * -1);
            DateTime fechaFin = FechaFin.AddHours(23 - hoursFin).AddMinutes(59 - minutesFin);
            if (SucursalId.HasValue)
            {
                return ventaDaoImpl.RecuperarCorteCaja(fechaInicio, fechaFin).Where(x=>x.SucursalId==SucursalId).ToList();
            }
            return ventaDaoImpl.RecuperarCorteCaja(fechaInicio, fechaFin);
        }

        public List<ReporteVentasProducto> RecuperarVentasProducto(int SucursalId)
        {
            return ventaDaoImpl.RecuperarVentasProducto(SucursalId);
        }

        /// <summary>
        /// Recuperar el resumen de totales de un tipo de venta por semana
        /// </summary>
        /// <param name="tipo">Tipo de la venta (Venta, orden, servicio, apartado o domicilio)</param>
        /// <returns></returns>
        public List<ConteoVenta> RecuperarResumenSemanal(AIPos.DAO.Implementation.Enums.TipoVenta tipo)
        {
            List<ConteoVenta> resumen = new List<ConteoVenta>();
            int semana=DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(DateTime.Now, DateTimeFormatInfo.CurrentInfo.CalendarWeekRule, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
            DateTime fechaActual = DateTime.Now;
            int DiasRecuperar = 6;
            for (int Dia = 0; Dia <= DiasRecuperar; Dia++)
            {
                int quitarDia=(Dia*-1);
                DateTime fechaInicio = Tools.DateTimeManager.AbsoluteStart(fechaActual.AddDays(quitarDia));
                DateTime fechaFin = Tools.DateTimeManager.AbsoluteEnd(fechaActual.AddDays(quitarDia));
                ConteoVenta resumenDiario = ventaDaoImpl.RecuperarResumenVenta(fechaInicio, fechaFin, tipo);
                if (resumenDiario == null)
                {
                    resumenDiario = new ConteoVenta() { Conteo = 0, Fecha = fechaInicio, ImporteDia = 0 };
                }
                resumen.Add(resumenDiario);
            }
            return resumen;
        }
    }
}
