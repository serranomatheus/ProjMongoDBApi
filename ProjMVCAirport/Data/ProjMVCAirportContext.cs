using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjMVCAirport.Models;

namespace ProjMVCAirport.Data
{
    public class ProjMVCAirportContext : DbContext
    {
        public ProjMVCAirportContext (DbContextOptions<ProjMVCAirportContext> options)
            : base(options)
        {
        }

        public DbSet<ProjMVCAirport.Models.AirportData> AirportData { get; set; }
    }
}
