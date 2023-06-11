using System;
using travelAgency.model;

namespace travelAgency.Dialogs
{
    public class ToPlaceEventArgs : EventArgs
    {
        public Place Place { get; }

        public ToPlaceEventArgs(Place place)
        {
            Place = place;
        }
    }
}