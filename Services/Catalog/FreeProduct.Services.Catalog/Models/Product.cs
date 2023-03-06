using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeProduct.Services.Catalog.Models
{
    public class Product
    {
        [BsonId] // For MongoDb
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }


        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }


        public ProductFeature ProductFeature { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }

        [BsonIgnore]
        public Category Category { get; set; }
    }
}
