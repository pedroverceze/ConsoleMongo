using CDBMongo.Data.Repositories;
using CDBMongo.Model;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

namespace CDBMongo.Service
{
    public class MongoService : IMongoService
    {
        private readonly IMongoRepository<Recipe> _investmentFundRepository;
        public MongoService(IMongoRepository<Recipe> mongoRepository)
        {
            _investmentFundRepository = mongoRepository;
        }

        public async Task MongoHandler()
        {
            try
            {
                var investmentFund = await _investmentFundRepository.FindByIdAsync("5e6a297641f21a118007ceb0");

                investmentFund.Id = ObjectId.GenerateNewId();
                investmentFund.RegisterDate = DateTime.Now;

                await _investmentFundRepository.InsertOneAsync(investmentFund);
            }
            catch (Exception e)
            {
                var ext = e.Message;
                Console.WriteLine(ext);
            }

            Console.WriteLine("foi");
        }
    }
}
