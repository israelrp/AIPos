using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AIPos.Domain;
using AIPos.BusinessLayer;
using System.Collections.Generic;

namespace AIPos.BusinessLayer.Test
{
    [TestClass]
    public class BLUnidadTest
    {
        [TestMethod]
        public void InsertNewUnidad()
        {
            Unidad unidad = new Unidad();
            BOUnidad blUnidad = new BOUnidad();
            unidad.Eliminado = false;
            unidad.Nombre = "Pieza";
            blUnidad.Insert(unidad);
        }

        [TestMethod]
        public void UpdateUnidad()
        {
            BOUnidad blUnidad = new BOUnidad();
            Unidad unidad = blUnidad.SelectById(7);
            unidad.Nombre = "Piezas";
            blUnidad.Update(unidad);
        }

        [TestMethod]
        public void SelectAll()
        {
            BOUnidad blUnidad = new BOUnidad();
            List<Unidad> unidades = blUnidad.SelectAll();
            foreach (var unidad in unidades)
            {
                Console.WriteLine(unidad.Id.ToString() + " " + unidad.Nombre + " " + unidad.Eliminado.ToString());
            }
        }

        [TestMethod]
        public void Delete()
        {
            BOUnidad blUnidad = new BOUnidad();
            blUnidad.Delete(7);
        }
    }
}
