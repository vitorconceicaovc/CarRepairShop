﻿using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShop.web.Data
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        IQueryable GetCountriesWithCities();

        Task<Country> GetCountryWithCitiesAsync(int id);

        Task<City> GetCityAsync(int id);

        Task AddCityAsync(CityViewModel model);

        Task<int> UpdateCityAsync(City city);

        Task<int> DeleteCityAsync(City city);

        IEnumerable<SelectListItem> GetComboCountries();

        IEnumerable<SelectListItem> GetComboCities(int countryId);

        Task<Country> GetCountryAsync(City city);
    }
}
