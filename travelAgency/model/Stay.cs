using System.Collections.Generic;

namespace travelAgency.model;

public class Stay : Place
{
    public Stay() : base()
    {
        StayAmenities = new List<Amenity>();
    }
    public virtual List<Amenity> StayAmenities { get; set; }
}