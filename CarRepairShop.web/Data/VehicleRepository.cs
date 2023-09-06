using CarRepairShop.web.Data.Entities;

namespace CarRepairShop.web.Data
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DataContext context) : base(context)
        {

        }
    }
}
