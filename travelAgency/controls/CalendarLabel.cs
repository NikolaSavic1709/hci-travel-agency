using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace travelAgency.controls
{
    public class CalendarLabel : Label
    {
        public static DependencyProperty IsSelectedProperty =
        DependencyProperty.Register("IsSelected", typeof(string), typeof(CalendarLabel), new PropertyMetadata(null));

        // Define the CLR property wrapper for the dependency property
        public string IsSelected
        {
            get { return (string)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public CalendarLabel()
        {
            IsSelected = "False";
        }
    }
}

