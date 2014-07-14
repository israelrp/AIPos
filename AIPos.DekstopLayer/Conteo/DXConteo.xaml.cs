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
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace AIPos.DekstopLayer.Conteo
{
    /// <summary>
    /// Interaction logic for DXConteo.xaml
    /// </summary>
    public partial class DXConteo : DXWindow
    {
        public DXConteo()
        {
            InitializeComponent();
            InicializarBillettes();
            InicializarMonedas();
            //List<Gasto> gastos = new List<Gasto>();
            //IEditableCollectionView view=gastos as IEditableCollectionView;
            gridGastos.ItemsSource = new GastosCollection();
        }

        void InicializarBillettes()
        {
            List<Dinero> dinero = new List<Dinero>();
            dinero.Add(new Dinero() { Concepto = 20, Conteo = 0, Monto=0 });
            dinero.Add(new Dinero() { Concepto = 50, Conteo = 0, Monto = 0 });
            dinero.Add(new Dinero() { Concepto = 100, Conteo = 0, Monto = 0 });
            dinero.Add(new Dinero() { Concepto = 200, Conteo = 0, Monto = 0 });
            dinero.Add(new Dinero() { Concepto = 500, Conteo = 0, Monto = 0 });
            dinero.Add(new Dinero() { Concepto = 1000, Conteo = 0, Monto = 0 });
            gridBilletes.ItemsSource = dinero;
        }

        void InicializarMonedas()
        {
            List<Dinero> dinero = new List<Dinero>();
            dinero.Add(new Dinero() { Concepto = decimal.Parse("0.5"), Conteo = 0, Monto = 0 });
            dinero.Add(new Dinero() { Concepto = 1, Conteo = 0, Monto = 0 });
            dinero.Add(new Dinero() { Concepto = 2, Conteo = 0, Monto = 0 });
            dinero.Add(new Dinero() { Concepto = 5, Conteo = 0, Monto = 0 });
            dinero.Add(new Dinero() { Concepto = 10, Conteo = 0, Monto = 0 });
            gridMonedas.ItemsSource = dinero;
        }

        private void TableView_CellValueChanged_1(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            List<Dinero> billetes = (List<Dinero>)gridBilletes.ItemsSource;
            foreach (var billete in billetes)
            {
                billete.Monto = billete.Conteo * billete.Concepto;
            }
            gridBilletes.ItemsSource = billetes;
            CalcularTotal();
        }

        private void TableView_CellValueChanged_2(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            List<Dinero> monedas = (List<Dinero>)gridMonedas.ItemsSource;
            foreach (var moneda in monedas)
            {
                moneda.Monto = moneda.Conteo * moneda.Concepto;
            }
            gridMonedas.ItemsSource = monedas;
            CalcularTotal();
        }

        void CalcularTotal()
        {
            List<Dinero> dineroBilletes = (List<Dinero>)gridBilletes.ItemsSource;
            decimal totalBilletes = 0;
            foreach (var billete in dineroBilletes)
            {
                totalBilletes += billete.Concepto * billete.Conteo;
            }
            lblBilletes.Content = "Total en billetes " + totalBilletes.ToString("c");

            List<Dinero> dineroMonedas = (List<Dinero>)gridMonedas.ItemsSource;
            decimal totalMonedas = 0;
            foreach (var moneda in dineroMonedas)
            {
                totalMonedas += moneda.Concepto * moneda.Conteo;
            }
            lblMonedas.Content = "Total en monedas " + totalMonedas.ToString("c");

            lblTotal.Content = "GRAN TOTAL "+ (totalBilletes + totalMonedas).ToString("c");

            decimal totalGasto = 0;
            GastosCollection gastos = (GastosCollection)gridGastos.ItemsSource;
            decimal totalGastos = 0;
            foreach (var gasto in gastos)
            {
                totalGasto += gasto.Importe;
            }

            lblGastos.Content = "Total gastos " + totalGasto.ToString("c");

            lblTotal2.Content = (totalMonedas + totalBilletes - totalGasto).ToString("c");


        }

        private void TableView_CellValueChanged_3(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            CalcularTotal();
        }


    }

    public class Dinero
    {
        public decimal Concepto { get; set; }
        public int Conteo { get; set; }
        public decimal Monto { get; set; }
    }

    public class Gasto
    {
        public string Concepto { get; set; }
        public decimal Importe { get; set; }
    }

    public class GastosCollection : ObservableCollection<Gasto>
    {
        public GastosCollection()
            : base()
        {
        }
    }
}
