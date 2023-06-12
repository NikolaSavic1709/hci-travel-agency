using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.components;
using travelAgency.model;

namespace travelAgency.Dialogs
{
    public class ArrangementCardEventArgs : EventArgs
    {
        public List<ArrangementCard> ArrangementCards { get; }

        public ArrangementCardEventArgs(List<ArrangementCard> arrangementCards)
        {
            ArrangementCards = arrangementCards;
        }
    }
}
