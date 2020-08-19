using System.Threading.Tasks;

namespace CDBMongo.Service
{
    public interface IMongoService
    {
        Task MongoHandler();
    }
}