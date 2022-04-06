using System;
using System.IO;
using Models;
using ProjDapperAirport.Services;

namespace ProjDapperAirport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader readerAirport = new StreamReader(@"C:\Users\matheus\Downloads\Dados.csv");
            
            string line;
            do
            {
                line = readerAirport.ReadLine();
                if(line != null)
                {
                    var values = line.Split(';');
                    AirportData airportData = new AirportData(values[0], values[1], values[2], values[3]);
                    new AirportService().Add(airportData);
                }
            }while(line != null);

            foreach (var item in new AirportService().GetAll())
            {
                Console.WriteLine(item);
            }
        }
    }
}
