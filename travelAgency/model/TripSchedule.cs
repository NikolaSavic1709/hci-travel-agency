using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
