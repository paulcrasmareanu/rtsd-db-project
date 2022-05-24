using System;

namespace Catalog.Dtos
{

    // Data transfer object presents objects used internaly in a more secure way, 
    //for example, we can let some properties out of the presented objects, etc..
    public record ItemDto
    {
        public Guid Id {get; init;}
        public double Latitude {get; init;}
        public double Longitude {get; init;}
        public DateTimeOffset CreatedDate {get; init;} 
    }
}