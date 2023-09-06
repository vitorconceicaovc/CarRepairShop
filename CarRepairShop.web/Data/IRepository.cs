using System.Collections.Generic;
using System.Threading.Tasks;
using CarRepairShop.web.Data.Entities;

namespace CarRepairShop.web.Data
{
    public interface IRepository
    {
        void AddVehicle(Vehicle vehicle);

        bool VehicleExists(int id);

        Vehicle GetVehicle(int id);

        IEnumerable<Vehicle> GetVehicles();

        void RemoveVehicle(Vehicle vehicle);

        Task<bool> SaveAllAsync();

        void UpdateVehicle(Vehicle vehicle);
    }
}
