using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepairShop.web.Data.Entities;

namespace CarRepairShop.web.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Vehicle> GetVehicles()
        {
            return _context.Vehicles.OrderBy(v => v.CarPlate);        
        }

        public Vehicle GetVehicle(int id)
        {
            return _context.Vehicles.Find(id);
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(v => v.Id == id);
        }
    }
}
