using CarRepairShop.web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShop.web.Data
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IQueryable<Appointment>> GetAppointmentAsync(string userName);
    }
}
