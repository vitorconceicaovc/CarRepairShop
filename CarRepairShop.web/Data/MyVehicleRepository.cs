using CarRepairShop.web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarRepairShop.web.Data
{
    public class MyVehicleRepository : GenericRepository<Vehicle>, IMyVehicleRepository
    {
        private readonly DataContext _context;

        public MyVehicleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Vehicles.Include(v => v.User);
        }
    }
}
