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
    public class ConfiguracionGeneralDaoImpl : ConfiguracionGeneralDao
    {
        ConnectionManager cm = new ConnectionManager();

        public ConfiguracionGeneral Update(ConfiguracionGeneral entity)
        {
            object[] parameters = new object[] { entity.Id, entity.ActivarBascula, entity.ActivarTicketPesaje, entity.ActivarFotoProductoPOS, entity.NumeroCopiasTicketVenta,
                entity.DecimalesPrecioProducto, entity.DecimalesCantidad, entity.LogoTicket, entity.TituloTicket, entity.AgradecimientoTicket, entity.LeyendaFisalTicket,
                entity.ActivarImprimirFechaHoraTicket };
            return cm.Database.SqlQuery<ConfiguracionGeneral>("dbo.usp_ConfiguracionGeneralUpdate @Id={0}, @ActivarBascula={1}, @ActivarTicketPesaje={2}, @ActivarFotoProductoPOS={3}, "+
                " @NumeroCopiasTicketVenta={4}, @DecimalesPrecioProducto={5}, @DecimalesCantidad={6}, @LogoTicket={7}, @TituloTicket={8}, @AgradecimientoTicket={9}, " +
                " @LeyendaFisalTicket={10}, @ActivarImprimirFechaHoraTicket={11}", parameters).FirstOrDefault();
        }

        public List<ConfiguracionGeneral> SelectAll()
        {
            object[] parameters = new object[] { null };
            return cm.Database.SqlQuery<ConfiguracionGeneral>("dbo.usp_ConfiguracionGeneralSelect @Id={0}", parameters).ToList();
        }

        public ConfiguracionGeneral SelectByKey(int key)
        {
            object[] parameters = new object[] { key };
            return cm.Database.SqlQuery<ConfiguracionGeneral>("dbo.usp_ConfiguracionGeneralSelect @Id={0}", parameters).FirstOrDefault();
        }
    }
}
