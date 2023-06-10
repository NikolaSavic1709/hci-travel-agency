using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAgency.model;

public enum AmenityEnum
{
    Pool, WIFI, Gym, Restaurant, PetFriendly, BabyFriendly, AirCondition, Sport, Bar, Laundry

}

public enum AmenityIconKind
{
    Swim, Wifi, Dumbbell, SilverwareForkKnife, Paw, BabyCarriage, Snowflake, Basketball, GlassCocktail, WashingMachine
}

public class Amenity
{
    [Key]
    public int Id { get; set; }
    public AmenityEnum  amenity { get; set; }

    public int StayId { get; set; }

    public Stay Stay { get; set; } 
}
