using CarRepairShop.web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRepairShop.web.Data
{
    public class DataContext : DbContext
    {

        public DbSet<Vehicle> Vehicles { get; set; }    

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
