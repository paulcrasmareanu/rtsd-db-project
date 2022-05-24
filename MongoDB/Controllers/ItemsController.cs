using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository; 
        }

        // GET /items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select( item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)// ActionResult allows to return not only Item type
        {
            var item = repository.GetItem(id);
            if(item is null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        // POST /items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Random rand = new Random();
            Item item = new(){
                Id = Guid.NewGuid(),
                Latitude = itemDto.Latitude,
                Longitude = itemDto.Longitude,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            Console.WriteLine("One entry : " + repository.CreateItem(item));

            List<Item> items = new List<Item>();
            for(int i = 0; i < 10; i++){
                items.Add(
                    new Item{
                        Id = Guid.NewGuid(),
                        Latitude = rand.NextDouble() * (90 - (-90) + (-90)),
                        Longitude = rand.NextDouble() * (180 - (-180) + (-180)),
                        CreatedDate = DateTimeOffset.UtcNow,
                    }
                );
            }

            Console.WriteLine("10 entries : " + repository.CreateItems(items));

            items = new List<Item>();
            for(int i = 0; i < 100; i++){
                items.Add(
                    new Item{
                        Id = Guid.NewGuid(),
                        Latitude = rand.NextDouble() * (90 - (-90) + (-90)),
                        Longitude = rand.NextDouble() * (180 - (-180) + (-180)),
                        CreatedDate = DateTimeOffset.UtcNow,
                    }
                );
            }

            Console.WriteLine("100 entries : " + repository.CreateItems(items));

            items = new List<Item>();
            for(int i = 0; i < 1000; i++){
                items.Add(
                    new Item{
                        Id = Guid.NewGuid(),
                        Latitude = rand.NextDouble() * (90 - (-90) + (-90)),
                        Longitude = rand.NextDouble() * (180 - (-180) + (-180)),
                        CreatedDate = DateTimeOffset.UtcNow,
                    }
                );
            }

            Console.WriteLine("1000 entries : " + repository.CreateItems(items));

            items = new List<Item>();
            for(int i = 0; i < 10000; i++){
                items.Add(
                    new Item{
                        Id = Guid.NewGuid(),
                        Latitude = rand.NextDouble() * (90 - (-90) + (-90)),
                        Longitude = rand.NextDouble() * (180 - (-180) + (-180)),
                        CreatedDate = DateTimeOffset.UtcNow,
                    }
                );
            }

            Console.WriteLine("10000 entries : " + repository.CreateItems(items));

            items = new List<Item>();
            for(int i = 0; i < 25000; i++){
                items.Add(
                    new Item{
                        Id = Guid.NewGuid(),
                        Latitude = rand.NextDouble() * (90 - (-90) + (-90)),
                        Longitude = rand.NextDouble() * (180 - (-180) + (-180)),
                        CreatedDate = DateTimeOffset.UtcNow,
                    }
                );
            }

            Console.WriteLine("25000 entries : " + repository.CreateItems(items));

            items = new List<Item>();
            for(int i = 0; i < 50000; i++){
                items.Add(
                    new Item{
                        Id = Guid.NewGuid(),
                        Latitude = rand.NextDouble() * (90 - (-90) + (-90)),
                        Longitude = rand.NextDouble() * (180 - (-180) + (-180)),
                        CreatedDate = DateTimeOffset.UtcNow,
                    }
                );
            }

            Console.WriteLine("50000 entries : " + repository.CreateItems(items));

            return CreatedAtAction(nameof(GetItem), /*Anonymous type, not typeof Item */ new { id = item.Id }, item.AsDto());
        }

        [HttpPost("many")]
        public ActionResult<ItemDto> CreateItems(List<CreateItemDto> itemsDto)
        {

            List<Item> items = new List<Item>();
            foreach(CreateItemDto dto in itemsDto){
                items.Add(
                    new Item{
                        Id = Guid.NewGuid(),
                        Latitude = dto.Latitude,
                        Longitude = dto.Longitude,
                        CreatedDate = DateTimeOffset.UtcNow,
                    }
                );
            }
            repository.CreateItems(items);

            return Created("https://localhost:5001/swagger/many", items.AsDto());
        }

        // PUT /items/
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repository.GetItem(id);

            if(existingItem is null)
            {
                return NotFound();
            }
            else
            {
                Item updatedItem = existingItem with {
                Latitude = itemDto.Latitude,
                Longitude = itemDto.Longitude,
                };

                repository.UpdateItem(updatedItem);
                return NoContent();
            }
        }
        // DELETE /items/
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if(existingItem is null)
            {
                return NotFound();
            }
            else
            {
                repository.DeleteItem(id);
                return NoContent();
            }
        }

        [HttpDelete("delete/{longitude}")]
        public ActionResult DeleteItems(double longitude){
            repository.DeleteItemsByLongitude(longitude);
            return NoContent();
        }

    }
}