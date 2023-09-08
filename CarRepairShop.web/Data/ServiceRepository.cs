using CarRepairShop.web.Data.Entities;
using System.Linq;

namespace CarRepairShop.web.Data
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(DataContext context) : base(context)
        {

        }
    }
}
