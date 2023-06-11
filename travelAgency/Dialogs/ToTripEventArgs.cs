using System;
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