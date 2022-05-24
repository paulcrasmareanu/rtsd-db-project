using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos
{
    public record CreateItemDto
    {        
        [Required]
        [Range(-90,90)]
        public double Latitude {get; init;}

        [Required]
        [Range(-180,180)]
        public double Longitude {get; init;}
    }
}