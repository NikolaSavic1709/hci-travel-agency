using System.Collections.Generic;

namespace travelAgency.model;

public class Stay : Place
{
    public virtual List<Amenity> StayAmenities { get; set; }
}