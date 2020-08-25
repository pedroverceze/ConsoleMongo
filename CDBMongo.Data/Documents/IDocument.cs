using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CDBMongo.Data.Documents
{
    public interface IDocument
    {
        [BsonId]
        Guid Id { get; set; }
    }
}