using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace ProjDapperAirport.Services
{
    internal class Log
    {
        HttpClient ApiConnection = new HttpClient();
        public static void PostLog(Log log)
        {           
            HttpClient ApiConnection = new HttpClient();

            ApiConnection.PostAsJsonAsync("https://localhost:44395/api/Log", log);

        }
    }
}
//https://localhost:44395/api/Log