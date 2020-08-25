using MongoDB.Bson;
using System;

namespace CDBMongo.Data.Documents
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public string ConsolidateId { get; set; }
    }
}
