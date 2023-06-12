using System;
using System.Windows;

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

            loginPage = new LoginPage(this);
            loginPage.ToSignUpClicked += To_SignUpNavigation;

            FormContent.Navigate(loginPage);
        }

        private void To_LoginNavigation(object sender, EventArgs e)
        {
            loginPage = new LoginPage(this);
            loginPage.ToSignUpClicked += To_SignUpNavigation;
            FormContent.Navigate(loginPage);
        }

        private void To_SignUpNavigation(object sender, EventArgs e)
        {
            registrationPage = new RegistrationPage();
            registrationPage.ToLoginClicked += To_LoginNavigation;
            FormContent.Navigate(registrationPage);
        }
    }
}