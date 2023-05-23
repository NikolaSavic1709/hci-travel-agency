using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace travelAgency.model
{
    public class Journey
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<JourneyPlace> JourneyPlaces { get; set; }
        public Journey(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
            JourneyPlaces = new List<JourneyPlace>();
        }
    }
}
