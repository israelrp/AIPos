using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DAO;
using AIPos.DAO.Implementation;
using AIPos.Domain;
using System.Globalization;
using AIPos.DAO.Implementation.Enums;

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
            Venta ventaActualizada = ventaDaoImpl.Update(venta);
            ventaActualizada.VentasDetalle = venta.VentasDetalle;
            ventaActualizada.Usuario = venta.Usuario;
            ventaActualizada.Sucursal = venta.Sucursal;
            ventaActualizada.Cliente = venta.Cliente;
            return ventaActualizada;
        }

        public void Delete(int Id)
        {
            ventaDaoImpl.Delete(Id);
        }

        public Venta SelectById(int Id)
        {
            return ventaDaoImpl.SelectByKey(Id);
        }

        public Venta SelectByTipoFolio(TipoVenta Tipo,int SucursalId, DateTime Fecha, int Folio)
        {
            DateTime FechaInicio=Tools.DateTimeManager.AbsoluteStart(Fecha);
            DateTime FechaFin=Tools.DateTimeManager.AbsoluteEnd(Fecha);
            return ventaDaoImpl.SelectByTipoSucursalFechaFolio((byte)Tipo,SucursalId,FechaInicio,FechaFin,Folio);
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

        public List<ReporteCorteCaja> RecuperarVentasFacturar()
        {
            return ventaDaoImpl.RecuperarVentasFacturar();
        }

        public List<ReporteVentasProducto> RecuperarVentasProducto(int SucursalId, int Semana, int Año)
        {
            DateTime fechaInicioAño = new DateTime(Año, 1, 1);
            int numeroDias = Semana * 7;
            DateTime fechaFinSemana = fechaInicioAño.AddDays(numeroDias - 4);
            DateTime fechaInicioSemana = fechaFinSemana.AddDays(-6);
            return ventaDaoImpl.RecuperarVentasProducto(SucursalId, fechaInicioSemana, fechaFinSemana);
        }

        /// <summary>
        /// Recuperar el resumen de totales de un tipo de venta por semana
        /// </summary>
        /// <param name="tipo">Tipo de la venta (Venta, orden, servicio, apartado o domicilio)</param>
        /// <returns></returns>
        public List<ConteoVenta> RecuperarResumenSemanal(AIPos.DAO.Implementation.Enums.TipoVenta tipo)
        {
            List<ConteoVenta> resumen = new List<ConteoVenta>();
            DateTime fechaFinSemana = Tools.DateTimeManager.EndOfWeek(DateTime.Now, DayOfWeek.Saturday);
            int DiasRecuperar = 6;
            for (int Dia = 0; Dia <= DiasRecuperar; Dia++)
            {
                int quitarDia=(Dia*-1);
                DateTime fechaInicio = Tools.DateTimeManager.AbsoluteStart(fechaFinSemana.AddDays(quitarDia));
                DateTime fechaFin = Tools.DateTimeManager.AbsoluteEnd(fechaFinSemana.AddDays(quitarDia));
                ConteoVenta resumenDiario = ventaDaoImpl.RecuperarResumenVenta(fechaInicio, fechaFin, tipo);
                if (resumenDiario == null)
                {
                    resumenDiario = new ConteoVenta() { Conteo = 0, Fecha = fechaInicio, ImporteDia = 0 };
                }
                resumen.Add(resumenDiario);
            }
            return resumen;
        }

        public List<ConteoVenta> RecuperarResumenSemanal(AIPos.DAO.Implementation.Enums.TipoVenta tipo, int Semana, int Año)
        {
            List<ConteoVenta> resumen = new List<ConteoVenta>();
            DateTime fechaInicioAño = new DateTime(Año, 1, 1);
            int numeroDias = Semana * 7;
            DateTime fechaFinSemana = fechaInicioAño.AddDays(numeroDias-4);
            int DiasRecuperar = 6;
            for (int Dia = 0; Dia <= DiasRecuperar; Dia++)
            {
                int quitarDia = (Dia * -1);
                DateTime fechaInicio = Tools.DateTimeManager.AbsoluteStart(fechaFinSemana.AddDays(quitarDia));
                DateTime fechaFin = Tools.DateTimeManager.AbsoluteEnd(fechaFinSemana.AddDays(quitarDia));
                ConteoVenta resumenDiario = ventaDaoImpl.RecuperarResumenVenta(fechaInicio, fechaFin, tipo);
                if (resumenDiario == null)
                {
                    resumenDiario = new ConteoVenta() { Conteo = 0, Fecha = fechaInicio, ImporteDia = 0 };
                }
                resumen.Add(resumenDiario);
            }
            return resumen;
        }

        public List<ConteoVenta> RecuperarResumenSemanal(AIPos.DAO.Implementation.Enums.TipoVenta tipo, int Semana, int Año, string SucursalIds)
        {
            List<ConteoVenta> resumen = new List<ConteoVenta>();
            DateTime fechaInicioAño = new DateTime(Año, 1, 1);
            int numeroDias = Semana * 7;
            DateTime fechaFinSemana = fechaInicioAño.AddDays(numeroDias - 4);
            int DiasRecuperar = 6;
            int indexSucursal = 0;
            string sqlStrWhere = " AND ";
            List<string> sucursales = SucursalIds.Split(',').ToList();
            foreach (string id in sucursales)
            {
                if(indexSucursal==0)
                    sqlStrWhere += " (Ventas.SucursalId=" + id;
                else
                    sqlStrWhere += " OR Ventas.SucursalId=" + id;
                indexSucursal += 1;
            }
            sqlStrWhere += ")";
            for (int Dia = 0; Dia <= DiasRecuperar; Dia++)
            {
                int quitarDia = (Dia * -1);
                DateTime fechaInicio = Tools.DateTimeManager.AbsoluteStart(fechaFinSemana.AddDays(quitarDia));
                DateTime fechaFin = Tools.DateTimeManager.AbsoluteEnd(fechaFinSemana.AddDays(quitarDia));
                ConteoVenta resumenDiario = ventaDaoImpl.RecuperarResumenVentaBySucursal(fechaInicio, fechaFin, tipo,sqlStrWhere);
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
