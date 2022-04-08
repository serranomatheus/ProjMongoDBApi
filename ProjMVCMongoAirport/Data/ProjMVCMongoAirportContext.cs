using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjMVCMongoAirport.Models;

namespace ProjMVCMongoAirport.Data
{
    public class ProjMVCMongoAirportContext : DbContext
    {
        public ProjMVCMongoAirportContext (DbContextOptions<ProjMVCMongoAirportContext> options)
            : base(options)
        {
        }

        public DbSet<ProjMVCMongoAirport.Models.Airport> Airport { get; set; }
    }
}
