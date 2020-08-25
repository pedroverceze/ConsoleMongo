using AutoBogus;
using CDBMongo.Data.Repositories;
using CDBMongo.Model;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TemplateKafka.Producer.Domain.Products.Dto;

namespace CDBMongo.Service
{
    public class MongoService : IMongoService
    {
        private readonly IMongoRepository<Recipe> _recipeRepository;
        private readonly IMongoRepository<ProductDto> _productRepository;
        private readonly ILogger<MongoService> _logger;

        public MongoService(IMongoRepository<Recipe> recipeRepository,
                            IMongoRepository<ProductDto> productRepository,
                            ILogger<MongoService> logger)
        {
            _recipeRepository = recipeRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task MongoHandler()
        {
            await HandleProduct();
        }

        //Product CRUD
        private async Task HandleProduct()
        {
            try
            {
                _logger.LogInformation("Handle Product started");

                var product = CreateProduct();

                await _productRepository.InsertOneAsync(product);

                var prod = await GetProductById(product.Id);

                prod.Name = "NameUpdated";

                await _productRepository.ReplaceOneAsync(prod);

                await _productRepository.DeleteByIdAsync(prod.Id);

                _logger.LogInformation("Handle Product finished");

            }
            catch(Exception e)
            {
                var ext = e.Message;

                _logger.LogError(ext);

                throw;
            }

            _logger.LogInformation("Handle Product finhished");
        }

        private ProductDto CreateProduct()
        {
            var id = Guid.NewGuid();

            var productFake = new AutoFaker<ProductDto>()
                .RuleFor(f => f.Id, id);

            var product = productFake.Generate();

            _logger.LogInformation("Product created");
            return product;
        }

        private async Task<ProductDto> GetProductById(Guid id)
        {
            var prod = await _productRepository.FindByIdAsync(id);

            return prod;
        }
    }
}
