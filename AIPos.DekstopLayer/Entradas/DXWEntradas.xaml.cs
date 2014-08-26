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


namespace AIPos.DekstopLayer.Entradas
{
    /// <summary>
    /// Interaction logic for DXWEntradas.xaml
    /// </summary>
    public partial class DXWEntradas : DXWindow
    {
        public DXWEntradas()
        {
            InitializeComponent();
            dateEditEntradas.DateTime = DateTime.Now.Date;
            dateEditEntradas.EditValue = DateTime.Now.Date;
            RecuperarInformacion();
        }

        private void RecuperarInformacion()
        {
            try
            {
                ServiceProveedor.SProveedorClient sProveedorClient = new ServiceProveedor.SProveedorClient();
                cmbProveedores.ItemsSource = sProveedorClient.SelectAll().ToList();
                ServiceProducto.SProductoClient sProductoClient = new ServiceProducto.SProductoClient();
                cmbProductos.ItemsSource = sProductoClient.SelectAllProductos().ToList();
                ServiceTipoProducto.STipoProductoClient sTipoProductoClient = new ServiceTipoProducto.STipoProductoClient();
                cmbTipoProducto.ItemsSource = sTipoProductoClient.SelectAll();
                ServiceEntrada.SEntradaClient sEntradaClient = new ServiceEntrada.SEntradaClient();
                gridEntradas.ItemsSource = sEntradaClient.SelectByDay(General.ConfiguracionApp.SucursalId, dateEditEntradas.DateTime.ToFileTimeUtc());
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

        private void cmbProveedores_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if(cmbProveedores.SelectedIndex>=0)
                txtCodigoProveedor.Text = ((Proveedor)cmbProveedores.SelectedItem).Codigo;
        }

        private void cmbProductos_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbProductos.SelectedIndex >= 0)
            {
                Producto producto = (Producto)cmbProductos.SelectedItem;
                txtCodigoProducto.Text = producto.Codigo;
                txtPrecioUnitario.Text = producto.Precio.ToString();
                try
                {
                    ServiceListaPrecioProducto.ISListaPrecioProductoClient isListaPrecioProductoClient = new ServiceListaPrecioProducto.ISListaPrecioProductoClient();
                    ListaPrecioProducto listaPrecioProducto = null;
                    if (listaPrecioProducto == null)
                    {
                        listaPrecioProducto = isListaPrecioProductoClient.SelectByProductoSucursal(General.ConfiguracionApp.SucursalId, producto.Id);
                        if (listaPrecioProducto != null)
                            txtPrecioUnitario.Text = listaPrecioProducto.Precio.ToString();

                    }
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
        }

        private void txtCodigoProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && txtCodigoProveedor.Text.Trim()!="")
            {
                try
                {
                    Proveedor proveedor = new ServiceProveedor.SProveedorClient().SelectByCodigo(txtCodigoProveedor.Text);
                    if (proveedor != null)
                    {
                        cmbProveedores.SelectedItem = proveedor;
                        txtCodigoProducto.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El código de proveedor no existe");
                    }
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
        }

        private void txtCodigoProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && txtCodigoProducto.Text.Trim()!="")
            {
                try
                {
                    Producto producto = new ServiceProducto.SProductoClient().SelectByCodigo(txtCodigoProducto.Text);
                    if (producto != null)
                    {
                        cmbProductos.SelectedItem = producto;
                        txtPrecioUnitario.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El código de producto no existe");
                    }
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
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarDatos())
            {
                if (ValidarUsuario())
                {
                    Entrada entrada = new Entrada();
                    entrada.ProductoId = ((Producto)cmbProductos.SelectedItem).Id;
                    entrada.ProveedorId = ((Proveedor)cmbProveedores.SelectedItem).Id;
                    entrada.SucursalId = General.ConfiguracionApp.SucursalId;
                    entrada.TipoProductoId = ((TipoProducto)cmbTipoProducto.SelectedItem).Id;
                    entrada.UsuarioId = General.UsuarioLogueado.Id;
                    entrada.TotalPiezas = 0;
                    entrada.PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text);
                    entrada.Fecha = DateTime.Now;
                    entrada.CantidadProveedor = decimal.Parse(txtCantidadDS.Text);
                    entrada.CantidadReal = decimal.Parse(txtCantidadPV.Text);
                    entrada.Importe = entrada.CantidadReal * entrada.PrecioUnitario;
                    entrada.Diferencia = entrada.CantidadProveedor - entrada.CantidadReal;
                    ServiceEntrada.SEntradaClient sEntradaClient = new ServiceEntrada.SEntradaClient();
                    try
                    {
                        sEntradaClient.Insert(entrada);
                        gridEntradas.ItemsSource = sEntradaClient.SelectByDay(General.ConfiguracionApp.SucursalId, dateEditEntradas.DateTime.ToFileTimeUtc());
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
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("El password introducido no es válido");
                }
            }
            else
            {
                MessageBox.Show("Debe de llenar todos los campos");
            }
        }

