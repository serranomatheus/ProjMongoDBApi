using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{

    public class Passenger : Person
    {
        #region Properties
       public string CodePassport { get; set; }

        #endregion
    }
}
