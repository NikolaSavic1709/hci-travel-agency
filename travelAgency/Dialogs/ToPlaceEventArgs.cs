using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
