using System;
using travelAgency.model;

namespace travelAgency.Dialogs;

public class ToStayEatEventArgs : EventArgs
{
    public Place Place { get; }

    public ToStayEatEventArgs(Place place)
    {
        Place = place;
    }
}