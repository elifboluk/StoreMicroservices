using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeProduct.Services.Catalog.Models
{
    public class Category
    {
        [BsonId] // For MongoDb
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
