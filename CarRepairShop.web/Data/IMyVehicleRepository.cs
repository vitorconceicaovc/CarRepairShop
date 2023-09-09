using CarRepairShop.web.Data.Entities;
using System.Linq;

namespace CarRepairShop.web.Data
{
    public interface IMyVehicleRepository : IGenericRepository<Vehicle>
    {
        public IQueryable GetAllWithUsers();
    }
}
