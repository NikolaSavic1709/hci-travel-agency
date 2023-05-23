using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAgency.model
{
    [PrimaryKey(nameof(JourneyId), nameof(PlaceId))]
    public class JourneyPlace
    {
        [ForeignKey("Journey")]
        public int JourneyId { get; set; }
        public virtual Journey Journey { get; set; }
        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }

}
