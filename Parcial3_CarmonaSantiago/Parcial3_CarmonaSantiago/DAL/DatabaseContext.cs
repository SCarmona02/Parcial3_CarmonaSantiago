using Microsoft.EntityFrameworkCore;
using Parcial3_CarmonaSantiago.DAL.Entities;

namespace Parcial3_CarmonaSantiago.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleDetail> VehicleDetails { get; set; }

    }
}
