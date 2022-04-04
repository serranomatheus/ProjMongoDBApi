using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjMongoDBApi.Model
{
    public class Address
    {
        #region Properties
        [BsonId]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Street { get; set; }
        public string Number { get; set; }
        #endregion
    }
}
