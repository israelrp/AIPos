﻿using System;
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


namespace AIPos.DekstopLayer.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : DXWindow
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            ValidarUsuario();
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarUsuario();
            }
        }

        void ValidarUsuario()
        {

            if (txtUserName.Text.Trim() != "")
            {
                if (txtPassword.Text.Trim() != "")
                {
                    ServiceUsuario.SUsuarioClient sUsuarioClient = new ServiceUsuario.SUsuarioClient();
                    int SucursalId = General.ConfiguracionApp.SucursalId;
                    Usuario usuario = sUsuarioClient.Login(txtUserName.Text, txtPassword.Password, SucursalId);
                    if (usuario != null)
                    {
                        General.UsuarioLogueado = usuario;
                        var principal = new Principal();
                        principal.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Los datos de accesos son incorrectos");
                    }
                }
            }
        }

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { txtPassword.Focus(); }
        }
    }
}
