using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IConfiguration _configuration;

        public CatalogContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration.GetValue<string>("ConnectionStrings:ConnectionString"));
            var database = client.GetDatabase(_configuration.GetValue<string>("ConnectionStrings:DatabaseName"));
            Products = database.GetCollection<Product>(_configuration.GetValue<string>("ConnectionStrings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
