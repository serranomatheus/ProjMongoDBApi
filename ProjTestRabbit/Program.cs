using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace ProjTestRabbit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpClient ApiConnection = new HttpClient();

            Log log = new Log("dasdas", "dasdas", "dasdasd", "dasdsa");

            for (int i = 1; i <= 500; i++)
            {
                Console.WriteLine(i);
                Class1.Add(log).Wait();
            }

            
        }
    }
}
