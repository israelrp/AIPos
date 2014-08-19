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
using System.IO.Ports;
using System.Threading;
using System.Windows.Threading;
using System.ServiceModel;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;


namespace AIPos.DekstopLayer.Ventas
{
    /// <summary>
    /// Interaction logic for DXVentas.xaml
    /// </summary>
    public partial class DXVentas : DXWindow
    {
        public System.IO.Ports.SerialPort PuertoSerieBascula=new SerialPort();
        private delegate void DelegateSetCantidad(string peso);
        private decimal descuento = 0;
        private Venta ventaActual = null;

        public DXVentas()
        {
            InitializeComponent();
            RecuperarDatos();

        }

        private void RecuperarDatos()
        {
            try
            {
                ServiceCliente.SClienteClient sClienteClient = new ServiceCliente.SClienteClient();
                cmbClientes.ItemsSource = sClienteClient.SelectAll();
                Cliente cliente = sClienteClient.SelectByCodigo(0);
                cmbClientes.SelectedItem = cliente;
                ServiceProducto.SProductoClient sProductoClient = new ServiceProducto.SProductoClient();
                cmbProductos.ItemsSource = sProductoClient.SelectAllProductos();
                List<VentaDetalle> inicio = new List<VentaDetalle>();
                gridVenta.ItemsSource = inicio;
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Ha ocurrido un problema con la conexión al servicio de principal del sistema. Detalles: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Ventas.DXBuscadorCliente buscador = new DXBuscadorCliente();
            buscador.ShowDialog();
            if (buscador.ClienteSeleccionado != null)
            {
                cmbClientes.SelectedItem = buscador.ClienteSeleccionado;
                txtCodigoCliente.Text = buscador.ClienteSeleccionado.Codigo.ToString();
            }
        }

        private void btnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            Ventas.DXBuscadoProductosxaml buscador = new DXBuscadoProductosxaml();
            buscador.ShowDialog();
            if (buscador.ProductoSeleccionado != null)
            {
                cmbProductos.SelectedItem = buscador.ProductoSeleccionado;
                txtCodigoProducto.Text = buscador.ProductoSeleccionado.Codigo;
            }
        }

