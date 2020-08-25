using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CDBMongo.Data.Documents
{
    public interface IDocument
    {
        [BsonId]
        ObjectId Id { get; set; }

        string ConsolidateId { get; set; }
    }
}