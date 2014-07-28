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
    /// Interaction logic for RegistroApartadoServicio.xaml
    /// </summary>
    public partial class RegistroApartadoServicio : Window
    {
        public ServicioApartado ServicioApartadoVenta { get; set; }
        public decimal TotalVenta { get; set; }

        private int clienteId;
        public int ClienteId
        {
            get
            {
                return clienteId;
            }
            set{
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

        public RegistroApartadoServicio()
        {
            InitializeComponent();
            deFechaHoraEntrega.DateTime = DateTime.Now;
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.ServicioApartadoVenta = null;
            decimal anticipo = 0;
            if (decimal.TryParse(txtAnticipo.Text, out anticipo))
            {
                if (anticipo <= TotalVenta)
                {
                    if (cmbDireccionEntrega.SelectedItem != null)
                    {
                        ServicioApartado servicioApartado = new ServicioApartado();
                        servicioApartado.Anticipo = anticipo;
                        servicioApartado.DireccionEnvioId = ((Direccion)cmbDireccionEntrega.SelectedItem).Id;
                        servicioApartado.Especificaciones = txtEspecificaciones.Text;
                        if (chkEsServicio.IsChecked.Value)
                        {
                            servicioApartado.Tipo = 2;
                        }
                        else
                        {
                            servicioApartado.Tipo = 1;
                        }
                        //Nuevo
                        servicioApartado.EstatusId = 4;
                        servicioApartado.FechaEntrega = deFechaHoraEntrega.DateTime;
                        this.ServicioApartadoVenta = servicioApartado;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Debes de seleccionar una dirección del listado");
                    }
                }
                else
                {
                    MessageBox.Show("El anticipo debe ser igual o menor que el total de la venta.");
                }
            }
            else
            {
                MessageBox.Show("El anticipo debe ser una cifra númerica");
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
