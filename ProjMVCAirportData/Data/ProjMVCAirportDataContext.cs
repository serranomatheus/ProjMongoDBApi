using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjMVCAirportData.Models;

namespace ProjMVCAirportData.Data
{
    public class ProjMVCAirportDataContext : DbContext
    {
        public ProjMVCAirportDataContext (DbContextOptions<ProjMVCAirportDataContext> options)
            : base(options)
        {
        }

        public DbSet<ProjMVCAirportData.Models.AirportData> AirportData { get; set; }
    }
}
