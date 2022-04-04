using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    internal class Ticket
    {
        #region Properties
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
        public double Amount { get; set; }
        public FlightClass FlightClass { get; set; }

        #endregion
    }
}
