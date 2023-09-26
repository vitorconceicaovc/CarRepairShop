using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Models;
using System;

namespace CarRepairShop.web.Helpers
{
    public interface IConverterHelper
    {
        Vehicle ToVehicle(VehicleViewModel model, Guid imageId, bool isNew);

        VehicleViewModel ToVehicleViewModel(Vehicle vehicle);
    }
}
