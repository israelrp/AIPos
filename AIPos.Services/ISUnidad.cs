using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISUnidad" in both code and config file together.
    [ServiceContract]
    public interface ISUnidad
    {
        [OperationContract]
        Unidad SelectById(int Id);

        [OperationContract]
        List<Unidad> SelectAll();
    }
}
