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

namespace AIPos.DekstopLayer.CorteCaja
{
    /// <summary>
    /// Interaction logic for DXRealizarCorteCaja.xaml
    /// </summary>
    public partial class DXRealizarCorteCaja : DXWindow
    {
        public Domain.CorteCaja CorteCaja { get; set; }

        public DXRealizarCorteCaja()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click_1(object sender, RoutedEventArgs e)
        {
            decimal corteEntregado = 0;
            decimal dejaCambio = 0;
            if (decimal.TryParse(txtRetiraEfectivo.Text, out corteEntregado))
            {
                if (decimal.TryParse(txtDejaCambio.Text, out dejaCambio))
                {
                    ServiceRetiroDinero.ISRetiroDineroClient retiroDineroClient = new ServiceRetiroDinero.ISRetiroDineroClient();
                    List<Domain.RetiroDinero> retiros = retiroDineroClient.SelectAllByFechaSucursal(CorteCaja.Fecha, General.ConfiguracionApp.SucursalId).ToList();
                    Usuario usuario = new ServiceUsuario.SUsuarioClient().SelectById(CorteCaja.UsuarioId);
                    CorteCaja.CorteEntregado = corteEntregado;
                    CorteCaja.QuienRetira = txtQuienRetira.Text;
                    CorteCaja.Diferencia = CorteCaja.TotalCaja - CorteCaja.CorteEntregado;
                    Documentos.CorteCaja report = new Documentos.CorteCaja();
                    Domain.Sucursal sucursal = new ServiceSucursal.SSucursalClient().SelectById(General.ConfiguracionApp.SucursalId);
                    sucursal.Direccion = new ServiceDireccion.SDireccionClient().SelectById(sucursal.DireccionId);
                    report.Database.Tables[0].SetDataSource(new List<Sucursal>() { sucursal });
                    report.Database.Tables[1].SetDataSource(new List<Direccion>() { sucursal.Direccion });
                    report.Database.Tables[2].SetDataSource(new List<Domain.CorteCaja>() { CorteCaja });
                    report.Database.Tables[3].SetDataSource(new List<Domain.Usuario>() { usuario });
                    report.Database.Tables[4].SetDataSource(retiros);
                    report.PrintOptions.PrinterName = General.ConfiguracionApp.MiniPrinter;
                    report.PrintToPrinter(2, false, 1, 1);
                    while (MessageBox.Show("¿Se imprimio correctamente el corte de caja?", "Corte caja", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        report.PrintToPrinter(2, false, 1, 1);
                    }
                    ServiceCorteCaja.SCorteCajaClient corteCajaClient = new ServiceCorteCaja.SCorteCajaClient();
                    RetiroDinero retiroDinero = new RetiroDinero();
                    retiroDinero.Descripcion = "CORTE DE CAJA DEL DÍA " + CorteCaja.Fecha.ToShortDateString() + " DEL USUARIO " + usuario.Nombre;
                    retiroDinero.EsCorteCaja = true;
                    retiroDinero.Fecha = CorteCaja.Fecha;
                    retiroDinero.Monto = CorteCaja.CorteEntregado;
                    retiroDinero.SucursalId = CorteCaja.SucursalId;
                    retiroDinero.UsuarioId = usuario.Id;
                    retiroDineroClient.Insert(retiroDinero);
                    corteCajaClient.Insert(CorteCaja);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Debe ingresar cuanto deja de cambio");
                }
            }
            else
            {
                MessageBox.Show("Debes ingresar cuanto retiran");
            }
        }
    }
}
