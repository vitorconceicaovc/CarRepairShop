using CarRepairShop.web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRepairShop.web.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<Appointment> Appointments { get; set; }    

        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }

        public DbSet<AppointmentDetailTemp> AppointmentDetailsTemp { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //Habilitar a regra de apagar em cascata (Cascade Delete Rule)
        //protected override void OnModelCreating(ModelBuilder ModelBuilder)
        //{
        //    var cascadeFKs = ModelBuilder.Model
        //        .GetEntityTypes()
        //        .SelectMany(t => t.GetForeignKeys())
        //        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        //    foreach (var fk in cascadeFKs)
        //    {
        //        fk.DeleteBehavior = DeleteBehavior.Restrict; 
        //    }

        //    base.OnModelCreating(ModelBuilder); 
        //}
    }
}
