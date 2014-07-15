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
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Core;


namespace AIPos.DekstopLayer
{
    /// <summary>
    /// Interaction logic for Principal.xaml
    /// </summary>
    public partial class Principal : DXRibbonWindow
    {
        public Principal()
        {
            InitializeComponent();
            this.Title = "AIPOS 2.0           Usuario: " + General.UsuarioLogueado.Nombre + " " + General.UsuarioLogueado.Paterno + " " + General.UsuarioLogueado.Materno;
        }

        private void barButtonEntradas_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (!ExisteTabName("Entradas"))
            {
                AgregarTab("Entradas", "Entradas", new Entradas.DXWEntradas().Content);
            }
        }

        private bool ExisteTabName(string Name)
        {
            bool retorno = false;
            DXTabItem tabItem= tabControl.Items.OfType<DXTabItem>().Where(x => x.Name == Name).FirstOrDefault();
            if (tabItem != null) { tabItem.Visibility = System.Windows.Visibility.Visible; tabControl.SelectedItem = tabItem; retorno = true; }
            return retorno;
        }

        private void AgregarTab(string Name, string Header, object Content)
        {
            DXTabItem ti = new DXTabItem();
            ti.Name = Name;
            ti.Header = Header;
            ti.Content = Content;
            tabControl.Items.Add(ti);
            tabControl.SelectedItem = ti;
        }

        private void barButtonVentas_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

                AgregarTab("Ventas", "Ventas", new Ventas.DXVentas().Content);
            
        }

        private void barButtonRetiros_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (!ExisteTabName("Retiros"))
            {
                AgregarTab("Retiros", "Retiros", new Retiros.DXRetiros().Content);
            }
        }

        private void DXRibbonWindow_Closed_1(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void barButtonConteo_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (!ExisteTabName("Conteo"))
            {
                AgregarTab("Conteo", "Conteo", new Conteo.DXConteo().Content);
            }
        }

        private void barButtonConfiguracion_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (!ExisteTabName("Configuracion"))
            {
                var window = new Configuracion.ConfiguracionInicial();
                window.Reconfigurar = true;
                AgregarTab("Configuracion", "Configuración", window.Content);
            }
        }

        private void barButtonRequerimientos_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (!ExisteTabName("Requerimientos"))
            {
                var window = new Requerimientos.DXRequerimientos();
                AgregarTab("Requerimientos", "Requerimientos", window.Content);
            }
        }

        private void barButtonInventarios_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (!ExisteTabName("Inventarios"))
            {
                var window = new Inventario.DXInventarios() ;
                AgregarTab("Inventarios", "Inventarios", window.Content);
            }
        }

        private void barButtonCorteCaja_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (!ExisteTabName("CorteCaja"))
            {
                var window = new CorteCaja.DXCorteCaja();
                AgregarTab("CorteCaja", "CorteCaja", window.Content);
            }
        }

        private void barButtonEntragas_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (!ExisteTabName("EntregasDomicilio"))
            {
                var window = new EntregasDomicilio.DXEntregasDomicilio();
                AgregarTab("EntregasDomicilio", "EntregasDomicilio", window.Content);
            }
        }
    }
}
