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
    /// Interaction logic for RegistrarDireccion.xaml
    /// </summary>
    public partial class RegistrarDireccion : Window
    {

        public int ClienteId { get; set; }

        private Direccion direccionNueva;
        public Direccion DireccionNueva
        {
            get
            {
                return direccionNueva;
            }
            set
            {
                direccionNueva = value;
                txtCalle.Text = DireccionNueva.Calle;
                txtCiudad.Text = DireccionNueva.Ciudad;
                txtColonia.Text = DireccionNueva.Colonia;
                txtCp.Text = DireccionNueva.CodigoPostal;
                txtEstado.Text = DireccionNueva.Estado;
                txtNoExt.Text = DireccionNueva.NoExterior;
                txtNoInt.Text = DireccionNueva.NoInterior;
                txtReferencia.Text = DireccionNueva.Referencia;
                Editar = true;
            }

        }
        private bool Editar = false;

        public RegistrarDireccion()
        {
            InitializeComponent();
            if (DireccionNueva != null)
            {
                txtCalle.Text = DireccionNueva.Calle;
                txtCiudad.Text = DireccionNueva.Ciudad;
                txtColonia.Text = DireccionNueva.Colonia;
                txtCp.Text = DireccionNueva.CodigoPostal;
                txtEstado.Text = DireccionNueva.Estado;
                txtNoExt.Text = DireccionNueva.NoExterior;
                txtNoInt.Text = DireccionNueva.NoInterior;
                txtReferencia.Text = DireccionNueva.Referencia;
                Editar = true;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Direccion direccion = new Direccion();
            direccion.Calle = txtCalle.Text;
            direccion.Ciudad = txtCiudad.Text;
            direccion.CodigoPostal = txtCp.Text;
            direccion.Colonia = txtColonia.Text;
            direccion.Estado = txtEstado.Text;
            direccion.NoExterior = txtNoExt.Text;
            direccion.NoInterior = txtNoInt.Text;
            direccion.Referencia = txtReferencia.Text;
            
            if (Editar == false)
            {
                this.DireccionNueva = new ServiceDireccion.SDireccionClient().Insert(direccion);
                DireccionEnvio direccionEnvio = new DireccionEnvio();
                direccionEnvio.ClienteId = this.ClienteId;
                direccionEnvio.DireccionId = this.DireccionNueva.Id;
                ServiceDireccionEnvio.SDireccionEnvioClient direcccionEnvioClient = new ServiceDireccionEnvio.SDireccionEnvioClient();
                direcccionEnvioClient.Insert(direccionEnvio);
            }
            else
            {
                direccion.Id = this.DireccionNueva.Id;
                this.DireccionNueva = new ServiceDireccion.SDireccionClient().Update(direccion);
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DireccionNueva = null;
            this.Close();
        }
    }
}
