using CarRepairShop.web.Data;
using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShop.web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IServiceRepository _serviceRepository;

        public AppointmentsController(
            IAppointmentRepository appointmentRepository,
            IVehicleRepository vehicleRepository,
            IServiceRepository serviceRepository
            )
        {
            _appointmentRepository = appointmentRepository;
            _vehicleRepository = vehicleRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _appointmentRepository.GetAppointmentAsync(this.User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _appointmentRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return View(model);
        }

        public IActionResult AddAppointment()
        {
            var model = new AddItemViewModel
            {
                Quantity = 1,
                Vehicles = _vehicleRepository.GetComboVehicles(),
                Services = _serviceRepository.GetComboServices()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(AddItemViewModel model)
        {

            if (ModelState.IsValid)
            {
                await _appointmentRepository.AddItemToAppointmentAsync(model, this.User.Identity.Name);
                return RedirectToAction("Create");
            }

            return View(model);

        }
    }
}
