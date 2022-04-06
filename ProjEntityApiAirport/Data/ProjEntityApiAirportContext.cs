using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ProjEntityApiAirport.Data
{
    public class ProjEntityApiAirportContext : DbContext
    {
        public ProjEntityApiAirportContext (DbContextOptions<ProjEntityApiAirportContext> options)
            : base(options)
        {
        }

        public DbSet<Models.AirportData> AirportData { get; set; }
    }
}
