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
        }

        private void RecuperarDatos()
        {
            if (deFecha.DateTime != null)
            {
                ServicePedidoSucursal.SPedidoSucursalClient sPedidoSucursalClient = new ServicePedidoSucursal.SPedidoSucursalClient();
                List<Domain.PedidoSucursal> pedidos=sPedidoSucursalClient.SelectBySucursalFecha(General.ConfiguracionApp.SucursalId,deFecha.DateTime.ToFileTimeUtc()).ToList();
                ServiceUsuario.SUsuarioClient sUsuarioClient=new ServiceUsuario.SUsuarioClient();
                ServiceUnidad.SUnidadClient sUnidadClient=new ServiceUnidad.SUnidadClient();
                ServiceSucursal.SSucursalClient sSucursalClient = new ServiceSucursal.SSucursalClient();
                foreach (var pedido in pedidos)
                {
                    pedido.Usuario = sUsuarioClient.SelectById(pedido.UsuarioId);
                    pedido.Unidad = sUnidadClient.SelectById(pedido.UnidadId);
                    pedido.Sucursal = sSucursalClient.SelectById(pedido.SucursalId);
                }
                gridDatos.ItemsSource = pedidos;
            }
        }

        private void RecuperarInformacion()
        {
            ServiceProducto.SProductoClient sProductoClient = new ServiceProducto.SProductoClient();
            cmbProductos.ItemsSource = sProductoClient.SelectAllProductos().ToList();
            ServiceUnidad.SUnidadClient sUnidadClient = new ServiceUnidad.SUnidadClient();
            cmbUnidades.ItemsSource = sUnidadClient.SelectAll().ToList();
            cmbUnidades.SelectedItem = sUnidadClient.SelectById(4);
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
                ServicePedidoSucursal.SPedidoSucursalClient sPedidoSucursalClient = new ServicePedidoSucursal.SPedidoSucursalClient();
                PedidoSucursal pedido = new PedidoSucursal();
                pedido.Cantidad = decimal.Parse(txtCantidad.Text);
                pedido.FechaEntrega = deFecha.DateTime;
                pedido.FechaRegistro = DateTime.Now;
                pedido.Producto = txtPedido.Text;
                pedido.SucursalId = General.ConfiguracionApp.SucursalId;
                pedido.UnidadId = ((Unidad)cmbUnidades.SelectedItem).Id;
                pedido.UsuarioId = General.UsuarioLogueado.Id;
                sPedidoSucursalClient.Insert(pedido);
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
            deFecha.DateTime = DateTime.Now;
            deFechaEntregar.DateTime = DateTime.Now;
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
                    ServicePedidoSucursal.SPedidoSucursalClient sPedidoSucursalClient = new ServicePedidoSucursal.SPedidoSucursalClient();
                    PedidoSucursal pedido = ((PedidoSucursal)gridDatos.SelectedItem);
                    sPedidoSucursalClient.Delete(pedido.Id);
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
    }
}
