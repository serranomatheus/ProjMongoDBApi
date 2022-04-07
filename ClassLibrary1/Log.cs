using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    internal class Log
    {
        #region Properties
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public User User { get; set; }
        public Object Before { get; set; }
        public Object After { get; set; }  
        public string Operation { get; set; }
        public DateTime Date { get; set; }


        #endregion
    }
}
