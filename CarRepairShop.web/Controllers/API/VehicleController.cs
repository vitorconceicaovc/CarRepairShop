using CarRepairShop.web.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairShop.web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehiclesController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public IActionResult GetVeicles()
        {
            return Ok(_vehicleRepository.GetAll());
        }
    }
}
