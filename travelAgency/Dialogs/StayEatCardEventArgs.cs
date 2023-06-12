using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.components;

namespace travelAgency.Dialogs
{
    public class StayEatCardEventArgs : EventArgs
    {
        public List<StayEatCard> StayEatCards { get; }

        public StayEatCardEventArgs(List<StayEatCard> stayEatCards)
        {
            StayEatCards = stayEatCards;
        }
    }
}
