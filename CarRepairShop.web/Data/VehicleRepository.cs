using System.Linq;
using CarRepairShop.web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRepairShop.web.Data
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly DataContext _context;

        public VehicleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Vehicles.Include(v => v.User);
        }
    }
}
