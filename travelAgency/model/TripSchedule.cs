using System;
using System.ComponentModel.DataAnnotations;

namespace travelAgency.model;

public class TripSchedule
{
    [Key]
    public int Id { get; set; }

    public DateTime DateTime { get; set; }
    public int TripId { get; set; }
    public virtual Trip Trip { get; set; }
    public int PlaceId { get; set; }
    public virtual Place Place { get; set; }
}