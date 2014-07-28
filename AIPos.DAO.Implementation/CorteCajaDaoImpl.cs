using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.DataContext;
using AIPos.DAO.IGeneric;
using AIPos.Domain;

namespace AIPos.DAO.Implementation
{
    public class CorteCajaDaoImpl : CorteCajaDao
    {
        ConnectionManager cm = new ConnectionManager();

        public CorteCaja Insert(CorteCaja entity)
        {
            object[] parameters = new object[] { entity.UsuarioId, entity.SucursalId, entity.Fecha, entity.TotalMostrador, entity.TotalDomicilio,
                entity.TotalServicios, entity.TotalApartados, entity.TotalRetiros, entity.TotalAbonosServicios, entity.TotalAbonosApartados,
                entity.TotalCancelados, entity.TotalCaja, entity.TotalCambio, entity.QuienRetira, entity.CorteEntregado, entity.Diferencia,
                entity.TotalTickectsDomicilio, entity.TotalTickectsMostrador, entity.TotalVentas };
            return cm.Database.SqlQuery<CorteCaja>("dbo.usp_CortesCajaInsert @UsuarioId={0}, @SucursalId={1}, @Fecha={2}, @TotalMostrador={3}, "+
                "@TotalDomicilio={4}, @TotalServicios={5}, @TotalApartados={6}, @TotalRetiros={7}, @TotalAbonosServicios={8}, @TotalAbonosApartados={9}, " +
                "@TotalCancelados={10}, @TotalCaja={11}, @TotalCambio={12}, @QuienRetira={13}, @CorteEntregado={14}, @Diferencia={15}, "+
                "@TotalTickectsDomicilio={16}, @TotalTickectsMostrador={17}, @TotalVentas={18}", parameters).FirstOrDefault();
        }

        public bool Delete(int key)
        {
            bool retorno = false;
            object[] parameters = new object[] { key };
            int result = cm.Database.ExecuteSqlCommand("dbo.usp_CortesCajaDelete @Id={0}", parameters);
            if (result == -1)
            {
                retorno = true;
            }
            return retorno;
        }

        public CorteCaja Update(CorteCaja entity)
        {
            object[] parameters = new object[] { entity.Id, entity.UsuarioId, entity.SucursalId, entity.Fecha, entity.TotalMostrador, entity.TotalDomicilio,
                entity.TotalServicios, entity.TotalApartados, entity.TotalRetiros, entity.TotalAbonosServicios, entity.TotalAbonosApartados,
                entity.TotalCancelados, entity.TotalCaja, entity.TotalCambio, entity.QuienRetira, entity.CorteEntregado, entity.Diferencia,
                entity.TotalTickectsDomicilio, entity.TotalTickectsMostrador, entity.TotalVentas };
            return cm.Database.SqlQuery<CorteCaja>("dbo.usp_CortesCajaUpdate @Id={0}, @UsuarioId={1}, @SucursalId={2}, @Fecha={3}, @TotalMostrador={4}, " +
                "@TotalDomicilio={5}, @TotalServicios={6}, @TotalApartados={7}, @TotalRetiros={8}, @TotalAbonosServicios={9}, @TotalAbonosApartados={10}, " +
                "@TotalCancelados={11}, @TotalCaja={12}, @TotalCambio={13}, @QuienRetira={14}, @CorteEntregado={15}, @Diferencia={16}, "+
                "@TotalTickectsDomicilio={17}, @TotalTickectsMostrador={18}, @TotalVentas={19}", parameters).FirstOrDefault();
        }

        public List<CorteCaja> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<CorteCaja>("dbo.usp_CortesCajaSelect @Id={0}", parameters).ToList();
        }

        public List<CorteCaja> SelectByFecha(DateTime FechaInicio, DateTime FechaFin)
        {
            object[] parameters = new object[] { FechaInicio, FechaFin };
            return cm.Database.SqlQuery<CorteCaja>("dbo.usp_CortesCajaSelectByFecha @FechaInicio={0}, @FechaFin={1}", parameters).ToList();
        }

        public CorteCaja SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<CorteCaja>("dbo.usp_CortesCajaSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
