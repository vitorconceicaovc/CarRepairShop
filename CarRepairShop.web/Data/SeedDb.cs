﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRepairShop.web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper
            )
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
            await _userHelper.CheckRoleAsync("Mechanic");

            if (!_context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Lisboa" });
                cities.Add(new City { Name = "Porto" });
                cities.Add(new City { Name = "Faro" });

                _context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal"
                });

                await _context.SaveChangesAsync();
            }

            var user = await _userHelper.GetUserByEmailAsync("admin@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Senhor",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    PhoneNumber = "913827162",
                    Address = "Rua do Admin",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault(),
                    Role = "Admin"
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }


                await _userHelper.AddUserToRoleAsync(user, "Admin");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var mechanicUser = await _userHelper.GetUserByEmailAsync("mechanic@gmail.com");

            if (mechanicUser == null)
            {
                mechanicUser = new User
                {
                    FirstName = "Senhor",
                    LastName = "Mechanic",
                    Email = "mechanic@gmail.com",
                    UserName = "mechanic@gmail.com",
                    PhoneNumber = "913827162",
                    Address = "Rua do Mechanic",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault(),
                    Role = "Mechanic"
                };

                var result = await _userHelper.AddUserAsync(mechanicUser, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the mechanicUser in seeder");
                }


                await _userHelper.AddUserToRoleAsync(mechanicUser, "Mechanic");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(mechanicUser);
                await _userHelper.ConfirmEmailAsync(mechanicUser, token);

            }

            var customerUser = await _userHelper.GetUserByEmailAsync("customer@gmail.com");

            if (customerUser == null)
            {
                customerUser = new User
                {
                    FirstName = "Senhor",
                    LastName = "Customer",
                    Email = "customer@gmail.com",
                    UserName = "customer@gmail.com",
                    PhoneNumber = "937261536",
                    Address = "Rua do Customer",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault(),
                    Role = "Customer"
                };

                var result = await _userHelper.AddUserAsync(customerUser, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the customerUser in seeder");
                }


                await _userHelper.AddUserToRoleAsync(customerUser, "Customer");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(customerUser);
                await _userHelper.ConfirmEmailAsync(customerUser, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(customerUser, "Customer");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(customerUser, "Customer");

            }

            if (!_context.Vehicles.Any())
            {
                AddVehicle("28-24-GH", "Opel", "Corsa", "Yellow", 2003, customerUser);
                AddVehicle("23-VH-90", "Nisan", "Susano", "Red", 2006, customerUser);
                AddVehicle("12-29-TT", "Mercedes", "Ben10", "Gray", 2012, customerUser);

                await _context.SaveChangesAsync();
            }
        }

        private void AddVehicle(string plate, string brand, string model, string color, int year, User user)
        {
            _context.Vehicles.Add(new Vehicle
            {
                CarPlate = plate,
                Brand = brand,
                CarModel = model,
                Color = color,
                Year = year,
                User = user
            });
        }
    }
}
