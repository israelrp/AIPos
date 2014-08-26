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


namespace AIPos.DekstopLayer.Entradas
{
    /// <summary>
    /// Interaction logic for DXLoginEntrada.xaml
    /// </summary>
    public partial class DXLoginEntrada : DXWindow
    {
        public bool UsuarioValido { get; set; }

        public DXLoginEntrada()
        {
            this.UsuarioValido = false;
            InitializeComponent();
            txtPassword.Focus();
            txtUserName.Text = General.UsuarioLogueado.UserName;
        }

        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiceUsuario.SUsuarioClient sUsuarioClient = new ServiceUsuario.SUsuarioClient();
                int SucursalId = General.ConfiguracionApp.SucursalId;
                Usuario usuario = sUsuarioClient.Login(txtUserName.Text, txtPassword.Password, SucursalId);
                if (usuario != null)
                {
                    this.UsuarioValido = true;
                }
                this.Close();
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

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txtPassword.Focus();
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnEntrar.Focus();
        }
    }
}
