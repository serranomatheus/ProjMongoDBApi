using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class BasePrice
    {
        #region Properties
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
        public double Value { get; set; }
        
        public DateTime InclusionDate { get; set; }
        public string LoginUser { get; set; }
        #endregion
    }
}
