using System.ComponentModel.DataAnnotations;

namespace travelAgency.model;

public class Image
{
    [Key]
    public int Id { get; set; }

    public string ImgLocation { get; set; }
}