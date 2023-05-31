using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using travelAgency.model;
using travelAgency.repository;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private LoginPage loginPage;
        private RegistrationPage registrationPage;

        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = this;

            registrationPage = new RegistrationPage();
            registrationPage.ToLoginClicked += To_LoginNavigation;

            loginPage = new LoginPage();
            loginPage.ToSignUpClicked += To_SignUpNavigation;

            FormContent.Navigate(registrationPage);
        }

        private void To_LoginNavigation(object sender, EventArgs e)
        {
            FormContent.Navigate(loginPage);

        }

        private void To_SignUpNavigation(object sender, EventArgs e)
        {
            FormContent.Navigate(registrationPage);

        }

    }

}
