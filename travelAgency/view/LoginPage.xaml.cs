using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace travelAgency.view
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);

            // Manually trigger the validation
            bindingExpr.UpdateSource();
        }


        private void PasswordBox_Changed(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            BindingExpression bindingExpr = passwordBox.GetBindingExpression(PasswordBoxAssist.PasswordProperty);

            // Manually trigger the validation
            bindingExpr.UpdateSource();
        }

        public event EventHandler ToSignUpClicked;

        private void To_SignUp(object sender, MouseButtonEventArgs e)
        {
            ToSignUpClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

        }
    }


}
