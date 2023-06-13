using System;
using System.Windows;
using System.Windows.Input;

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
            Help.HelpProvider.SetHelpKey((DependencyObject)FormContent, "login");
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

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(System.Windows.Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = Help.HelpProvider.GetHelpKey((DependencyObject)FormContent);
                Help.HelpProvider.ShowHelp(str, this);
            }
        }
    }
}