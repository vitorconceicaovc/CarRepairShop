using System.Linq;
using System.Threading.Tasks;
using CarRepairShop.web.Data;
using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperShop.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserHelper _userHelper;

        public VehiclesController(
            IVehicleRepository vehicleRepository,
            IUserHelper userHelper
            )
        {
            _vehicleRepository = vehicleRepository;
            _userHelper = userHelper;
        }

        // GET: Vehicles
        public IActionResult Index()
        {
            return View(_vehicleRepository.GetAll().OrderBy(v => v.CarPlate));
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO
                vehicle.User = await _userHelper.GetUserByEmailAsync("admin@gmail.com");
                await _vehicleRepository.CreateAsync(vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);

            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO
                    vehicle.User = await _userHelper.GetUserByEmailAsync("admin@gmail.com");
                    await _vehicleRepository.UpdateAsync(vehicle);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _vehicleRepository.ExistAsync(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            await _vehicleRepository.DeleteAsync(vehicle);
            return RedirectToAction(nameof(Index));
        }
    }
}