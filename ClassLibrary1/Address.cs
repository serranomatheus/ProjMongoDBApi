using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Models
{
    public class Address
    {
        #region Properties
        [BsonId]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [JsonProperty("Logradouro")]
        public string Street { get; set; }
        public string Number { get; set; }
        [JsonProperty("Localidade")]
        public string City { get; set; }        
        public string Country { get; set; }
        [JsonProperty("cep")]
        public string PostalCode { get; set; }
        [JsonProperty("UF")]
        public string FederativeUnit { get; set; }
        [JsonProperty("Bairro")]
        public string District { get; set; }
        public string Complement { get; set; }

        public Address(string street, string city, string federativeUnit, string district,string number,string complement,string postalCode )
        {
            Street = street;
            City = city;
            Country = "Brazil";
            FederativeUnit = federativeUnit;
            District = district;
            Number = number;
            Complement = complement;
            PostalCode = postalCode;
            

        }


        #endregion
    }
}