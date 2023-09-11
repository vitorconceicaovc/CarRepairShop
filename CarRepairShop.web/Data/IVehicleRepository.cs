using System.Collections.Generic;
using System.Linq;
using CarRepairShop.web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRepairShop.web.Data
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboVehicles();
    }
}
