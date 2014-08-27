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
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using System.ServiceModel;


namespace AIPos.DekstopLayer.Configuracion
{
    /// <summary>
    /// Interaction logic for ConfiguracionInicial.xaml
    /// </summary>
    public partial class ConfiguracionInicial : DXWindow
    {
        public bool Reconfigurar { get; set; }

        public ConfiguracionInicial()
        {
            InitializeComponent();
            RecuperarSucursales();
            RecuperarImpresoras();
            RecuperarPuertosCOM();
            RecuperarListaDePreciosBase();
        }

        public ConfiguracionInicial(bool reconfigurar)
        {
            Reconfigurar = reconfigurar;
            InitializeComponent();
            RecuperarSucursales();
            RecuperarImpresoras();
            RecuperarPuertosCOM();
            RecuperarListaDePreciosBase();
            if (reconfigurar)
            {
                try
                {
                    ServiceSucursal.SSucursalClient sucursalClient = new ServiceSucursal.SSucursalClient();
                    ServiceListaPrecio.SListaPrecioClient listaPrecioClient = new ServiceListaPrecio.SListaPrecioClient();
                    cmbBasculas.SelectedItem = General.ConfiguracionApp.PuertoBascula;
                    cmbImpresoras.SelectedItem = General.ConfiguracionApp.MiniPrinter;
                    cmbSucursales.SelectedItem = sucursalClient.SelectById(General.ConfiguracionApp.SucursalId);
                    cmbListaPrecioMayoreo.SelectedItem = listaPrecioClient.SelectAll().Where(x => x.Id == General.ConfiguracionApp.ListaPrecioMayoreoId).FirstOrDefault();
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
                chkViewer.IsChecked = General.ConfiguracionApp.ImpresoraVirtual;
            }
        }


        private void RecuperarListaDePreciosBase()
        {
            string nombreListaPrecio = "";
            ServiceListaPrecio.SListaPrecioClient sListaPrecioClient = new ServiceListaPrecio.SListaPrecioClient();
            try
            {
                if (General.ConfiguracionApp != null)
                {
                    ListaPrecio listaPrecio = sListaPrecioClient.RecuperarListaPrecioDeSucursal(General.ConfiguracionApp.SucursalId);
                    nombreListaPrecio = listaPrecio.Nombre;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "La sucursal no tiene asignada una lista de precios")
                {
                    nombreListaPrecio = "PRECIO BASE";
                }
                else
                {
                    MessageBox.Show("Mensaje: "+ex.Message+", StackTrace: "+ex.StackTrace,"Error para recuperar lista precio de sucursal");
                }
            }
            lblListaPrecioBase.Content ="La lista de precios base para esta sucursal es " + nombreListaPrecio +", se puede configurar desde la parte administrativa";
            
        }

        private void RecuperarImpresoras()
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbImpresoras.Items.Add(printer);
            }
        }

        private void RecuperarPuertosCOM()
        {
            foreach (var puerto in System.IO.Ports.SerialPort.GetPortNames())
            {
                cmbBasculas.Items.Add(puerto);
            }
        }

        private void RecuperarSucursales()
        {
            try
            {
                ServiceSucursal.SSucursalClient sSucursalCliente = new ServiceSucursal.SSucursalClient();
                List<Sucursal> sucursales = sSucursalCliente.SelectAll().ToList();
                cmbSucursales.ItemsSource = sucursales;
                ServiceListaPrecio.SListaPrecioClient sListaPrecioClient = new ServiceListaPrecio.SListaPrecioClient();
                List<ListaPrecio> listaPrecio = sListaPrecioClient.SelectAll().ToList();
                cmbListaPrecioMayoreo.ItemsSource = listaPrecio;
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

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbListaPrecioMayoreo.SelectedItem != null)
            {
                if (cmbSucursales.SelectedItemValue != null)
                {
                    if (cmbImpresoras.SelectedItemValue != null)
                    {
                        Configuracion configuracion = new Configuracion();
                        if (cmbBasculas.SelectedItemValue != null)
                        {
                            configuracion.PuertoBascula = cmbBasculas.SelectedItemValue.ToString();
                            configuracion.SucursalId = ((Sucursal)cmbSucursales.SelectedItemValue).Id;
                            configuracion.ListaPrecioMayoreoId = ((ListaPrecio)cmbListaPrecioMayoreo.SelectedItemValue).Id;
                            configuracion.MiniPrinter = cmbImpresoras.SelectedItemValue.ToString();
                            configuracion.ImpresoraVirtual = chkViewer.IsChecked.Value;
                            XmlSerializer serializer = new XmlSerializer(typeof(Configuracion));
                            TextWriter textWriter = new StreamWriter("config.xml");
                            serializer.Serialize(textWriter, configuracion);
                            textWriter.Close();
                            General.ConfiguracionApp = configuracion;
                            if (!Reconfigurar)
                            {
                                this.Close();
                                var splash = new SplashAIPos();
                                splash.Show();
                            }
                            else { MessageBox.Show("Configuración guardada"); }
                        }
                        else
                        {
                            if (MessageBox.Show("¿Deseas trabajar sin conexión a una báscula? En caso contrario selecciona una de la lista.", "Configuración", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                configuracion.PuertoBascula = "";
                                configuracion.SucursalId = ((Sucursal)cmbSucursales.SelectedItemValue).Id;
                                configuracion.ListaPrecioMayoreoId = ((ListaPrecio)cmbListaPrecioMayoreo.SelectedItemValue).Id;
                                configuracion.MiniPrinter = cmbImpresoras.SelectedItemValue.ToString();
                                configuracion.ImpresoraVirtual = chkViewer.IsChecked.Value;
                                XmlSerializer serializer = new XmlSerializer(typeof(Configuracion));
                                TextWriter textWriter = new StreamWriter("config.xml");
                                serializer.Serialize(textWriter, configuracion);
                                textWriter.Close();
                                General.ConfiguracionApp = configuracion;
                                if (!Reconfigurar)
                                {
                                    this.Close();
                                    var splash = new SplashAIPos();
                                    splash.Show();
                                }
                                else { MessageBox.Show("Configuración guardada"); }
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show("Debes de selecccionar la miniprinter la cual usaras para todas tus impresiones");
                    }
                }
                else
                {
                    MessageBox.Show("Debes de seleccionar la sucursal con la cual deseas trabajar");
                }
            }
            else
            {
                MessageBox.Show("Debes de seleccionar forzosamente una lista de mayoreo");
            }
        }
    }
}
