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
using AIPos.DekstopLayer.Helpers;

namespace AIPos.DekstopLayer.EntregasDomicilio
{
    /// <summary>
    /// Interaction logic for DXEntregasDomicilio.xaml
    /// </summary>
    public partial class DXEntregasDomicilio : DXWindow
    {
        SelectionHelper<int> selectionHelper = new SelectionHelper<int>();
        List<ReporteServicioApartado> list=new List<ReporteServicioApartado>();        
        public List<ReporteServicioApartado> List { get { return list; } }
        public DXEntregasDomicilio()
        {
            InitializeComponent();
            RecuperarInformacion();
            gridVentas.DataContext = this;
        }

        private void RecuperarInformacion()
        {            
            List<EstatusServicioApartado> estatus = new ServiceEstatusServicioApartado.SEstatusServicioApartadoClient().SelectAll().ToList();
            if (estatus.Count > 0)
            {
                cmbEstatus.ItemsSource = estatus;
                cmbEstatus.SelectedIndex = 0;
                list = new ServiceServicioApartado.SServicioApartadoClient().RecuperarReporteServicioApartado(General.ConfiguracionApp.SucursalId, estatus[0].Id).ToList();
            }
        }

        private void btnEnviar_Click_1(object sender, RoutedEventArgs e)
        {
            Domain.ReporteServicioApartado venta=(Domain.ReporteServicioApartado)gridVentas.SelectedItem;
            DXEnviar dxEnviar = new DXEnviar();
            dxEnviar.VentaId = venta.VentaId;
            dxEnviar.ShowDialog();
            RecuperarInformacion();
        }

        private void btnConfirmar_Click_1(object sender, RoutedEventArgs e)
        {
            Domain.ReporteServicioApartado venta=(Domain.ReporteServicioApartado)gridVentas.SelectedItem;
            ServiceServicioApartado.SServicioApartadoClient servicioApartadoClient = new ServiceServicioApartado.SServicioApartadoClient();
            ServiceSeguimientoServicioApartado.SSeguimientoServicioApartadoClient seguimientoClient = new ServiceSeguimientoServicioApartado.SSeguimientoServicioApartadoClient();
            SeguimientoServicioApartado seguimiento = seguimientoClient.SelectById(venta.VentaId);
            ServicioApartado servicioApartado = servicioApartadoClient.SelectById(venta.VentaId);
            if (servicioApartado != null)
            {
                if (seguimiento != null)
                {
                    seguimiento.FechaLlegadaRepartidor = DateTime.Now;
                    seguimientoClient.Update(seguimiento);
                }
                servicioApartado.EstatusId = 6;
                servicioApartadoClient.Update(servicioApartado);
            }
            RecuperarInformacion();
        }

        private void gridVentas_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            Domain.ReporteServicioApartado venta = (Domain.ReporteServicioApartado)gridVentas.SelectedItem;
            ServiceServicioApartado.SServicioApartadoClient servicioApartadoClient = new ServiceServicioApartado.SServicioApartadoClient();
            ServicioApartado servicioApartado = servicioApartadoClient.SelectById(venta.VentaId);
            btnConfirmar.IsEnabled = false;
            btnEnviar.IsEnabled = false;
            if (servicioApartado != null)
            {
                if (servicioApartado.EstatusId == 5)
                {
                    btnConfirmar.IsEnabled = true;
                    btnEnviar.IsEnabled = false;
                }
                if (servicioApartado.EstatusId == 4)
                {
                    btnConfirmar.IsEnabled = false;
                    btnEnviar.IsEnabled = true;
                }

            }
        }

        private void gridVentas_CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            if (e.Column.FieldName != "Selected") return;
            int key = (int)e.GetListSourceFieldValue("VentaId");
            if (e.IsGetData)
                e.Value = selectionHelper.GetIsSelected(key);
            if (e.IsSetData)
                selectionHelper.SetIsSelected(key, (bool)e.Value);
        }

        private void cmbEstatus_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (cmbEstatus.SelectedIndex > -1)
            {
                EstatusServicioApartado estatus = (EstatusServicioApartado)cmbEstatus.SelectedItem;
                list = new ServiceServicioApartado.SServicioApartadoClient().RecuperarReporteServicioApartado(General.ConfiguracionApp.SucursalId, estatus.Id).ToList();
            }
        }

    }
}
