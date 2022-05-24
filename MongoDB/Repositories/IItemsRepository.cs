using System;
using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemsRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
        string CreateItem(Item item);
        string CreateItems(List<Item> items);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);
        void DeleteItemsByLongitude(double longitude);
    }
}