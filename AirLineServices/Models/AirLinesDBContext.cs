using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineServices.Models
{
    public class AirLinesDBContext : DbContext
    {
        public AirLinesDBContext(DbContextOptions<AirLinesDBContext> options) : base(options)
        {
        }
        public DbSet<TblAirLine> TblAirLines { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
