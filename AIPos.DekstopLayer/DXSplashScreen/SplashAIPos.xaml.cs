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
using System.Xml.Serialization;
using System.IO;

namespace AIPos.DekstopLayer
{
    /// <summary>
    /// Interaction logic for SplashAIPos.xaml
    /// </summary>
    public partial class SplashAIPos : Window, ISplashScreen
    {
        bool FaltaConfiguracion = false;

        public SplashAIPos()
        {
            InitializeComponent();
            VerificarConexionServicios();
            VerificarConfiguracionSucursal();
            if (FaltaConfiguracion)
            {
                var window = new Configuracion.ConfiguracionInicial();
                window.Show();
            }
            else
            {
                ServiceSucursal.SSucursalClient sucursalClient = new ServiceSucursal.SSucursalClient();
                ServiceDireccion.SDireccionClient direccionClient = new ServiceDireccion.SDireccionClient();
                General.SucursalActual = sucursalClient.SelectById(General.ConfiguracionApp.SucursalId);
                General.SucursalActual.Direccion = direccionClient.SelectById(General.SucursalActual.DireccionId);
                var login = new Login.Login();
                login.Show();
            }
            CloseSplashScreen();
            this.board.Completed += OnAnimationCompleted;            
        }

        private void VerificarConexionServicios()
        {
            bool conectado = false;
            Info.Text = "Verificando servicio de productos...";
            ServiceProducto.SProductoClient sProductoClient = new ServiceProducto.SProductoClient();
            sProductoClient.Open();
            while (!conectado)
            {
                if (sProductoClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    this.Progress(10);
                    sProductoClient.Close();
                    conectado = true;
                }
                else if (sProductoClient.State == System.ServiceModel.CommunicationState.Opening)
                {
                    conectado = false;
                }
                else
                {
                    conectado = true;
                    MessageBox.Show("Hay problema con conectar con el servicio de productos");
                }
            }
            Info.Text = "Verificando servicio de proveedores...";
            ServiceProveedor.SProveedorClient sProveedorClient = new ServiceProveedor.SProveedorClient();
            sProveedorClient.Open();
            conectado = false;
            while (!conectado)
            {
                if (sProveedorClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    this.Progress(20);
                    sProveedorClient.Close();
                    conectado = true;
                }
                else if (sProveedorClient.State == System.ServiceModel.CommunicationState.Opening)
                {
                    conectado = false;
                }
                else
                {
                    conectado = true;
                    MessageBox.Show("Hay problema con conectar con el servicio de proveedores");
                }
            }
            Info.Text = "Verificando servicio de sucursales...";
            ServiceSucursal.SSucursalClient sSucursalClient = new ServiceSucursal.SSucursalClient();
            sSucursalClient.Open();
            conectado = false;
            while (!conectado)
            {
                if (sSucursalClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    this.Progress(30);
                    sSucursalClient.Close();
                    conectado = true;
                }
                else if (sSucursalClient.State == System.ServiceModel.CommunicationState.Opening)
                {
                    conectado = false;
                }
                else
                {
                    conectado = true;
                    MessageBox.Show("Hay problema con conectar con el servicio de sucursales");
                }
            }
            Info.Text = "Verificando servicio de usuarios...";
            ServiceUsuario.SUsuarioClient sUsuarioClient = new ServiceUsuario.SUsuarioClient();
            sUsuarioClient.Open();
            conectado = false;
            while (!conectado)
            {
                if (sUsuarioClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    this.Progress(40);
                    sUsuarioClient.Close();
                    conectado = true;
                }
                else if (sUsuarioClient.State == System.ServiceModel.CommunicationState.Opening)
                {
                    conectado = false;
                }
                else
                {
                    conectado = true;
                    MessageBox.Show("Hay problema con conectar con el servicio de usuarios");
                }
            }
        }

        private void VerificarConfiguracionSucursal()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Configuracion.Configuracion));
            if (!File.Exists("config.xml"))
            {
                File.CreateText("config.xml").Close();

            }
            TextReader textReader = new StreamReader("config.xml");
            Configuracion.Configuracion configuracion;
            try
            {
                configuracion = (Configuracion.Configuracion)deserializer.Deserialize(textReader);
                if(configuracion.PuertoBascula!=null)
                    FaltaConfiguracion = false;
                else
                    FaltaConfiguracion = true;
                General.ConfiguracionApp = configuracion;
            }
            catch (Exception ex)
            {
                FaltaConfiguracion = true;
            }
            textReader.Close();
        }

        #region ISplashScreen
        public void Progress(double value)
        {
            progressBar.Value = value;
        }
        public void CloseSplashScreen()
        {
            this.board.Begin(this);
        }
        public void SetProgressState(bool isIndeterminate)
        {
            progressBar.IsIndeterminate = isIndeterminate;
        }
        #endregion

        #region Event Handlers
        void OnAnimationCompleted(object sender, EventArgs e)
        {
            this.board.Completed -= OnAnimationCompleted;
            this.Close();
        }
        #endregion
    }
}
