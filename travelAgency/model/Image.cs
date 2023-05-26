using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAgency.model;

public class Image
{
    [Key]
    public int Id { get; set; }
    public string ImgLocation { get; set; }
}
