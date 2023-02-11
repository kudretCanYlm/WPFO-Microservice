using MongoDB.Driver;
using WPFO.Catalog.Api.Entities;

namespace WPFO.Catalog.Api.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
