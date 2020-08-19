using CDBMongo.Data.Repositories;
using CDBMongo.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDBMongo.Service
{
    public class MongoService : IMongoService
    {
        private readonly IMongoRepository<Recipe> _recipeRepository;
        public MongoService(IMongoRepository<Recipe> mongoRepository)
        {
            _recipeRepository = mongoRepository;
        }

        public async Task MongoHandler()
        {
            try
            {
                var id = "5f3d6808d22b25e2dc9b0788";
                var recipe = await GetRecipe(id);

                //await InsertNewRecipe();

            }
            catch (Exception e)
            {
                var ext = e.Message;
                Console.WriteLine(ext);
            }

            Console.WriteLine("foi");
        }

        private async Task<Recipe> GetRecipe(string id)
        {
            try
            {
                var recipe = await _recipeRepository.FindByIdAsync(id);
                return recipe;
            }
            catch (Exception e)
            {
                var ext = e.Message;
                throw e;
            }
        }

        private async Task InsertNewRecipe()
        {
            var recipe = new Recipe();

            recipe.Id = ObjectId.GenerateNewId();
            recipe.Nome = "Miojão da massa";
            recipe.ModoDePreparo = "pegar o miojo, botar na agua fervente e ta pronto";
            recipe.Dificuldade = 0;
            recipe.Usuario = "Pedro";
            recipe.Ingredientes = new List<Ingredient>() { CreateIngredient() };

            await _recipeRepository.InsertOneAsync(recipe);
        }

        private async Task InsertRecipe(Recipe recipe)
        {
            try
            {
                await _recipeRepository.InsertOneAsync(recipe);
            }
            catch (Exception ext)
            {
                Console.WriteLine(ext.Message);
                throw;
            }

        }

        private Ingredient CreateIngredient()
        {
            var ingredient = new Ingredient();

            ingredient.Nome = "Miojo";
            ingredient.Id = ObjectId.GenerateNewId();

            return ingredient;
        }
    }
}
