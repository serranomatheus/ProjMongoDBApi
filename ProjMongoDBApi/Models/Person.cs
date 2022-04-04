using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjMongoDBApi.Model
{
    public class Person
    {
        #region Properties
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        #endregion
    }
}
