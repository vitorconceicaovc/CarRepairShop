using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShop.web.Data
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public AppointmentRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IUserHelper UserHelper { get; }

        public async Task<IQueryable<Appointment>> GetAppointmentAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Customer"))
            {
                return _context.Appointments
                    .Include(i => i.Items)
                    .ThenInclude(v => v.Vehicle)
                    .OrderByDescending(o => o.AppointmentDate);
            }

            return _context.Appointments
                .Include(i => i.Items)
                .ThenInclude(v => v.Vehicle)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.AppointmentDate);
        }
    }
}
