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
    public class BOConfiguracionGeneral
    {
        public ConfiguracionGeneralDaoImpl dao = new ConfiguracionGeneralDaoImpl();

        public ConfiguracionGeneral ObtenerConfiguracion()
        {
            ConfiguracionGeneral retorno = new ConfiguracionGeneral();
            List<ConfiguracionGeneral> data = dao.SelectAll();
            if (data != null)
            {
                retorno = data.FirstOrDefault();
            }
            return retorno;
        }

        public ConfiguracionGeneral ActualizarConfiguracion(ConfiguracionGeneral item)
        {
            if(item.LogoTicket==null)
                item.LogoTicket=new byte[0];
            return dao.Update(item);
        }
    }
}
