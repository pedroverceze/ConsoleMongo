using CDBMongo.Data.Documents;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CDBMongo.Model
{
    public class Ingredient : Document
    {
        [BsonElement("nome")]
        public string Nome { get; set; }
    }
}