        private void cmbClientes_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbClientes.SelectedIndex >= 0)
            {
                Cliente cliente = (Cliente)cmbClientes.SelectedItem;
                txtCodigoCliente.Text = cliente.Codigo.ToString();
                ReiniciarVenta();
                txtCodigoProducto.Focus();
            }
        }

        private void cmbProductos_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (cmbProductos.SelectedIndex >= 0)
            {
                Producto producto = (Producto)cmbProductos.SelectedItem;
                txtCodigoProducto.Text = producto.Codigo;
                txtPrecioUnitario.Text = producto.Precio.ToString();
                txtDescuento.Text = "0";
                descuento = 0;
                ServiceListaPrecioProducto.ISListaPrecioProductoClient isListaPrecioProductoClient = new ServiceListaPrecioProducto.ISListaPrecioProductoClient();
                ListaPrecioProducto listaPrecioProducto=null;

                try
                {
                    if (cmbClientes.SelectedItem != null)
                    {
                        Cliente cliente = (Cliente)cmbClientes.SelectedItem;
                        listaPrecioProducto = isListaPrecioProductoClient.SelectByProductoCliente(cliente.Id, producto.Id);
                    }

                    if (listaPrecioProducto == null)
                    {
                        listaPrecioProducto = isListaPrecioProductoClient.SelectByProductoSucursal(General.ConfiguracionApp.SucursalId, producto.Id);
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
                
                if (listaPrecioProducto != null)
                {
                    txtPrecioUnitario.Text = listaPrecioProducto.Precio.ToString();
                    txtDescuento.Text = CalcularDescuentoEnPesos(listaPrecioProducto.Precio,listaPrecioProducto.Descuento).ToString("c");
                    descuento=listaPrecioProducto.Descuento;
                }

                if (General.ConfiguracionApp.PuertoBascula != "")
                {
                    if (producto.SePesa)
                    {
                        if (InicializaPuertoBascula(General.ConfiguracionApp.PuertoBascula, 9600))
                        {
                            RecuperarPesoBascula();
                        }                        
                    }
                }
                txtCantidad.Focus();
            }
        }

        private void RecuperarPesoBascula()
        {
            try
            {
                //PuertoSerieBascula.Write("P");
                byte[] inBuffer = new byte[] { 80 };
                PuertoSerieBascula.Write(inBuffer, 0, inBuffer.Length);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error: El puerto " + PuertoSerieBascula.PortName + " no se encuentra abierto.");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Error: La respuesta fue null");
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("La operación no se completo en el tiempo establecido.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " " + ex.Source,"Recuperar peso bascula");
            }
        }

        private void txtCodigoCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && txtCodigoCliente.Text.Trim() != "")
            {
                Cliente cliente = new ServiceCliente.SClienteClient().SelectByCodigo(int.Parse(txtCodigoCliente.Text));
                if (cliente != null)
                {
                    cmbClientes.SelectedItem = cliente;
                    txtCodigoProducto.Focus();
                }
                else
                {
                    MessageBox.Show("El código de cliente no existe");
                }
            }
        }

        private void txtCodigoProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && txtCodigoProducto.Text.Trim() != "")
            {
                Producto producto = new ServiceProducto.SProductoClient().SelectByCodigo(txtCodigoProducto.Text);
                if (producto != null)
                {
                    cmbProductos.SelectedItem = producto;
                }
                else
                {
                    MessageBox.Show("El código de producto no existe");
                }
            }
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            decimal cantidad = 0;
            if (e.Key == Key.Enter && decimal.TryParse(txtCantidad.Text,out cantidad))
            {
                if (cmbProductos.SelectedIndex >= 0)
                {
                    Producto producto = (Producto)cmbProductos.SelectedItem;
                    VentaDetalle ventaDetalle = new VentaDetalle();
                    ventaDetalle.Id = 0;
                    ventaDetalle.VentaId = 0;
                    ventaDetalle.ProductoId = producto.Id;
                    ventaDetalle.Cantidad = cantidad;
                    ventaDetalle.PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text);
                    ventaDetalle.Codigo = producto.Codigo;
                    ventaDetalle.Nombre = producto.Nombre;
                    ventaDetalle.Fecha = DateTime.Now;
                    ventaDetalle.Descuento = descuento;
                    ventaDetalle.Importe =Math.Round( ventaDetalle.Cantidad * ventaDetalle.PrecioUnitario,0);
                    
                    if (ventaDetalle.Descuento > 0)
                    {
                        ventaDetalle.Importe = Math.Round(ventaDetalle.Importe - ((ventaDetalle.Descuento / 100) * ventaDetalle.Importe), 0);
                    }
                    List<VentaDetalle> ventasDetalle = (List<VentaDetalle>)gridVenta.ItemsSource;
                    ventasDetalle.Add(ventaDetalle);
                    gridVenta.ItemsSource = ventasDetalle;
                    gridVenta.RefreshData();
                    LimpiarAgregarProducto();
                    CalcularTotal();
                    txtRecibi.Focus();
                    if (producto.SePesa)
                    {
                        try
                        {
                            Unidad unidad = new ServiceUnidad.SUnidadClient().SelectById(producto.UnidadId);
                            Documentos.TicketPesaje ticketPesaje = new Documentos.TicketPesaje();
                            ticketPesaje.SetParameterValue("Producto", producto.Nombre);
                            ticketPesaje.SetParameterValue("PrecioUnitario", ventaDetalle.PrecioUnitario.ToString("c"));
                            decimal precioDescuento = Math.Round(ventaDetalle.PrecioUnitario - (ventaDetalle.PrecioUnitario * (ventaDetalle.Descuento / 100)), 0);
                            ticketPesaje.SetParameterValue("PrecioDescuento", precioDescuento.ToString("c"));
                            ticketPesaje.SetParameterValue("Cantidad", ventaDetalle.Cantidad.ToString() + " " + unidad.Nombre);
                            ticketPesaje.SetParameterValue("Importe", ventaDetalle.Importe.ToString("c"));
                            ticketPesaje.SetParameterValue("Usuario", General.UsuarioLogueado.Nombre + " " + General.UsuarioLogueado.Paterno + " " + General.UsuarioLogueado.Materno);
                            //----------------------------------------------------------------------
                            CrystalDecisions.Shared.PrintLayoutSettings PrintLayout = new CrystalDecisions.Shared.PrintLayoutSettings();
                            PrintLayout.Scaling = CrystalDecisions.Shared.PrintLayoutSettings.PrintScaling.Scale;
                            System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
                            printerSettings.PrinterName = General.ConfiguracionApp.MiniPrinter;
                            printerSettings.Copies = 2;
                            var pageSettings = new System.Drawing.Printing.PageSettings(printerSettings);
                            //pageSettings.PaperSize = new System.Drawing.Printing.PaperSize("CUSTOM", 1000, 3362);
                            //report.PrintOptions.PrinterName = General.ConfiguracionApp.MiniPrinter;
                            //report.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                            if (!General.ConfiguracionApp.ImpresoraVirtual)
                                ticketPesaje.PrintToPrinter(printerSettings, pageSettings, false, PrintLayout);
                            else
                            {
                                ReportViewer reportViewer = new ReportViewer();
                                reportViewer.Show();
                                ReportDocument reportDocument = (ReportDocument)ticketPesaje;
                                reportViewer.viewer.ViewerCore.ReportSource = reportDocument;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message+" - " + ex.StackTrace +" - " + ex.Source );
                        }
                        //----------------------------------------------------------------------
                    }
                }
            }
        }

        decimal CalcularDescuentoEnPesos(decimal precio, decimal descuento)
        {
            decimal precioConDescuento = 0;
            precioConDescuento = Math.Round(((descuento / 100) * precio), 0);
            return precioConDescuento;
        }

        void LimpiarAgregarProducto()
        {
            txtDescuento.Text = "";
            descuento = 0;
            txtPrecioUnitario.Text = "";
            txtCantidad.Text = "";
            txtCodigoProducto.Text = "";
            cmbProductos.SelectedIndex = -1;
            txtRecibi.Text = "";
        }

        void CalcularTotal()
        {
            List<VentaDetalle> ventasDetalle = (List<VentaDetalle>)gridVenta.ItemsSource;
            if (ventasDetalle != null)
            {
                lblTotal.Content = ventasDetalle.Sum(x => x.Importe).ToString("c");
            }
            else
            {
                lblTotal.Content = "$0.00";
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (gridVenta.SelectedItem != null)
            {
                VentaDetalle ventaDetalle = (VentaDetalle)gridVenta.SelectedItem;
                List<VentaDetalle> ventasDetalle = (List<VentaDetalle>)gridVenta.ItemsSource;
                ventasDetalle.Remove(ventaDetalle);
                gridVenta.ItemsSource = ventasDetalle;
                gridVenta.RefreshData();
                CalcularTotal();
            }
            else
            {
                MessageBox.Show("No ha seleccionado ninguna fila de la venta para eliminarla.");
            }
        }

        private void btnCancelar_Click_1(object sender, RoutedEventArgs e)
        {
            if (gridVenta.VisibleRowCount > 0)
                ReiniciarVenta();
            else
                MessageBox.Show("No hay ninguna venta que cancelar.");
        }

        void ReiniciarVenta()
        {
            gridVenta.ItemsSource = new List<VentaDetalle>();
            gridVenta.RefreshData();
            LimpiarAgregarProducto();
            CalcularTotal();
            cmbClientes.SelectedIndex = 0;
            chkPrecioMayoreo.IsChecked = false;
            chkFacturar.IsChecked = false;
        }

        private void CerrarVenta()
        {
            decimal cambio = 0;
            decimal recibio = 0;
            decimal total = 0;
            try
            {
                if (cmbClientes.SelectedItem != null)
                {
                    if (gridVenta.VisibleRowCount > 0)
                    {
                        Venta venta = new Venta();
                        venta.Cambio = cambio;
                        venta.Cancelado = false;
                        venta.ClienteId = ((Cliente)cmbClientes.SelectedItem).Id;
                        venta.Cliente = (Cliente)cmbClientes.SelectedItem;
                        venta.Fecha = DateTime.Now;
                        venta.Folio = 0;
                        venta.FolioCancelado = 0;
                        venta.Id = 0;
                        venta.Recibio = recibio;
                        venta.Facturado = false;
                        venta.Estatus = 0;
                        venta.SucursalId = General.ConfiguracionApp.SucursalId;
                        ServiceSucursal.SSucursalClient sucursalClient = new ServiceSucursal.SSucursalClient();
                        Sucursal sucursal = sucursalClient.SelectById(venta.SucursalId);
                        venta.Sucursal = new Sucursal();
                        venta.Sucursal.Id = sucursal.Id;
                        venta.Sucursal.Nombre = sucursal.Nombre;
                        venta.Sucursal.DireccionId = sucursal.DireccionId;
                        venta.Sucursal.FraseTicket = sucursal.FraseTicket;
                        venta.Total = total;
                        venta.UsuarioId = General.UsuarioLogueado.Id;
                        venta.Usuario = new Usuario();
                        venta.Usuario.Nombre = General.UsuarioLogueado.Nombre;
                        venta.Usuario.Id = General.UsuarioLogueado.Id;
                        venta.Usuario.Paterno = General.UsuarioLogueado.Paterno;
                        venta.Usuario.Materno = General.UsuarioLogueado.Materno;
                        venta.VentasDetalle = (List<VentaDetalle>)gridVenta.ItemsSource;
                        ServiceVenta.ISVentaClient ventaClient = new ServiceVenta.ISVentaClient();
                        Venta ventaInsertada = ventaClient.Insert(venta);
                        //ImprimirTicket(venta);
                        MessageBox.Show("¡EL ID DE LA VENTA FUE EL " + ventaInsertada.Id.ToString() + "!");
                        ReiniciarVenta();
                    }

                    else
                    {
                        MessageBox.Show("Debes de agregar produtos a la venta");
                    }
                }
                else
                {
                    MessageBox.Show("Debes de seleccionar un cliente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " " + ex.Source);
            }
        }

        private void ActualizarVenta(ServicioApartado ServicioApartadoVenta, bool EsOrden)
        {
            decimal cambio = 0;
            decimal recibio = 0;
            decimal total = 0;
            try
            {
                if (cmbClientes.SelectedItem != null)
                {
                    if (gridVenta.VisibleRowCount > 0)
                    {
                        if (decimal.TryParse(lblCambio.Content.ToString().Replace("$", "").Replace(",", ""), out cambio))
                        {
                            if (decimal.TryParse(txtRecibi.Text.Replace("$", "").Replace(",", ""), out recibio))
                            {
                                if (decimal.TryParse(lblTotal.Content.ToString().Replace("$", "").Replace(",", ""), out total))
                                {

                                    ventaActual.Cambio = cambio;
                                    ventaActual.Cancelado = false;
                                    ventaActual.ClienteId = ((Cliente)cmbClientes.SelectedItem).Id;
                                    ventaActual.Cliente = (Cliente)cmbClientes.SelectedItem;
                                    ventaActual.Fecha = DateTime.Now;
                                    ventaActual.Folio = 0;
                                    ventaActual.FolioCancelado = 0;
                                    ventaActual.Recibio = recibio;
                                    ventaActual.Facturado = false;
                                    ventaActual.Estatus = 1;
                                    ventaActual.SucursalId = General.ConfiguracionApp.SucursalId;
                                    ServiceSucursal.SSucursalClient sucursalClient = new ServiceSucursal.SSucursalClient();
                                    Sucursal sucursal = sucursalClient.SelectById(ventaActual.SucursalId);
                                    ventaActual.Sucursal = new Sucursal();
                                    ventaActual.Sucursal.Id = sucursal.Id;
                                    ventaActual.Sucursal.Nombre = sucursal.Nombre;
                                    ventaActual.Sucursal.DireccionId = sucursal.DireccionId;
                                    ventaActual.Sucursal.FraseTicket = sucursal.FraseTicket;
                                    ventaActual.Total = total;
                                    ventaActual.UsuarioId = General.UsuarioLogueado.Id;
                                    ventaActual.Usuario = new Usuario();
                                    ventaActual.Usuario.Nombre = General.UsuarioLogueado.Nombre;
                                    ventaActual.Usuario.Id = General.UsuarioLogueado.Id;
                                    ventaActual.Usuario.Paterno = General.UsuarioLogueado.Paterno;
                                    ventaActual.Usuario.Materno = General.UsuarioLogueado.Materno;
                                    ventaActual.VentasDetalle = (List<VentaDetalle>)gridVenta.ItemsSource;
                                    ServiceVenta.ISVentaClient ventaClient = new ServiceVenta.ISVentaClient();
                                    if (EsOrden)
                                        ventaActual.FolioCancelado = ventaClient.GenerarFolioCancelado(ventaActual.SucursalId, DateTime.Now.ToFileTimeUtc());
                                    else
                                        ventaActual.Folio = ventaClient.GenerarFolioVenta(ventaActual.SucursalId);
                                    if (ServicioApartadoVenta != null)
                                    {
                                        switch (ServicioApartadoVenta.Tipo)
                                        {
                                            case 0:
                                                ventaActual.FolioCancelado = ventaClient.GenerarFolioCanceladoDomicilio(ventaActual.SucursalId, DateTime.Now.ToFileTimeUtc());
                                                break;
                                            case 1:
                                                ventaActual.FolioCancelado = ventaClient.GenerarFolioCanceladoApartado(ventaActual.SucursalId, DateTime.Now.ToFileTimeUtc());
                                                break;
                                            case 2:
                                                ventaActual.FolioCancelado = ventaClient.GenerarFolioCanceladoServicio(ventaActual.SucursalId, DateTime.Now.ToFileTimeUtc());
                                                break;
                                        }
                                    }
                                    if (chkFacturar.IsChecked.HasValue)
                                        ventaActual.RequiereFactura = chkFacturar.IsChecked.Value;
                                    else
                                        ventaActual.RequiereFactura = false;
                                    Venta ventaInsertada = ventaClient.Update(ventaActual);
                                    if (ServicioApartadoVenta != null)
                                    {
                                        ServicioApartadoVenta.VentaId = ventaInsertada.Id;
                                        ServiceServicioApartado.SServicioApartadoClient servicioApartadoClient = new ServiceServicioApartado.SServicioApartadoClient();
                                        ServicioApartadoVenta = servicioApartadoClient.Insert(ServicioApartadoVenta);
                                        ServicioApartadoVenta.DireccionEnvio = new ServiceDireccion.SDireccionClient().SelectById(ServicioApartadoVenta.DireccionEnvioId);
                                        ImprimirApartadosServicio(ventaActual, ServicioApartadoVenta);
                                    }
                                    else
                                    {
                                        ImprimirTicket(ventaActual);
                                    }
                                    MessageBox.Show("¡GRACIAS POR SU COMPRA!");
                                    ventaActual = null;

                                    ReiniciarVenta();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe escribir con números la cantidad recibida de dinero");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debes de agregar produtos a la venta");
                    }
                }
                else
                {
                    MessageBox.Show("Debes de seleccionar un cliente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " " + ex.Source);
            }
        }


        private void GenerarVenta(ServicioApartado ServicioApartadoVenta, bool EsOrden)
        {
            decimal cambio = 0;
            decimal recibio = 0;
            decimal total = 0;
            try
            {
                if (cmbClientes.SelectedItem != null)
                {
                    if (gridVenta.VisibleRowCount > 0)
                    {
                        if (decimal.TryParse(lblCambio.Content.ToString().Replace("$", "").Replace(",", ""), out cambio))
                        {
                            if (decimal.TryParse(txtRecibi.Text.Replace("$", "").Replace(",", ""), out recibio))
                            {
                                if (decimal.TryParse(lblTotal.Content.ToString().Replace("$", "").Replace(",", ""), out total))
                                {
                                    Venta venta = new Venta();
                                    venta.Cambio = cambio;
                                    venta.Cancelado = false;
                                    venta.ClienteId = ((Cliente)cmbClientes.SelectedItem).Id;
                                    venta.Cliente = (Cliente)cmbClientes.SelectedItem;
                                    venta.Fecha = DateTime.Now;
                                    venta.Folio = 0;
                                    venta.FolioCancelado = 0;
                                    venta.Id = 0;
                                    venta.Recibio = recibio;
                                    venta.Facturado = false;
                                    venta.Estatus = 1;
                                    venta.SucursalId = General.ConfiguracionApp.SucursalId;
                                    ServiceSucursal.SSucursalClient sucursalClient = new ServiceSucursal.SSucursalClient();
                                    Sucursal sucursal = sucursalClient.SelectById(venta.SucursalId);
                                    venta.Sucursal = new Sucursal();
                                    venta.Sucursal.Id = sucursal.Id;
                                    venta.Sucursal.Nombre = sucursal.Nombre;
                                    venta.Sucursal.DireccionId = sucursal.DireccionId;
                                    venta.Sucursal.FraseTicket = sucursal.FraseTicket;
                                    venta.Total = total;
                                    venta.UsuarioId = General.UsuarioLogueado.Id;
                                    venta.Usuario = new Usuario();
                                    venta.Usuario.Nombre = General.UsuarioLogueado.Nombre;
                                    venta.Usuario.Id = General.UsuarioLogueado.Id;
                                    venta.Usuario.Paterno = General.UsuarioLogueado.Paterno;
                                    venta.Usuario.Materno = General.UsuarioLogueado.Materno;
                                    venta.VentasDetalle = (List<VentaDetalle>)gridVenta.ItemsSource;
                                    ServiceVenta.ISVentaClient ventaClient = new ServiceVenta.ISVentaClient();
                                    if (EsOrden)
                                        venta.FolioCancelado = ventaClient.GenerarFolioCancelado(venta.SucursalId, DateTime.Now.ToFileTimeUtc());
                                    else
                                        venta.Folio = ventaClient.GenerarFolioVenta(venta.SucursalId);
                                    if (ServicioApartadoVenta != null)
                                    {
                                        switch (ServicioApartadoVenta.Tipo)
                                        {
                                            case 0:
                                                venta.FolioCancelado = ventaClient.GenerarFolioCanceladoDomicilio(venta.SucursalId, DateTime.Now.ToFileTimeUtc());
                                                break;
                                            case 1:
                                                venta.FolioCancelado = ventaClient.GenerarFolioCanceladoApartado(venta.SucursalId, DateTime.Now.ToFileTimeUtc());
                                                break;
                                            case 2:
                                                venta.FolioCancelado = ventaClient.GenerarFolioCanceladoServicio(venta.SucursalId, DateTime.Now.ToFileTimeUtc());
                                                break;
                                        }
                                    }
                                    if (chkFacturar.IsChecked.HasValue)
                                        venta.RequiereFactura = chkFacturar.IsChecked.Value;
                                    else
                                        venta.RequiereFactura = false;
                                    Venta ventaInsertada = ventaClient.Insert(venta);
                                    if (ServicioApartadoVenta != null)
                                    {
                                        ServicioApartadoVenta.VentaId = ventaInsertada.Id;
                                        ServiceServicioApartado.SServicioApartadoClient servicioApartadoClient = new ServiceServicioApartado.SServicioApartadoClient();
                                        ServicioApartadoVenta = servicioApartadoClient.Insert(ServicioApartadoVenta);
                                        ServicioApartadoVenta.DireccionEnvio = new ServiceDireccion.SDireccionClient().SelectById(ServicioApartadoVenta.DireccionEnvioId);
                                        ImprimirApartadosServicio(venta, ServicioApartadoVenta);
                                    }
                                    else
                                    {
                                        ImprimirTicket(venta);
                                    }
                                    MessageBox.Show("¡GRACIAS POR SU COMPRA!");
                                    ventaActual = null;

                                    ReiniciarVenta();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe escribir con números la cantidad recibida de dinero");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debes de agregar produtos a la venta");
                    }
                }
                else
                {
                    MessageBox.Show("Debes de seleccionar un cliente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " " + ex.Source);
            }
        }

        private void btnOrden_Click(object sender, RoutedEventArgs e)
        {
            if (ventaActual != null)
                ActualizarVenta(null, true);
            else
                GenerarVenta(null, true);
        }

        private void ImprimirTicket(Venta venta)
        {
            Documentos.Ticket report = new Documentos.Ticket();
            venta.Sucursal.Direccion = new ServiceDireccion.SDireccionClient().SelectById(venta.Sucursal.DireccionId);
            report.Database.Tables[0].SetDataSource(new List<Cliente>() { venta.Cliente });
            report.Database.Tables[1].SetDataSource(new List<Sucursal>() { venta.Sucursal });
            report.Database.Tables[2].SetDataSource(new List<Usuario>() { venta.Usuario });
            report.Database.Tables[3].SetDataSource(venta.VentasDetalle);
            report.Database.Tables[4].SetDataSource(new List<Venta>() {venta});
            report.Database.Tables[5].SetDataSource(new List<Direccion>() { venta.Sucursal.Direccion });
            report.PrintOptions.PrinterName = General.ConfiguracionApp.MiniPrinter;
            //----------------------------------------------------------------------
            CrystalDecisions.Shared.PrintLayoutSettings PrintLayout = new CrystalDecisions.Shared.PrintLayoutSettings();
            PrintLayout.Scaling = CrystalDecisions.Shared.PrintLayoutSettings.PrintScaling.Scale;
            System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
            printerSettings.PrinterName = General.ConfiguracionApp.MiniPrinter;
            printerSettings.Copies = 2;
            var pageSettings = new System.Drawing.Printing.PageSettings(printerSettings);
            //pageSettings.PaperSize = new System.Drawing.Printing.PaperSize("CUSTOM", 1000, 3362);
            //report.PrintOptions.PrinterName = General.ConfiguracionApp.MiniPrinter;
            //report.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
            if(!General.ConfiguracionApp.ImpresoraVirtual)
                report.PrintToPrinter(printerSettings, pageSettings, false, PrintLayout);
            else
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.Show();
                ReportDocument reportDocument=(ReportDocument)report;
                reportViewer.viewer.ViewerCore.ReportSource = reportDocument;
            }
            //----------------------------------------------------------------------
        }

        private void ImprimirApartadosServicio(Venta venta, ServicioApartado servicioApartado)
        {
            Documentos.TicketServicioApartado report = new Documentos.TicketServicioApartado();
            venta.Sucursal.Direccion = new ServiceDireccion.SDireccionClient().SelectById(venta.Sucursal.DireccionId);
            string direccionSucursal=venta.Sucursal.Direccion.Calle+" "+venta.Sucursal.Direccion.NoExterior+" "+
                venta.Sucursal.Direccion.NoInterior+" "+venta.Sucursal.Direccion.Colonia+" "+
                venta.Sucursal.Direccion.Ciudad+" "+venta.Sucursal.Direccion.CodigoPostal;
            report.Database.Tables[0].SetDataSource(new List<Cliente>() { venta.Cliente });
            report.Database.Tables[1].SetDataSource(new List<Direccion> { servicioApartado.DireccionEnvio });
            report.Database.Tables[2].SetDataSource(new List<ServicioApartado> { servicioApartado });
            report.Database.Tables[3].SetDataSource(new List<Sucursal>() { venta.Sucursal });
            report.Database.Tables[4].SetDataSource(new List<Usuario>() { venta.Usuario });
            report.Database.Tables[5].SetDataSource(venta.VentasDetalle);
            report.Database.Tables[6].SetDataSource(new List<Venta>() { venta });
            report.PrintOptions.PrinterName = General.ConfiguracionApp.MiniPrinter;
            report.SetParameterValue(0, direccionSucursal);
            //----------------------------------------------------------------------
            CrystalDecisions.Shared.PrintLayoutSettings PrintLayout = new CrystalDecisions.Shared.PrintLayoutSettings();
            PrintLayout.Scaling = CrystalDecisions.Shared.PrintLayoutSettings.PrintScaling.Scale;
            System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
            printerSettings.PrinterName = General.ConfiguracionApp.MiniPrinter;
            printerSettings.Copies = 2;
            var pageSettings = new System.Drawing.Printing.PageSettings(printerSettings);
            //pageSettings.PaperSize = new System.Drawing.Printing.PaperSize("CUSTOM", 1000, 3362);
            //report.PrintOptions.PrinterName = General.ConfiguracionApp.MiniPrinter;
            //report.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
            if (!General.ConfiguracionApp.ImpresoraVirtual)
                report.PrintToPrinter(printerSettings, pageSettings, false, PrintLayout);
            else
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.Show();
                ReportDocument reportDocument = (ReportDocument)report;
                reportViewer.viewer.ViewerCore.ReportSource = reportDocument;
            }
            //----------------------------------------------------------------------
        }

        private void txtRecibi_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal  total = 0;
            decimal recibi = 0;
            if (decimal.TryParse(lblTotal.Content.ToString().Replace("$","").Replace(",",""), out total) && decimal.TryParse(txtRecibi.Text, out recibi))
            {
                lblCambio.Content = (recibi - total).ToString("c");
            }
            else
            {
                lblCambio.Content = "CAMBIO";
            }
        }

        private void gridVenta_CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                decimal precioUnitario = Convert.ToDecimal(e.GetListSourceFieldValue("PrecioUnitario"));
                decimal descuetoPorcentaje= Convert.ToDecimal(e.GetListSourceFieldValue("Descuento"))/100;
                e.Value = precioUnitario - (precioUnitario * descuetoPorcentaje);
            }
        }

        private void txtRecibi_GotFocus(object sender, RoutedEventArgs e)
        {
            txtRecibi.Text = "";
        }

        private void txtRecibi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ventaActual != null)
                    ActualizarVenta(null, true);
                else
                    GenerarVenta(null,true);
            }
        }

        private void btnApartar_Click(object sender, RoutedEventArgs e)
        {
            if (gridVenta.VisibleRowCount > 0)
            {
                decimal recibio = 0;
                if (!decimal.TryParse(txtRecibi.Text, out recibio))
                {
                    recibio = 0;
                }
                List<VentaDetalle> ventasDetalle = (List<VentaDetalle>)gridVenta.ItemsSource;
                RegistroApartadoServicio registroApartadoServicio = new RegistroApartadoServicio();
                registroApartadoServicio.ClienteId = ((Cliente)cmbClientes.SelectedItem).Id;
                registroApartadoServicio.TotalVenta = ventasDetalle.Sum(x => x.Importe);
                if (recibio <= registroApartadoServicio.TotalVenta)
                {
                    registroApartadoServicio.txtAnticipo.Text = recibio.ToString();
                }
                else
                {
                    registroApartadoServicio.txtAnticipo.Text = registroApartadoServicio.TotalVenta.ToString();
                }
                registroApartadoServicio.ShowDialog();
                if (registroApartadoServicio.ServicioApartadoVenta != null)
                {
                    //Se hacer persistente la venta y el servicio o apartado
                    if (ventaActual != null)
                        ActualizarVenta(registroApartadoServicio.ServicioApartadoVenta, true);
                    else
                        GenerarVenta(registroApartadoServicio.ServicioApartadoVenta, true);
                }
            }
            else
            {
                MessageBox.Show("Debes agregar productos a la venta.");
            }
        }

        private void btnVenta_Click(object sender, RoutedEventArgs e)
        {
            if (ventaActual != null)
                ActualizarVenta(null, false);
            else
                GenerarVenta(null, false);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            RegistrarCliente registrarCliente = new RegistrarCliente();
            registrarCliente.ShowDialog();
            if (registrarCliente.ClienteNuevo != null)
            {
                ServiceCliente.SClienteClient sClienteClient = new ServiceCliente.SClienteClient();
                cmbClientes.ItemsSource = sClienteClient.SelectAll();
                cmbClientes.SelectedItem = registrarCliente.ClienteNuevo;
            }
        }

        private void btnDomicilio_Click(object sender, RoutedEventArgs e)
        {
            if (gridVenta.VisibleRowCount > 0)
            {
                decimal recibio = 0;
                if (!decimal.TryParse(txtRecibi.Text, out recibio))
                {
                    recibio = 0;
                    txtRecibi.Text = "0";
                }
                Ventas.ResgistroVentaDomicilio registroVentaDomicilio = new ResgistroVentaDomicilio();
                registroVentaDomicilio.ClienteId = ((Cliente)cmbClientes.SelectedItem).Id;
                registroVentaDomicilio.ShowDialog();
                if (registroVentaDomicilio.ServicioApartadoVenta != null)
                {
                    //Se guarda la venta y el domicilio de envio
                    registroVentaDomicilio.ServicioApartadoVenta.Anticipo = recibio;
                    if (ventaActual != null)
                        ActualizarVenta(registroVentaDomicilio.ServicioApartadoVenta, true);
                    else
                        GenerarVenta(registroVentaDomicilio.ServicioApartadoVenta, true);
                }
            }
            else
            {
                MessageBox.Show("Debes de agregar productos a la venta.");
            }
        }

        private void txtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCantidad.SelectionStart = 0;
            txtCantidad.SelectionLength = txtCantidad.Text.Length;
        }


        public bool InicializaPuertoBascula(string puerto, int baud)
        {
            bool retorno=false;
            if (puerto != "" && puerto != string.Empty && !PuertoSerieBascula.IsOpen)
            {
                PuertoSerieBascula = new SerialPort(puerto, baud);
                // ComPort.PortName = port; 
                // ComPort.BaudRate = baud;
                //PuertoSerie.PortName = "COM23";
                if (!PuertoSerieBascula.IsOpen)
                {
                    PuertoSerieBascula.Parity = Parity.None;
                    PuertoSerieBascula.StopBits = StopBits.One;
                    PuertoSerieBascula.DataBits = 8;
                    PuertoSerieBascula.Handshake = Handshake.None;
                    PuertoSerieBascula.ReadTimeout = 4800;

                    PuertoSerieBascula.DataReceived += PuertoSerieBascula_DataReceived;
                    PuertoSerieBascula.ErrorReceived += PuertoSerieBascula_ErrorReceived;

                    try
                    {

                        PuertoSerieBascula.Open();
                        retorno = true;
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show("Acceso denegado al puerto "+puerto+". Detalle: "+ex.Message);
                    }
                    catch(ArgumentOutOfRangeException ex){
                        MessageBox.Show("Una configuración para conectarse a la báscula esta incorrecta, por favor verifiquela. Detalles: " + ex.Message);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show("El nombre del puesto no empieza con \"COM\" o el tipo de puerto no esta soportado.");
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("El puerto se encuentra en un estado inválido. Detalles: " + ex.Message);
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show("El puerto especificado ya se encuentra abierto");
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al abrir el dispositivo (Bascula) ...\r" + error.Message);
                    }
                }
                else
                    MessageBox.Show("El puerto está abierto...");

            }
            return retorno;
        }
        
        void PuertoSerieBascula_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string peso = PuertoSerieBascula.ReadExisting();
                
                this.txtCantidad.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new DelegateSetCantidad(this.ponteTextoBascula), peso);
                PuertoSerieBascula.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " " + ex.Source+" info: "+ ex.InnerException,"Data received");
            }
        }

        private void ponteTextoBascula(string peso)
        {

            txtCantidad.Text = peso.Replace("F", "").Replace("kg","") + Convert.ToChar(13);
        }



        void PuertoSerieBascula_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

            switch (e.EventType)
            {
                case SerialError.Frame:
                    MessageBox.Show("Error de trama...");
                    break;
                case SerialError.Overrun:
                    MessageBox.Show("Saturación de buffer...");
                    break;
                case SerialError.RXOver:
                    MessageBox.Show("Desbordamiento de buffer de entrada");
                    break;
                case SerialError.RXParity:
                    MessageBox.Show("Error de paridad...");
                    break;
                case SerialError.TXFull:
                    MessageBox.Show("Buffer lleno...");
                    break;

            }
            throw new NotImplementedException();
        }

        private void chkPrecioMayoreo_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (chkPrecioMayoreo.IsChecked.Value)
            {
                if (cmbProductos.SelectedIndex >= 0)
                {
                    Producto producto = (Producto)cmbProductos.SelectedItem;
                    txtPrecioUnitario.Text = producto.Precio.ToString();
                    txtDescuento.Text = "0";
                    descuento = 0;
                    ServiceListaPrecioProducto.ISListaPrecioProductoClient isListaPrecioProductoClient = new ServiceListaPrecioProducto.ISListaPrecioProductoClient();
                    ListaPrecioProducto listaPrecioProducto = null;
                    listaPrecioProducto = isListaPrecioProductoClient.SelectByProductoLista(General.ConfiguracionApp.ListaPrecioMayoreoId, producto.Id);
                    if (listaPrecioProducto != null)
                    {
                        txtPrecioUnitario.Text = listaPrecioProducto.Precio.ToString();
                        txtDescuento.Text = CalcularDescuentoEnPesos(listaPrecioProducto.Precio,listaPrecioProducto.Descuento).ToString("c");
                        descuento = listaPrecioProducto.Descuento;
                    }
                    else
                    {
                        MessageBox.Show("No existe un precio de mayoreo asignado a este producto, consulte al administrador del sistema");
                        chkPrecioMayoreo.IsChecked = false;
                    }
                }
            }
            else
            {
                if (cmbProductos.SelectedIndex >= 0)
                {
                    Producto producto = (Producto)cmbProductos.SelectedItem;
                    txtPrecioUnitario.Text = producto.Precio.ToString();
                    txtDescuento.Text = "0";
                    descuento = 0;
                    ServiceListaPrecioProducto.ISListaPrecioProductoClient isListaPrecioProductoClient = new ServiceListaPrecioProducto.ISListaPrecioProductoClient();
                    ListaPrecioProducto listaPrecioProducto = null;

                    if (cmbClientes.SelectedItem != null)
                    {
                        Cliente cliente = (Cliente)cmbClientes.SelectedItem;
                        listaPrecioProducto = isListaPrecioProductoClient.SelectByProductoCliente(cliente.Id, producto.Id);
                    }

                    if (listaPrecioProducto == null)
                    {
                        listaPrecioProducto = isListaPrecioProductoClient.SelectByProductoSucursal(General.ConfiguracionApp.SucursalId, producto.Id);
                    }

                    if (listaPrecioProducto != null)
                    {
                        txtPrecioUnitario.Text = listaPrecioProducto.Precio.ToString();
                        txtDescuento.Text = CalcularDescuentoEnPesos(listaPrecioProducto.Precio, listaPrecioProducto.Descuento).ToString("c");
                        descuento = listaPrecioProducto.Descuento;
                    }
                }
            }
        }

        private void txtCantidad_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }


        private void btnCerrarVenta_Click(object sender, RoutedEventArgs e)
        {
            CerrarVenta();
        }

        private void btnRecuperarVenta_Click(object sender, RoutedEventArgs e)
        {
            DXVentaId dxVentaId = new DXVentaId();
            dxVentaId.ShowDialog();
            if (dxVentaId.VentaRecuperada != null)
            {
                ventaActual = dxVentaId.VentaRecuperada;
                Cliente cliente = new ServiceCliente.SClienteClient().SelectById(ventaActual.ClienteId);
                cmbClientes.SelectedItem = cliente;
                gridVenta.ItemsSource = ventaActual.VentasDetalle;
                CalcularTotal();
            }

        }





    }
}
