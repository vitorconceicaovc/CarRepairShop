﻿using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Models;
using System;

namespace CarRepairShop.web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Vehicle ToVehicle(VehicleViewModel model, Guid imageId, bool isNew)
        {
            return new Vehicle
            {
                Id = isNew ? 0 : model.Id,
                CarPlate = model.CarPlate,
                Brand = model.Brand,    
                CarModel = model.CarModel,  
                Color = model.Color,    
                Year = model.Year,
                ImageId = imageId,
                User = model.User
            };
        }

        public VehicleViewModel ToVehicleViewModel(Vehicle vehicle)
        {
            return new VehicleViewModel
            {
                Id = vehicle.Id,
                CarPlate = vehicle.CarPlate,    
                Brand = vehicle.Brand,  
                CarModel = vehicle.CarModel,    
                Color = vehicle.Color,
                Year = vehicle.Year,
                ImageId = vehicle.ImageId,
                User = vehicle.User
            };
        }
    }
}
