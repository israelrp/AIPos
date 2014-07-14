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


namespace AIPos.DekstopLayer.Inventario
{
    /// <summary>
    /// Interaction logic for DXInventarios.xaml
    /// </summary>
    public partial class DXInventarios : DXWindow
    {
        public DXInventarios()
        {
            InitializeComponent();
            RecuperarInformacion();
            deFecha.DateTime = DateTime.Now;
            RecuperarDatos();
        }

        private void RecuperarDatos()
        {
            if (deFecha.DateTime != null)
            {
                ServiceInventario.SInventarioClient sInventarioClient = new ServiceInventario.SInventarioClient();
                List<Domain.Inventario> inventarios=sInventarioClient.SelectBySucursalFecha(General.ConfiguracionApp.SucursalId, deFecha.DateTime.ToFileTimeUtc()).ToList();
                foreach(var item in inventarios)
                {
                    item.Producto=new ServiceProducto.SProductoClient().SelectById(item.ProductoId);
                    item.Usuario=new ServiceUsuario.SUsuarioClient().SelectById(item.UsuarioId);
                }
                gridDatos.ItemsSource = inventarios;
            }
        }

        private void RecuperarInformacion()
        {
            ServiceProducto.SProductoClient sProductoClient = new ServiceProducto.SProductoClient();
            cmbProductos.ItemsSource = sProductoClient.SelectAllProductos().ToList();
        }

        bool ValidarDatos()
        {
            bool retorno = false;
            if (cmbProductos.SelectedIndex >= 0 && txtCantidad.Text.Trim().Length > 0)
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
                ServiceInventario.SInventarioClient sInventarioClient = new ServiceInventario.SInventarioClient();
                AIPos.Domain.Inventario inventario = new Domain.Inventario();
                inventario.CantidadReal = decimal.Parse(txtCantidad.Text);
                inventario.CantidadSistema = 0;
                inventario.FechaRegistro = DateTime.Now;
                inventario.PrecioUnitario = ((Producto)cmbProductos.SelectedItem).Precio;
                ServiceListaPrecioProducto.ISListaPrecioProductoClient isListaPrecioProductoClient = new ServiceListaPrecioProducto.ISListaPrecioProductoClient();
                ListaPrecioProducto listaPrecioProducto = null;
                if (listaPrecioProducto == null)
                {
                    listaPrecioProducto = isListaPrecioProductoClient.SelectByProductoSucursal(General.ConfiguracionApp.SucursalId, ((Producto)cmbProductos.SelectedItem).Id);
                    if (listaPrecioProducto != null)
                        inventario.PrecioUnitario = listaPrecioProducto.Precio;

                }
                
                inventario.Monto = inventario.PrecioUnitario*inventario.CantidadReal;
                inventario.ProductoId = ((Producto)cmbProductos.SelectedItem).Id;
                inventario.SucursalId = General.ConfiguracionApp.SucursalId;
                inventario.UsuarioId = General.UsuarioLogueado.Id;
                sInventarioClient.Insert(inventario);
                LimpiarDatos();
                RecuperarDatos();
            }
        }


        private void LimpiarDatos()
        {
            txtCantidad.Text = "";
            txtCodigoProducto.Text = "";
            cmbProductos.SelectedIndex = -1;
            deFecha.DateTime = DateTime.Now;
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
                    ServiceInventario.SInventarioClient sInventarioClient = new ServiceInventario.SInventarioClient();
                    AIPos.Domain.Inventario inventario = ((AIPos.Domain.Inventario)gridDatos.SelectedItem);
                    sInventarioClient.Delete(inventario.Id);
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
                    txtCantidad.Focus();
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
                txtCantidad.Focus();
            }
        }

        private void deFecha_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            RecuperarDatos();
        }

        private void cmbProductos_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                txtCantidad.Focus();
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnGuardar.Focus();
        }
    }
}
