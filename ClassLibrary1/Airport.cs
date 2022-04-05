using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Airport
    {
        #region Properties
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public  Address Address { get; set; }
        public string CodeIata { get; set; }
        #endregion
    }
}