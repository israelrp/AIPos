using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.BusinessLayer;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SInventario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SInventario.svc or SInventario.svc.cs at the Solution Explorer and start debugging.
    public class SInventario : ISInventario
    {


        public Inventario Insert(Inventario entity)
        {
            BOInventario boInventario = new BOInventario();
            entity.FechaRegistro = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(entity.FechaRegistro.ToUniversalTime());
            return boInventario.Insert(entity);
        }

        public Inventario Update(Inventario entity)
        {
            BOInventario boInventario = new BOInventario();
            entity.FechaRegistro = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(entity.FechaRegistro.ToUniversalTime());
            return boInventario.Update(entity);
        }

        public void Delete(int Id)
        {
            BOInventario boInventario = new BOInventario();
            boInventario.Delete(Id);
        }

        public List<Inventario> SelectBySucursalFecha(int SucursalId, long Day)
        {
            BOInventario boInventario = new BOInventario();
            DateTime fecha = DateTime.FromFileTimeUtc(Day);
            fecha = BusinessLayer.Tools.TimeConverter.GetDateTimeMexico(fecha);
            return boInventario.SelectBySucursalFecha(SucursalId, fecha);
        }
    }
}
