using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAgency.model
{
    public class Place
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<JourneyPlace> JourneyPlaces { get; set; }
        public Place(string name, string description)
        {
            Name = name;
            Description = description;
            JourneyPlaces= new List<JourneyPlace>();
        }
    }
}
