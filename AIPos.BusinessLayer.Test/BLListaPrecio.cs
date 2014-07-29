using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AIPos.BusinessLayer.Test
{
    [TestClass]
    public class BLListaPrecio
    {
        [TestMethod]
        public void RecuperarListaPrecioDeSucursal()
        {
            AIPos.BusinessLayer.BOListaPrecio boListaPrecio = new BOListaPrecio();
            AIPos.Domain.ListaPrecio lista = boListaPrecio.RecuperarListaPrecioDeSucursal(4);
            Assert.IsNotNull(lista);
        }
    }
}
