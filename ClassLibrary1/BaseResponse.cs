using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BaseResponse
    {
        public bool Sucess { get; set; }
        public object Result { get; set; }
        public List<string> Error { get; set; }

        public BaseResponse()
        {
            Error = new List<string>();
            Sucess = true;

        }
        public void ConnectionSucess(object result)
        {
            Result = result;
            Sucess = true;

        }

        public void ConnectionError(string error)
        {
            Error.Add(error);
            Sucess = false;
        }
    }
}
