using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class EPSAContext : DbContext
    {
        public EPSAContext(DbContextOptions<EPSAContext> options) : base(options)
        {

        }

        public DbSet<Consumption> Consumptions { get; set; }
        
        public DbSet<Costs> Costs { get; set; }

        public DbSet<Losses> Losses { get; set; }






    }
}
