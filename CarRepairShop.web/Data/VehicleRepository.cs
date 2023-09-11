using System.Collections.Generic;
using System.Linq;
using CarRepairShop.web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarRepairShop.web.Data
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly DataContext _context;

        public VehicleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Vehicles.Include(v => v.User);
        }

        public IEnumerable<SelectListItem> GetComboVehicles()
        {
            var list = _context.Vehicles.Select(c => new SelectListItem
            {
                Text = c.CarPlate,
                Value = c.Id.ToString(),
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select a vehicle...",
                Value = "0"
            });

            return list;
        }
    }
}