        bool ValidarUsuario()
        {
            bool retorno = false;
            DXLoginEntrada loginEntrada = new DXLoginEntrada();
            loginEntrada.ShowDialog();
            retorno = loginEntrada.UsuarioValido;
            return retorno;
        }

        bool ValidarDatos()
        {
            bool retorno = false;
            if (cmbProductos.SelectedIndex >= 0 && cmbProveedores.SelectedIndex>=0 && cmbTipoProducto.SelectedIndex>=0 && txtPrecioUnitario.Text.Length>0
                && txtCantidadPV.Text.Length>0 &&txtCantidadDS.Text.Length>0)
            {
                retorno = true;
            }
            return retorno;
        }

        void LimpiarControles()
        {
            cmbProductos.Clear();
            cmbProveedores.Clear();
            cmbTipoProducto.Clear();
            txtPrecioUnitario.Text = "";
            txtCantidadDS.Text = "";
            txtCantidadPV.Text = "";
            txtCodigoProducto.Text = "";
            txtCodigoProveedor.Text = "";
        }

        private void dateEditEntradas_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                ServiceEntrada.SEntradaClient sEntradaClient = new ServiceEntrada.SEntradaClient();
                gridEntradas.ItemsSource = sEntradaClient.SelectByDay(General.ConfiguracionApp.SucursalId, dateEditEntradas.DateTime.ToFileTimeUtc());
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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Entrada entrada = (Entrada)gridEntradas.SelectedItem;
            try
            {
                ServiceEntrada.SEntradaClient sEntradaClient = new ServiceEntrada.SEntradaClient();
                sEntradaClient.Delete(entrada.Id);
                gridEntradas.ItemsSource = sEntradaClient.SelectByDay(General.ConfiguracionApp.SucursalId, dateEditEntradas.DateTime.ToFileTimeUtc());
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

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            Documentos.Entradas report = new Documentos.Entradas();
            try
            {
                ServiceEntrada.SEntradaClient sEntradaClient = new ServiceEntrada.SEntradaClient();
                List<Entrada> entradas = sEntradaClient.SelectByDay(General.ConfiguracionApp.SucursalId, dateEditEntradas.DateTime.ToFileTimeUtc()).ToList();
                report.Database.Tables[0].SetDataSource(entradas);
                List<Producto> productos = new ServiceProducto.SProductoClient().SelectAllProductos().ToList();
                report.Database.Tables[1].SetDataSource(productos);
                List<TipoProducto> tipos = new ServiceTipoProducto.STipoProductoClient().SelectAll().ToList();
                report.Database.Tables[2].SetDataSource(tipos);
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
            report.PrintOptions.PrinterName = General.ConfiguracionApp.MiniPrinter;
            //----------------------------------------------------------------------
            CrystalDecisions.Shared.PrintLayoutSettings PrintLayout = new CrystalDecisions.Shared.PrintLayoutSettings();
            PrintLayout.Scaling = CrystalDecisions.Shared.PrintLayoutSettings.PrintScaling.Scale;
            System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
            printerSettings.PrinterName = General.ConfiguracionApp.MiniPrinter;
            printerSettings.Copies = 2;
            var pageSettings = new System.Drawing.Printing.PageSettings(printerSettings);
            pageSettings.PaperSize = new System.Drawing.Printing.PaperSize("CUSTOM", 1000, 3362);
            report.PrintOptions.PrinterName = General.ConfiguracionApp.MiniPrinter;
            report.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
            report.PrintToPrinter(printerSettings, pageSettings, false, PrintLayout);
            //----------------------------------------------------------------------

        }

        private void btnBuscarProducto_Click_1(object sender, RoutedEventArgs e)
        {
            Ventas.DXBuscadoProductosxaml buscador = new Ventas.DXBuscadoProductosxaml();
            buscador.ShowDialog();
            if (buscador.ProductoSeleccionado != null)
            {
                cmbProductos.SelectedItem = buscador.ProductoSeleccionado;
                txtCodigoProducto.Text = buscador.ProductoSeleccionado.Codigo;
            }
        }

        private void txtPrecioUnitario_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txtCantidadPV.Focus();
        }

        private void txtCantidadPV_KeyUp_1(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                txtCantidadDS.Focus();
        }

        private void txtCantidadDS_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                cmbTipoProducto.Focus();
        }

        private void cmbTipoProducto_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnGuardar.Focus();
        }

        private void txtPrecioUnitario_GotFocus_1(object sender, RoutedEventArgs e)
        {
            txtPrecioUnitario.SelectionStart = 0;
            txtPrecioUnitario.SelectionLength = txtPrecioUnitario.Text.Length;
        }

        private void txtCantidadPV_GotFocus_1(object sender, RoutedEventArgs e)
        {
            txtCantidadPV.SelectionStart = 0;
            txtCantidadPV.SelectionLength = txtCantidadPV.Text.Length;
        }

        private void txtCantidadDS_GotFocus_1(object sender, RoutedEventArgs e)
        {
            txtCantidadDS.SelectionStart = 0;
            txtCantidadDS.SelectionLength = txtCantidadDS.Text.Length;
        }


    }
}
