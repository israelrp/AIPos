﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AIPos.Domain;

namespace AIPos.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISTipoProducto" in both code and config file together.
    [ServiceContract]
    public interface ISTipoProducto
    {
        [OperationContract]
        List<TipoProducto> SelectAll();

        [OperationContract]
        TipoProducto SelectById(int Id);
    }
}
