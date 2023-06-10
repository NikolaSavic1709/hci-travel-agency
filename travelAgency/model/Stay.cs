using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAgency.model;

public class Stay : Place
{
    public Stay() : base()
    {
        StayAmenities = new List<Amenity>();
    }
    public virtual List<Amenity> StayAmenities { get; set; }
}
