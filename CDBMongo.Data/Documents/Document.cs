using MongoDB.Bson;

namespace CDBMongo.Data.Documents
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }
}
