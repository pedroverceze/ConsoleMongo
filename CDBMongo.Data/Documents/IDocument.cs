using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CDBMongo.Data.Documents
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        ObjectId Id { get; set; }
    }
}