using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Permission
    {
        #region Properties
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        #endregion
    }
}