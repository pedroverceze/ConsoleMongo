using MongoDB.Bson;
using System;

namespace CDBMongo.Data.Documents
{
    public abstract class Document : IDocument
    {
        public Guid Id { get; set; }
    }
}
