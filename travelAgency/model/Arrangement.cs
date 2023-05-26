using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAgency.model;

public class Arrangement
{

    [Key]
    public int Id { get; set; }
    public int numberOfPearsons { get; set; }
    public DateTime DateTime { get; set; }
    public int TripId { get; set; }
    public virtual Trip Trip { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }

}
