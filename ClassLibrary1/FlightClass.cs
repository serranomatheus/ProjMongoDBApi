using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class FlightClass
    {
        #region Properties
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Code { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string LoginUser { get; set; }
        #endregion
    }
}