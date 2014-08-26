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
using System.ServiceModel;


namespace AIPos.DekstopLayer.Ventas
{
    /// <summary>
    /// Interaction logic for DXBuscadorCliente.xaml
    /// </summary>
    public partial class DXBuscadorCliente : DXWindow
    {
        public Cliente ClienteSeleccionado { get; set; }

        public DXBuscadorCliente()
        {
            InitializeComponent();
            gridClientes.View.SearchControl = scClientes;
            try
            {
                ServiceCliente.SClienteClient sClienteClient = new ServiceCliente.SClienteClient();
                gridClientes.ItemsSource = sClienteClient.SelectAll();
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Ha ocurrido un problema con la conexión al servicio de principal del sistema. Detalles: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridClientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClienteSeleccionado = (Cliente)gridClientes.SelectedItem;
            this.Close();
        }
    }
}
