﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace travelAgency.model
{
    public class Place
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsDeleted { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }

        public virtual List<Image> images { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}