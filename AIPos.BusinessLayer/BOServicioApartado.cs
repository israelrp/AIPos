using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.DAO.Implementation;

namespace AIPos.BusinessLayer
{
    public class BOServicioApartado
    {
        ServicioApartadoDaoImpl servicioApartadoDaoImpl = new ServicioApartadoDaoImpl();

        public List<ServicioApartado> SelectAll()
        {
            return servicioApartadoDaoImpl.SelectAll();
        }

        public ServicioApartado SelectById(int Id)
        {
            return servicioApartadoDaoImpl.SelectByKey(Id);
        }

        public ServicioApartado Insert(ServicioApartado servicioApartado)
        {
            return servicioApartadoDaoImpl.Insert(servicioApartado);
        }

        public ServicioApartado Update(ServicioApartado servicioApartado)
        {
            return servicioApartadoDaoImpl.Update(servicioApartado);
        }

        public void Delete(int Id)
        {
            servicioApartadoDaoImpl.Delete(Id);
        }

        public List<ReporteServicioApartado> RecuperarReporteServicioApartado(int SucursalId, int? EstatusId)
        {
            return servicioApartadoDaoImpl.RecuperarReporteServiciosApartados(SucursalId, EstatusId);
        }
    }
}
