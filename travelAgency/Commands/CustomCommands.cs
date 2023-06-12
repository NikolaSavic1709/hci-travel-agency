using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace travelAgency.view
{
    public static class CustomCommands
    {
        public static readonly RoutedCommand Filter = new RoutedCommand("Filter", typeof(CustomCommands));
        public static readonly RoutedCommand Search = new RoutedCommand("Search", typeof(CustomCommands));
        public static readonly RoutedCommand New = new RoutedCommand("New", typeof(CustomCommands));
        public static readonly RoutedCommand Logout = new RoutedCommand("Logout", typeof(CustomCommands));
        public static readonly RoutedCommand Save = new RoutedCommand("Save", typeof(CustomCommands));
        public static readonly RoutedCommand Quit = new RoutedCommand("Quit", typeof(CustomCommands));
        public static readonly RoutedCommand TripSchedule = new RoutedCommand("TripSchedule", typeof(CustomCommands));
        public static readonly RoutedCommand ToLogin = new RoutedCommand("ToLogin", typeof(CustomCommands));
        public static readonly RoutedCommand ToRegister = new RoutedCommand("ToRegister", typeof(CustomCommands));
        public static readonly RoutedCommand Plus = new RoutedCommand("Plus", typeof(CustomCommands));
        public static readonly RoutedCommand Minus = new RoutedCommand("Minus", typeof(CustomCommands));
        public static readonly RoutedCommand AddAll = new RoutedCommand("AddAll", typeof(CustomCommands));
        public static readonly RoutedCommand RemoveAll = new RoutedCommand("RemoveAll", typeof(CustomCommands));

        public static readonly RoutedCommand Home = new RoutedCommand("Home", typeof(CustomCommands));
        public static readonly RoutedCommand Places = new RoutedCommand("Places", typeof(CustomCommands));
        public static readonly RoutedCommand StayEat = new RoutedCommand("StayEat", typeof(CustomCommands));
        public static readonly RoutedCommand Report = new RoutedCommand("Report", typeof(CustomCommands));
        public static readonly RoutedCommand History = new RoutedCommand("History", typeof(CustomCommands));

        public static readonly RoutedCommand Purchase = new RoutedCommand("Purchase", typeof(CustomCommands));
        public static readonly RoutedCommand Reserve = new RoutedCommand("Reserve", typeof(CustomCommands));
    }
}
