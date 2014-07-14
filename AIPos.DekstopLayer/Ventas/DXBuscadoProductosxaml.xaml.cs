using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using AIPos.Domain;
using DevExpress.Xpf.Editors.Settings;


namespace AIPos.DekstopLayer.Ventas
{
    /// <summary>
    /// Interaction logic for DXBuscadoProductosxaml.xaml
    /// </summary>
    public partial class DXBuscadoProductosxaml : DXWindow
    {
        public Producto ProductoSeleccionado { get; set; }

        public DXBuscadoProductosxaml()
        {
            InitializeComponent();
            gridProductos.View.SearchControl = scProductos;
            ServiceProducto.SProductoClient sProductoClient = new ServiceProducto.SProductoClient();
            ((ComboBoxEditSettings)gridProductos.Columns["UnidadId"].EditSettings).ItemsSource = new ServiceUnidad.SUnidadClient().SelectAll();
            ((ComboBoxEditSettings)gridProductos.Columns["TipoId"].EditSettings).ItemsSource = new ServiceTipo.STipoClient().SelectAll();
            gridProductos.ItemsSource = sProductoClient.SelectAllProductos();
        }

        private void gridProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductoSeleccionado = (Producto)gridProductos.SelectedItem;
            this.Close();
        }


    }
}
