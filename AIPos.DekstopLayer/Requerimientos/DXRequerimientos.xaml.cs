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

namespace AIPos.DekstopLayer.Requerimientos
{
    /// <summary>
    /// Interaction logic for DXRequerimientos.xaml
    /// </summary>
    public partial class DXRequerimientos : DXWindow
    {
        public DXRequerimientos()
        {
            InitializeComponent();
            RecuperarInformacion();
            deFecha.DateTime = DateTime.Now.Date;
            deFecha.EditValue = DateTime.Now.Date;
            RecuperarDatos();
        }

        private void RecuperarDatos()
        {
            if (deFecha.DateTime != null)
            {
                try
                {
                    ServicePedidoSucursal.SPedidoSucursalClient sPedidoSucursalClient = new ServicePedidoSucursal.SPedidoSucursalClient();
                    List<Domain.PedidoSucursal> pedidos = sPedidoSucursalClient.SelectBySucursalFechaEntrega(General.ConfiguracionApp.SucursalId, deFecha.DateTime.ToFileTimeUtc()).ToList();
                    gridDatos.ItemsSource = pedidos;
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

        private void RecuperarInformacion()
        {
            try
            {
                ServiceProducto.SProductoClient sProductoClient = new ServiceProducto.SProductoClient();
                cmbProductos.ItemsSource = sProductoClient.SelectAllProductos().ToList();
                ServiceUnidad.SUnidadClient sUnidadClient = new ServiceUnidad.SUnidadClient();
                cmbUnidades.ItemsSource = sUnidadClient.SelectAll().ToList();
                cmbUnidades.SelectedItem = sUnidadClient.SelectById(4);
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

        bool ValidarDatos()
        {
            bool retorno = false;
            if (cmbProductos.SelectedIndex >= 0 && cmbUnidades.SelectedIndex >= 0 && txtPedido.Text.Trim().Length > 0 && txtCantidad.Text.Trim().Length > 0 &&
                deFecha.DateTime != null)
            {
                retorno = true;
            }
            else
            {
                MessageBox.Show("Todos los campos deben ser llenados");
            }
            return retorno;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarDatos())
            {
                try
                {
                    ServicePedidoSucursal.SPedidoSucursalClient sPedidoSucursalClient = new ServicePedidoSucursal.SPedidoSucursalClient();
                    PedidoSucursal pedido = new PedidoSucursal();
                    pedido.Cantidad = decimal.Parse(txtCantidad.Text);
                    pedido.FechaEntrega = deFecha.DateTime;
                    pedido.FechaRegistro = DateTime.Now;
                    pedido.Producto = txtPedido.Text;
                    pedido.SucursalId = General.ConfiguracionApp.SucursalId;
                    pedido.UnidadId = ((Unidad)cmbUnidades.SelectedItem).Id;
                    pedido.UsuarioId = General.UsuarioLogueado.Id;
                    sPedidoSucursalClient.Insert(pedido, pedido.FechaRegistro.ToFileTimeUtc(), pedido.FechaEntrega.ToFileTimeUtc());
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
                LimpiarDatos();
                RecuperarDatos();
            }
        }

        private void LimpiarDatos()
        {
            txtCodigoProducto.Text = "";
            txtCodigoProducto.Focus();
            cmbProductos.SelectedIndex = -1;
            txtPedido.Text = "";
            txtCantidad.Text = "";
            cmbUnidades.SelectedIndex = -1;
            deFecha.DateTime = DateTime.Now.Date;
            //deFechaEntregar.DateTime = DateTime.Now;
            txtCodigoProducto.Focus();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Esta en desarrollo");
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (gridDatos.SelectedItem != null)
            {
                if (MessageBox.Show("¿Deseas eliminar el registro seleccionado?", "Eliminando...", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        ServicePedidoSucursal.SPedidoSucursalClient sPedidoSucursalClient = new ServicePedidoSucursal.SPedidoSucursalClient();
                        PedidoSucursal pedido = ((PedidoSucursal)gridDatos.SelectedItem);
                        sPedidoSucursalClient.Delete(pedido.Id);
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
                    RecuperarDatos();
                }
            }
            else
            {
                MessageBox.Show("Primero debes de seleccionar una fila");
            }
        }

        private void txtCodigoProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && txtCodigoProducto.Text.Trim() != "")
            {
                try
                {
                    Producto producto = new ServiceProducto.SProductoClient().SelectByCodigo(txtCodigoProducto.Text);
                    if (producto != null)
                    {
                        cmbProductos.SelectedItem = producto;
                        txtPedido.Focus();
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

        private void cmbProductos_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbProductos.SelectedIndex >= 0)
            {
                Producto producto = (Producto)cmbProductos.SelectedItem;
                txtCodigoProducto.Text = producto.Codigo;
                txtPedido.Text = producto.Nombre;
                txtPedido.Focus();
            }
        }

        private void deFecha_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            RecuperarDatos();
        }

        private void deFechaEntregar_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            DateTime fecha = deFechaEntregar.DateTime;
        }

        private void txtPedido_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                txtCantidad.Focus();
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                cmbUnidades.Focus();
        }

        private void deFechaEntregar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnGuardar.Focus();
        }

        private void cmbUnidades_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                deFechaEntregar.Focus();
        }
    }
}
