
using CDBMongo.Data.Attributes;
using CDBMongo.Data.Documents;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace CDBMongo.Model
{
    [BsonCollection("receita")]
    public class Recipe : Document
    {
        [BsonElement("nome")]
        [BsonRequired()]
        public string Nome { get; set; }

        [BsonElement("dificuldade")]
        [BsonRequired()]
        public int Dificuldade { get; set; }

        [BsonElement("modoDePreparo")]
        [BsonRequired()]
        public string ModoDePreparo { get; set; }

        [BsonElement("ingredients")]
        [BsonRequired()]
        public List<Ingredient> Ingredientes { get; set; }

        [BsonElement("Usuario")]
        [BsonRequired()]
        public string Usuario { get; set; }
    }
}
