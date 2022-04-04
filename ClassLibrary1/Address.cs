using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Address
    {
        #region Properties
        [BsonId]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }        
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string FederativeUnit { get; set; }
        public string District { get; set; }
        public string Complement { get; set; }


        #endregion
    }
}