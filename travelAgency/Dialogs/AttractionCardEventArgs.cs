using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.components;

namespace travelAgency.Dialogs
{
    public class AttractionCardEventArgs : EventArgs
    {
        public List<AttractionCard> AttractionCards { get; }

        public AttractionCardEventArgs(List<AttractionCard> attractionCards)
        {
            AttractionCards = attractionCards;
        }
    }
}
