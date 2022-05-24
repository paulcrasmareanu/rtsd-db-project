using System;

namespace Catalog.Entities
{
    /* Record Types (Instead of class)  - used for immutable objects 
                                        - with-expresion support (e.g Item potion2 = potion1 with { Price = 12 }; )
                                        - Value-based equality support (If all the instances have the same value for all properties, they are equal)
    */
    public record Item 
    {
        public Guid Id {get; init;} // GUID = Globally Unique Identifier; init only allows setting a value during initialization of an instance (immutable properties, kinda like private set)
        public string Name {get; init;}
        public double Latitude {get; init;}
        public double Longitude {get; init;}
        public DateTimeOffset CreatedDate {get; init;} 
    }
}