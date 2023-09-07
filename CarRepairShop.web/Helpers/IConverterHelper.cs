using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Models;

namespace CarRepairShop.web.Helpers
{
    public interface IConverterHelper
    {
        Vehicle ToVehicle(VehicleViewModel model, string path, bool isNew);

        VehicleViewModel ToVehicleViewModel(Vehicle vehicle);
    }
}
