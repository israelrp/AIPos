using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AIPos.Domain;

namespace AIPos.DekstopLayer.Ventas
{
    /// <summary>
    /// Interaction logic for RegistrarCliente.xaml
    /// </summary>
    public partial class RegistrarCliente : Window
    {
        public Cliente ClienteNuevo { get; set; }

        public RegistrarCliente()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.ClienteNuevo = null;
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text.Trim().Length > 0)
            {
                Cliente cliente = new Cliente();
                cliente.Celular = txtCelular.Text;
                cliente.Codigo = 0;
                cliente.Descuento = 0;
                cliente.Eliminado = false;
                cliente.Nombre = txtNombre.Text;
                cliente.Telefono = txtTelefono.Text;
                cliente.Rfc = "";
                cliente.RazonSocial = "";
                cliente.SucursalRegistroId = General.ConfiguracionApp.SucursalId;
                this.ClienteNuevo = new ServiceCliente.SClienteClient().Insert(cliente);
                if (expander.IsExpanded)
                {
                    ClienteNuevo.Rfc = txtRfc.Text;
                    ClienteNuevo.RazonSocial = txtRazonSocial.Text;
                    this.ClienteNuevo = new ServiceCliente.SClienteClient().Update(ClienteNuevo);
                    Direccion direccion = new Direccion();
                    direccion.Calle = txtCalle.Text;
                    direccion.Ciudad = txtCiudad.Text;
                    direccion.CodigoPostal = txtCp.Text;
                    direccion.Colonia = txtColonia.Text;
                    direccion.Estado = txtEstado.Text;
                    direccion.NoExterior = txtNoExt.Text;
                    direccion.NoInterior = txtNoInt.Text;
                    direccion.Referencia = txtReferencia.Text;
                    direccion = new ServiceDireccion.SDireccionClient().Insert(direccion);
                    DireccionFacturacion direccionFacturacion = new DireccionFacturacion();
                    direccionFacturacion.ClienteId = ClienteNuevo.Id;
                    direccionFacturacion.DireccionId = direccion.Id;
                    direccionFacturacion = new ServiceDireccionFacturacion.SDireccionFacturacionClient().Insert(direccionFacturacion);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("El nombre del cliente es obligatorio");
            }

        }

        private void Expander_Expanded_1(object sender, RoutedEventArgs e)
        {
            this.Height =440 ;
        }

        private void Expander_Collapsed_1(object sender, RoutedEventArgs e)
        {
            this.Height = 220;
        }
    }
}
