using Catalog.Dtos;
using Catalog.Entities;
using System.Collections.Generic;

namespace Catalog
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item){
            return new ItemDto{
                Id = item.Id,
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                CreatedDate = item.CreatedDate
            };
        }

        public static List<ItemDto> AsDto(this List<Item> items){
            
            List<ItemDto> itemsDtos = new List<ItemDto>();
            
            foreach(Item item in items){
                itemsDtos.Add(
                    new ItemDto{
                        Id = item.Id,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        CreatedDate = item.CreatedDate
                    }
                );
            }
            return itemsDtos;
        }
    }
}