using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.components;

namespace travelAgency.Dialogs
{
    public class ClientTripCardEventArgs : EventArgs
    {
        public List<ClientTripCard> ClientTripCards { get; }

        public ClientTripCardEventArgs(List<ClientTripCard> clientTripCards)
        {
            ClientTripCards = clientTripCards;
        }
    }
}
