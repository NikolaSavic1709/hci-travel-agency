using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAgency.model
{
    public class PackIconKindGroup
    {
        public PackIconKindGroup(int kindValue)
        {
            if (kindValue > 9) throw new ArgumentNullException(nameof(kindValue));
            AmenityEnum amenity = (AmenityEnum)kindValue;
            AmenityIconKind amenityIconKind = (AmenityIconKind)kindValue;

            KindValue = kindValue;

            Kind = amenityIconKind.ToString();

            Aliases = amenity.ToString();
        }

        public int KindValue { get; }
        public string Kind { get; }
        public string Aliases { get; }
    }
}
