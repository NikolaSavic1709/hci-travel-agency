using System.Windows;
using System.Windows.Controls;

namespace travelAgency.controls
{
    public class NavbarButton : Button
    {
        public static DependencyProperty IsClickedProperty =
        DependencyProperty.Register("IsClicked", typeof(string), typeof(NavbarButton), new PropertyMetadata(null));

        // Define the CLR property wrapper for the dependency property
        public string IsClicked
        {
            get { return (string)GetValue(IsClickedProperty); }
            set { SetValue(IsClickedProperty, value); }
        }

        public NavbarButton()
        {
            IsClicked = "False";
        }
    }
}