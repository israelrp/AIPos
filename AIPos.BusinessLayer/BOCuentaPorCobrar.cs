using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIPos.Domain;
using AIPos.BusinessLayer;
using AIPos.DAO.Implementation;

namespace AIPos.BusinessLayer
{
    public class BOCuentaPorCobrar
    {
        CuentasPorCobrarDaoImpl cxcDaoImpl = new CuentasPorCobrarDaoImpl();

        public CuentaPorCobrar Insert(CuentaPorCobrar cuentaPorCobrar)
        {
            return cxcDaoImpl.Insert(cuentaPorCobrar);
        }

        public List<CuentaPorCobrar> SelectByCliente(int ClienteId)
        {
            List<CuentaPorCobrar> cuentasPorCobrar = cxcDaoImpl.SelectAll().Where(x => x.ClienteId == ClienteId).ToList();
            return cuentasPorCobrar;
        }

        public List<CuentaPorCobrar> SelectByClienteEstatus(int ClienteId, byte Estatus)
        {
            List<CuentaPorCobrar> cuentasPorCobrar = cxcDaoImpl.SelectAll().Where(x => x.ClienteId == ClienteId && x.Estatus==Estatus).ToList();
            return cuentasPorCobrar;
        }
    }
}
