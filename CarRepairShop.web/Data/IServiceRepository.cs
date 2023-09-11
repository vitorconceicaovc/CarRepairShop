using CarRepairShop.web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CarRepairShop.web.Data
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        IEnumerable<SelectListItem> GetComboServices();
    }
}
