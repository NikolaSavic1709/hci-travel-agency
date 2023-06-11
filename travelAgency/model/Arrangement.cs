using System;
using System.ComponentModel.DataAnnotations;

namespace travelAgency.model;

public class Arrangement
{
    [Key]
    public int Id { get; set; }
    public int NumberOfPersons { get; set; }
    public DateTime DateTime { get; set; }
    public double Price { get; set; }
    public int TripId { get; set; }
    public virtual Trip Trip { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public bool IsReservation { get; set; }

}
