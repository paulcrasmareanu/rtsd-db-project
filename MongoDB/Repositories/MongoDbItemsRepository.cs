using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {

        // MongoDB will run from a docker container 
        // docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
        //27017:27017 is the local port on which docker will run

        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }

        public string CreateItem(Item item)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            itemsCollection.InsertOne(item);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            return stopWatch.Elapsed.ToString(@"m\:ss\.fff");
        }

        public string CreateItems(List<Item>items){
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            itemsCollection.InsertMany(items);
            stopWatch.Stop();
            return stopWatch.Elapsed.ToString(@"m\:ss\.fff");
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            itemsCollection.DeleteOne(filter);
        }

        public void DeleteItemsByLongitude(double longitude)
        {
            var filter = filterBuilder.Eq(item => item.Longitude, longitude);
            itemsCollection.DeleteManyAsync(filter);
        }

        public Item GetItem(Guid id)
        {
            // Filtering the item by ID
            var filter = filterBuilder.Eq(item => item.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            Stopwatch stopWatch = new Stopwatch();
            IEnumerable<Item> items;
            stopWatch.Start();
            items = itemsCollection.Find(new BsonDocument()).Limit(1).ToList();
            stopWatch.Stop();
            Console.WriteLine("Read 1 entry in " + stopWatch.Elapsed.ToString(@"m\:ss\.fff"));
            stopWatch.Restart();
            items = itemsCollection.Find(new BsonDocument()).Limit(10).ToList();
            stopWatch.Stop();
            Console.WriteLine("Read 10 entries in " + stopWatch.Elapsed.ToString(@"m\:ss\.fff"));
            stopWatch.Restart();
            items = itemsCollection.Find(new BsonDocument()).Limit(100).ToList();
            stopWatch.Stop();
            Console.WriteLine("Read 100 entries in " + stopWatch.Elapsed.ToString(@"m\:ss\.fff"));
            stopWatch.Restart();
            items = itemsCollection.Find(new BsonDocument()).Limit(1000).ToList();
            stopWatch.Stop();
            Console.WriteLine("Read 1000 entries in " + stopWatch.Elapsed.ToString(@"m\:ss\.fff"));
            stopWatch.Restart();
            items = itemsCollection.Find(new BsonDocument()).Limit(10000).ToList();
            stopWatch.Stop();
            Console.WriteLine("Read 10000 entries in " + stopWatch.Elapsed.ToString(@"m\:ss\.fff"));
            stopWatch.Restart();
            items = itemsCollection.Find(new BsonDocument()).Limit(25000).ToList();
            stopWatch.Stop();
            Console.WriteLine("Read 25000 entries in " + stopWatch.Elapsed.ToString(@"m\:ss\.fff"));
            stopWatch.Restart();
            items = itemsCollection.Find(new BsonDocument()).Limit(50000).ToList();
            stopWatch.Stop();
            Console.WriteLine("Read 50000 entries in " + stopWatch.Elapsed.ToString(@"m\:ss\.fff"));
            return items;
        }

        public void UpdateItem(Item item)
        {
            // Filtering the item by id
            var filter = filterBuilder.Eq(existing_item => existing_item.Id, item.Id);
            itemsCollection.ReplaceOne(filter, item);
        }
    }
}