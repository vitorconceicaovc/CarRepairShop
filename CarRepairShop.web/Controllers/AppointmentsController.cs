using CarRepairShop.web.Data;
using CarRepairShop.web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShop.web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _appointmentRepository.GetAppointmentAsync(this.User.Identity.Name);
            return View(model);
        }
    }
}
