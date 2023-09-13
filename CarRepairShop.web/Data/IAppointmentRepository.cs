using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShop.web.Data
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IQueryable<Appointment>> GetAppointmentAsync(string userName);

        Task<IQueryable<AppointmentDetailTemp>> GetDetailTempsAsync(string userName);

        Task AddItemToAppointmentAsync(AddItemViewModel model, string userName);

        Task ModifyAppointmentDetailTempQuantityAsync(int id, double quantity);

        Task DeleteDetailTempAsync(int id);
    }
}
