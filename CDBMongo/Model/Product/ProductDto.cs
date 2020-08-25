using CDBMongo.Data.Attributes;
using CDBMongo.Data.Documents;
using CDBMongo.Model.Product.Enum;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using TemplateKafka.Producer.Domain.Products.Enums;

namespace TemplateKafka.Producer.Domain.Products.Dto
{
    [BsonCollection("product")]
    public class ProductDto : Document
    {
        [BsonElement("Name")]
        [BsonRequired()]
        public string Name { get; set; }

        [BsonElement("Image")]
        [BsonRequired()]
        public string Image { get; set; }

        [BsonElement("Price")]
        [BsonRequired()]
        public decimal Price { get; set; }

        [BsonElement("Stock")]
        [BsonRequired()]
        public int Stock { get; set; }

        [BsonElement("Tags")]
        [BsonRequired()]
        public List<Tags> Tags { get; set; }

        [BsonElement("Category")]
        [BsonRequired()]
        public string Category { get; set; }

        [BsonElement("Vendor")]
        [BsonRequired()]
        public string Vendor { get; set; }

        [BsonElement("Status")]
        [BsonRequired()]
        public EProductStatus Status { get; set; }
    }
}
