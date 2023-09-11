using CarRepairShop.web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CarRepairShop.web.Data
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly DataContext _context;

        public ServiceRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboServices()
        {
            var list = _context.Services.Select(s => new SelectListItem
            {
                Text = s.ServiceName,
                Value = s.Id.ToString(),
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select a Service...",
                Value = "0"
            });

            return list;
        }
    }
}
