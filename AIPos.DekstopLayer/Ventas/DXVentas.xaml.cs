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


namespace AIPos.DekstopLayer.Ventas
{
    /// <summary>
    /// Interaction logic for DXVentas.xaml
    /// </summary>
    public partial class DXVentas : DXWindow
    {
        public System.IO.Ports.SerialPort PuertoSerieBascula;
        public delegate void DelegateSetCantidad(string peso);
        string peso = "0";
        private decimal descuento = 0;

        public DXVentas()
        {
            InitializeComponent();
            RecuperarDatos();

        }

        private void RecuperarDatos()
        {
            ServiceCliente.SClienteClient sClienteClient = new ServiceCliente.SClienteClient();
            cmbClientes.ItemsSource = sClienteClient.SelectAll();
            Cliente cliente = sClienteClient.SelectByCodigo("0");
            cmbClientes.SelectedItem = cliente;
            ServiceProducto.SProductoClient sProductoClient = new ServiceProducto.SProductoClient();
            cmbProductos.ItemsSource = sProductoClient.SelectAllProductos();
            List<VentaDetalle> inicio = new List<VentaDetalle>();
            gridVenta.ItemsSource = inicio;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Ventas.DXBuscadorCliente buscador = new DXBuscadorCliente();
            buscador.ShowDialog();
            if (buscador.ClienteSeleccionado != null)
            {
                cmbClientes.SelectedItem = buscador.ClienteSeleccionado;
                txtCodigoCliente.Text = buscador.ClienteSeleccionado.Codigo;
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
                txtCodigoCliente.Text = cliente.Codigo;
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

                if (cmbClientes.SelectedItem != null)
                {
                    Cliente cliente=(Cliente)cmbClientes.SelectedItem;
                    listaPrecioProducto = isListaPrecioProductoClient.SelectByProductoCliente(cliente.Id, producto.Id);
                }

                if (listaPrecioProducto == null)
                {
                    listaPrecioProducto = isListaPrecioProductoClient.SelectByProductoSucursal(General.ConfiguracionApp.SucursalId, producto.Id);
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
                        if (General.ConfiguracionApp.PuertoBascula != "")
                        {
                            InicializaPuertoBascula(General.ConfiguracionApp.PuertoBascula, 19200);
                            RecuperarPesoBascula();
                            Thread.Sleep(5000);
                            txtCantidad.Text = peso;
                            PuertoSerieBascula.Close();
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
                PuertoSerieBascula.Write("P");
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
                Cliente cliente = new ServiceCliente.SClienteClient().SelectByCodigo(txtCodigoCliente.Text);
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
        }

        private void btnCancelar_Click_1(object sender, RoutedEventArgs e)
        {
            ReiniciarVenta();
        }

        void ReiniciarVenta()
        {
            gridVenta.ItemsSource = new List<VentaDetalle>();
            gridVenta.RefreshData();
            LimpiarAgregarProducto();
            CalcularTotal();
            cmbClientes.SelectedIndex = 0;
            chkPrecioMayoreo.IsChecked = false;
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
                                    venta.RequiereFactura = false;
                                    venta.SucursalId = General.ConfiguracionApp.SucursalId;
                                    ServiceSucursal.SSucursalClient sucursalClient = new ServiceSucursal.SSucursalClient();
                                    Sucursal sucursal = sucursalClient.SelectById(venta.SucursalId);
                                    venta.Sucursal = new Sucursal();
                                    venta.Sucursal.Id = sucursal.Id;
                                    venta.Sucursal.Nombre = sucursal.Nombre;
                                    venta.Sucursal.DireccionId = sucursal.DireccionId;
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
            report.PrintToPrinter(2, false, 1, 1);
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
            report.PrintToPrinter(2, false, 1, 1);
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
                GenerarVenta(null,true);
            }
        }

        private void btnApartar_Click(object sender, RoutedEventArgs e)
        {
            decimal recibio = 0;
            if (!decimal.TryParse(txtRecibi.Text, out recibio))
            {
                recibio = 0;
            }
            RegistroApartadoServicio registroApartadoServicio = new RegistroApartadoServicio();
            registroApartadoServicio.txtAnticipo.Text = recibio.ToString();
            registroApartadoServicio.ClienteId = ((Cliente)cmbClientes.SelectedItem).Id;
            registroApartadoServicio.ShowDialog();
            if (registroApartadoServicio.ServicioApartadoVenta != null)
            {
                //Se hacer persistente la venta y el servicio o apartado
                GenerarVenta(registroApartadoServicio.ServicioApartadoVenta, true);
            }
        }

        private void btnVenta_Click(object sender, RoutedEventArgs e)
        {
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
                GenerarVenta(registroVentaDomicilio.ServicioApartadoVenta, true);
            }
        }

        private void txtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCantidad.SelectionStart = 0;
            txtCantidad.SelectionLength = txtCantidad.Text.Length;
        }


        public void InicializaPuertoBascula(string puerto, int baud)
        {
            if (puerto != "" && puerto != string.Empty)
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
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al abrir el dispositivo (Bascula) ...\r" + error.Message);

                    }
                }
                else
                    MessageBox.Show("El puerto está abierto...");

            }
        }
        
        void PuertoSerieBascula_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            try
            {
                peso = PuertoSerieBascula.ReadExisting().Replace("Info:","").Replace("LSQ N/S:K121931","").Replace("kg","").Replace("N/S:K121931","").Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " " + ex.Source+" info: "+peso,"Data received");
            }
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





    }
}
