using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.components;

namespace travelAgency.Dialogs
{
    public class TripCardEventArgs : EventArgs
    {
        public List<TripCard> TripCards { get; }

        public TripCardEventArgs(List<TripCard> tripCards)
        {
            TripCards = tripCards;
        }
    }
}
