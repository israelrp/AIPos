using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;
using AIPos.BusinessLayer;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SUsuario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SUsuario.svc or SUsuario.svc.cs at the Solution Explorer and start debugging.
    public class SUsuario : ISUsuario
    {

        public Usuario Login(string Username, string Password, int SucursalId)
        {
            return new BOUsuario().Login(Username, Password, SucursalId);
        }

        public Usuario SelectById(int Id)
        {
            return new BOUsuario().SelectById(Id);
        }


        public List<Usuario> SelectAll()
        {
            return new BOUsuario().SelectAll();
        }
    }
}
