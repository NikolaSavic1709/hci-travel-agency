using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace travelAgency.UImodels
{
    public class CalendarLabel : Label
    {
        public static DependencyProperty IsClickedProperty =
        DependencyProperty.Register("IsClicked", typeof(string), typeof(CalendarLabel), new PropertyMetadata(null));

        // Define the CLR property wrapper for the dependency property
        public string IsClicked
        {
            get { return (string)GetValue(IsClickedProperty); }
            set { SetValue(IsClickedProperty, value); }
        }
        public CalendarLabel()
        {
            IsClicked = "False";
        }
    }
}

