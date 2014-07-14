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

namespace AIPos.DekstopLayer.Retiros
{
    /// <summary>
    /// Interaction logic for DXRetiros.xaml
    /// </summary>
    public partial class DXRetiros : DXWindow
    {
        public DXRetiros()
        {
            InitializeComponent();
            deFechaConsulta.DateTime = DateTime.Now;
            ServiceRetiroDinero.ISRetiroDineroClient retiroClient = new ServiceRetiroDinero.ISRetiroDineroClient();
            gridRetiros.ItemsSource = retiroClient.SelectAllByFechaSucursal(deFechaConsulta.DateTime, General.ConfiguracionApp.SucursalId);
        }



        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            decimal monto = 0;
            if (decimal.TryParse(txtMonto.Text, out monto))
            {
                RetiroDinero retiroDinero = new RetiroDinero();
                retiroDinero.Monto = monto;
                retiroDinero.Descripcion = txtDescripcion.Text;
                retiroDinero.EsCorteCaja = false;
                retiroDinero.SucursalId = General.ConfiguracionApp.SucursalId;
                retiroDinero.UsuarioId = General.UsuarioLogueado.Id;
                retiroDinero.Fecha = DateTime.Now;
                ServiceRetiroDinero.ISRetiroDineroClient retiroClient = new ServiceRetiroDinero.ISRetiroDineroClient();
                retiroClient.Insert(retiroDinero);
                txtDescripcion.Text = "";
                txtMonto.Text = "";
                gridRetiros.ItemsSource = retiroClient.SelectAllByFechaSucursal(deFechaConsulta.DateTime, General.ConfiguracionApp.SucursalId);
            }
            else
            {
                MessageBox.Show("Debes escribir el monto con números");
            }
        }

        private void deFechaConsulta_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            ServiceRetiroDinero.ISRetiroDineroClient retiroClient = new ServiceRetiroDinero.ISRetiroDineroClient();
            gridRetiros.ItemsSource = retiroClient.SelectAllByFechaSucursal(deFechaConsulta.DateTime, General.ConfiguracionApp.SucursalId);
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                txtMonto.Focus();
        }

        private void txtMonto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnGuardar.Focus();
        }


    }
}
