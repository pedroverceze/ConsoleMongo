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

        private async Task HandleProduct()
        {
            try
            {
                _logger.LogInformation("Handle Product started");

                var product = CreateProduct();

                await _productRepository.InsertOneAsync(product);

                //var id = product.Id.ToString();
                var id = "d05e8682-5461-48e7-8b17-84ffd0532831";

                var prod = await GetProductById(id);

                //var consolidation = "4cb6592f-1471-422c-8791-dc48ea2add43";

                //var prod2 = await GetProductByConsolidationId(consolidation);

            }catch(Exception e)
            {
                var ext = e.Message;
                throw;
            }

            _logger.LogInformation("Handle Product finhished");
        }

        private ProductDto CreateProduct()
        {
            var id = Guid.NewGuid();

            var productFake = new AutoFaker<ProductDto>()
                .RuleFor(f => f.Id, id)
                .RuleFor(f => f.ConsolidationId, Guid.NewGuid().ToString());

            var product = productFake.Generate();

            _logger.LogInformation("Product created");
            return product;
        }

        private async Task<ProductDto> GetProductById(string id)
        {
            var prod = await _productRepository.FindByIdAsync(id);

            return prod;
        }

        private async Task<ProductDto> GetProductByConsolidationId(string id)
        {
            //var consolidation = Guid.Parse(id);

            var prod = await _productRepository.FindOneAsync(p => p.ConsolidationId == id);

            return prod;
        }

        //private async Task HandleRecipe()
        //{
        //    try
        //    {
        //        var id = "5f3d6808d22b25e2dc9b0788";
        //        var recipe = await GetRecipe(id);

        //        await InsertNewRecipe();

        //    }
        //    catch (Exception e)
        //    {
        //        var ext = e.Message;
        //        Console.WriteLine(ext);
        //    }

        //    Console.WriteLine("foi");
        //}

        //private async Task<Recipe> GetRecipe(string id)
        //{
        //    try
        //    {
        //        var recipe = await _recipeRepository.FindByIdAsync(id);
        //        return recipe;
        //    }
        //    catch (Exception e)
        //    {
        //        var ext = e.Message;
        //        throw e;
        //    }
        //}

        //private async Task InsertNewRecipe()
        //{
        //    var recipe = new Recipe();

        //    recipe.Id = ObjectId.GenerateNewId();
        //    recipe.Nome = "Miojão parte 2";
        //    recipe.ModoDePreparo = "jogar oregano e queijo ralado";
        //    recipe.Dificuldade = 0;
        //    recipe.Usuario = "Pedro";
        //    recipe.Ingredientes = new List<Ingredient>() { CreateIngredient() };

        //    await _recipeRepository.InsertOneAsync(recipe);
        //}

        //private async Task InsertRecipe(Recipe recipe)
        //{
        //    try
        //    {
        //        await _recipeRepository.InsertOneAsync(recipe);
        //    }
        //    catch (Exception ext)
        //    {
        //        Console.WriteLine(ext.Message);
        //        throw;
        //    }

        //}

        //private Ingredient CreateIngredient()
        //{
        //    var ingredient = new Ingredient();

        //    ingredient.Nome = "Miojo";
        //    ingredient.Id = ObjectId.GenerateNewId();

        //    return ingredient;
        //}
    }
}
