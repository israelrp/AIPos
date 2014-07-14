using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISUsuario" in both code and config file together.
    [ServiceContract]
    public interface ISUsuario
    {
        [OperationContract]
        Usuario Login(string Username, string Password, int Sucursal);

        [OperationContract]
        Usuario SelectById(int Id);

        [OperationContract]
        List<Usuario> SelectAll();
    }
}
