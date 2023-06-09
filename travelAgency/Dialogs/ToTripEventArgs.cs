using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.model;

namespace travelAgency.Dialogs
{
    public class ToTripEventArgs : EventArgs
    {
        public Trip Trip { get; }

        public ToTripEventArgs(Trip trip)
        {
            Trip = trip;
        }
    }
}
