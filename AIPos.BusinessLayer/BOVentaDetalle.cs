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
    public class BOVentaDetalle
    {
        VentaDetalleDaoImpl ventaDetalleDaoImpl = new VentaDetalleDaoImpl();

        public VentaDetalle Insert(VentaDetalle ventaDetalle)
        {
            return ventaDetalleDaoImpl.Insert(ventaDetalle);
        }

        public VentaDetalle Update(VentaDetalle ventaDetalle)
        {
            return ventaDetalleDaoImpl.Update(ventaDetalle);
        }

        public void Delete(int Id)
        {
            ventaDetalleDaoImpl.Delete(Id);
        }

        public List<VentaDetalle> SelectByVentaId(int VentaId)
        {
            return ventaDetalleDaoImpl.SelectAll().Where(x => x.VentaId == VentaId).ToList();
        }


    }
}
