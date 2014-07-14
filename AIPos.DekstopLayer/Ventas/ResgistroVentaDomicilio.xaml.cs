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
    /// Interaction logic for ResgistroVentaDomicilio.xaml
    /// </summary>
    public partial class ResgistroVentaDomicilio : Window
    {
        public ServicioApartado ServicioApartadoVenta { get; set; }
        public Direccion DireccionEnvio { get; set; }

        private int clienteId;
        public int ClienteId
        {
            get
            {
                return clienteId;
            }
            set
            {
                clienteId = value;
                List<Direccion> direcciones = new ServiceDireccion.SDireccionClient().SelectByCliente(this.clienteId).ToList();
                cmbDireccionEntrega.ItemsSource = direcciones;
                if (direcciones.Count > 0)
                {
                    cmbDireccionEntrega.SelectedIndex = 0;
                    MostrarDatosDireccion(direcciones.FirstOrDefault());
                }
            }

        }

        public ResgistroVentaDomicilio()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.ServicioApartadoVenta = null;
            if (cmbDireccionEntrega.SelectedItem != null)
            {
                ServicioApartado servicioApartado = new ServicioApartado();
                servicioApartado.Anticipo = 0;
                servicioApartado.DireccionEnvioId = ((Direccion)cmbDireccionEntrega.SelectedItem).Id;
                servicioApartado.Especificaciones = "";
                //Nuevo
                servicioApartado.Tipo = 0;
                servicioApartado.EstatusId = 4;
                servicioApartado.FechaEntrega = DateTime.Now;
                this.ServicioApartadoVenta = servicioApartado;
                this.Close();
            }
            else
            {
                MessageBox.Show("Debes de seleccionar una dirección del listado");
            }
        }

        private void btnNuevaDireccion_Click(object sender, RoutedEventArgs e)
        {
            RegistrarDireccion registrarDireccion = new RegistrarDireccion();
            registrarDireccion.ClienteId = this.ClienteId;
            registrarDireccion.ShowDialog();
            if (registrarDireccion.DireccionNueva != null)
            {
                cmbDireccionEntrega.ItemsSource = new ServiceDireccion.SDireccionClient().SelectByCliente(this.ClienteId);
                cmbDireccionEntrega.SelectedItem = registrarDireccion.DireccionNueva;
            }
        }

        private void MostrarDatosDireccion()
        {
            Direccion direccion = (Direccion)cmbDireccionEntrega.SelectedItem;
            txtDireccionEnvio.Text = direccion.Calle + " " + direccion.NoExterior + " " +
                direccion.NoInterior + " " + direccion.Colonia + " " + direccion.CodigoPostal + " " + direccion.Ciudad + " " +
                direccion.Estado + " " + direccion.Referencia;
        }

        private void MostrarDatosDireccion(Direccion direccion)
        {
            txtDireccionEnvio.Text = direccion.Calle + " " + direccion.NoExterior + " " +
                direccion.NoInterior + " " + direccion.Colonia + " " + direccion.CodigoPostal + " " + direccion.Ciudad + " " +
                direccion.Estado + " " + direccion.Referencia;
        }

        private void cmbDireccionEntrega_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbDireccionEntrega.SelectedItem != null)
            {
                MostrarDatosDireccion();
            }
        }

        private void btnEditarDireccion_Click(object sender, RoutedEventArgs e)
        {
            if (cmbDireccionEntrega.SelectedItem != null)
            {
                RegistrarDireccion registrarDireccion = new RegistrarDireccion();
                registrarDireccion.DireccionNueva = (Direccion)cmbDireccionEntrega.SelectedItem;
                registrarDireccion.ShowDialog();
                cmbDireccionEntrega.ItemsSource = new ServiceDireccion.SDireccionClient().SelectByCliente(this.clienteId);
                Direccion direccion = registrarDireccion.DireccionNueva;
                cmbDireccionEntrega.SelectedItem = direccion;
                txtDireccionEnvio.Text = direccion.Calle + " " + direccion.Colonia + " " + direccion.NoExterior + " " +
                    direccion.NoInterior + " " + direccion.CodigoPostal + " " + direccion.Ciudad + " " +
                    direccion.Estado + " " + direccion.Referencia;
            }
            else
            {
                MessageBox.Show("Debes de seleccionar una dirección del listado, sino hay agrega una nueva");
            }
        }
    }
}
