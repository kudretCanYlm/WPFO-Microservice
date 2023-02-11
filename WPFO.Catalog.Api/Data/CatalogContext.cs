using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WPFO.Catalog.Api.Data.Interfaces;
using WPFO.Catalog.Api.Entities;

namespace WPFO.Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings: DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SendData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
