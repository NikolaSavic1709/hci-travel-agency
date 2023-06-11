using System;
using travelAgency.model;

namespace travelAgency.Dialogs
{
    public class ToAttractionEventArgs : EventArgs
    {
        public Attraction Attraction { get; }

        public ToAttractionEventArgs(Attraction attraction)
        {
            Attraction = attraction;
        }
    }
}