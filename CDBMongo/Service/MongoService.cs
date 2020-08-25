using AutoBogus;
using CDBMongo.Data.Repositories;
using CDBMongo.Model;
using CDBMongo.Model.Product.Enum;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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

                var prod = await GetProductById(product.Id.ToString());
                var prod2 = await _productRepository.FindByConsolidateIdAsync(prod.ConsolidateId);

                prod2.Name = "NameUpdated";
                var tags = new Tags()
                {
                    Name = "Alterado"
                };

                prod2.Tags.Add(tags);

                await _productRepository.ReplaceOneAsync(prod2);

                await _productRepository.DeleteByIdAsync(prod.Id.ToString());

                _logger.LogInformation("Handle Product finished");

            }
            catch (Exception e)
            {
                var ext = e.Message;

                _logger.LogError(ext);

                throw;
            }

            _logger.LogInformation("Handle Product finhished");
        }

        private ProductDto CreateProduct()
        {
            var id = ObjectId.GenerateNewId();
            var consolidateId = Guid.NewGuid().ToString();

            var productFake = new AutoFaker<ProductDto>()
                .RuleFor(f => f.Id, id)
                .RuleFor(f => f.ConsolidateId, consolidateId);

            var product = productFake.Generate();

            _logger.LogInformation("Product created");
            return product;
        }

        private async Task<ProductDto> GetProductById(string id)
        {
            var prod = await _productRepository.FindByIdAsync(id);

            return prod;
        }
    }
}
