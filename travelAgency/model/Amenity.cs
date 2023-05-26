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

public class Amenity
{
    [Key]
    public int Id { get; set; }
    public AmenityEnum  amenity { get; set; }
}
