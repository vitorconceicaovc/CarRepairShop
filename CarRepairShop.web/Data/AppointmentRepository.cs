using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Helpers;
using CarRepairShop.web.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<IQueryable<AppointmentDetailTemp>> GetDetailTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);

            if (user == null)
            {
                return null;
            }

            return _context.AppointmentDetailsTemp
                .Include(v => v.Vehicle)
                .Where(a => a.User == user)
                .OrderBy(v => v.Vehicle.CarPlate);
        }

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

        public async Task AddItemToAppointmentAsync(AddItemViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);

            if (user == null)
            {
                return;
            }

            var vehicle = await _context.Vehicles.FindAsync(model.VehicleId);
            var service = await _context.Services.FindAsync(model.ServiceId);

            if (vehicle == null)
            {
                return;
            }

            var appointmentDetailTemp = await _context.AppointmentDetailsTemp
                .Where(odt => odt.User == user && odt.Vehicle == vehicle)
                .FirstOrDefaultAsync();

            if (appointmentDetailTemp == null)
            {
                appointmentDetailTemp = new AppointmentDetailTemp
                {
                    Price = service.Price,
                    Vehicle = vehicle,
                    Quantity = model.Quantity,
                    User = user,
                };

                _context.AppointmentDetailsTemp.Add(appointmentDetailTemp);
            }
            else
            {
                appointmentDetailTemp.Quantity += model.Quantity;
                _context.AppointmentDetailsTemp.Update(appointmentDetailTemp);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ModifyAppointmentDetailTempQuantityAsync(int id, double quantity)
        {
            var appointmentDetailTemp = await _context.AppointmentDetailsTemp.FindAsync(id);

            if (appointmentDetailTemp == null)
            {
                return;
            }

            appointmentDetailTemp.Quantity += quantity;

            if (appointmentDetailTemp.Quantity > 0)
            {
                _context.AppointmentDetailsTemp.Update(appointmentDetailTemp);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDetailTempAsync(int id)
        {
            var appointmentDetailTemp = await _context.AppointmentDetailsTemp.FindAsync(id);

            if (appointmentDetailTemp == null)
            {
                return;
            }

            _context.AppointmentDetailsTemp.Remove(appointmentDetailTemp);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ConfirmAppointmentAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);

            if (user == null)
            {
                return false;
            }

            var appointmentTmps = await _context.AppointmentDetailsTemp
                .Include(a => a.Vehicle)
                .Where(a => a.User == user)
                .ToListAsync();

            if (appointmentTmps == null || appointmentTmps.Count == 0)
            {
                return false;
            }

            var details = appointmentTmps.Select(a => new AppointmentDetail
            {
                Price = a.Price,
                Vehicle = a.Vehicle,
                Quantity = a.Quantity
            }).ToList();

            var appointment = new Appointment
            {
                AppointmentDate = DateTime.UtcNow,
                User = user,
                Items = details
            };

            await CreateAsync(appointment);
            _context.AppointmentDetailsTemp.RemoveRange(appointmentTmps);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
