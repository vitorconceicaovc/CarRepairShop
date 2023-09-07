using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarRepairShop.web.Data;
using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Helpers;
using CarRepairShop.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRepairShop.Web.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public VehiclesController(
            IVehicleRepository bookRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper
            )
        {
            _vehicleRepository = bookRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Vehicles
        public IActionResult Index()
        {
            return View(_vehicleRepository.GetAll().OrderBy(b => b.CarPlate));
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
        public async Task<IActionResult> Create(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {

                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {

                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "vehicles");
                }

                var vehicle = _converterHelper.ToVehicle(model, path, true);

                //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO
                vehicle.User = await _userHelper.GetUserByEmailAsync("admin@gmail.com");
                await _vehicleRepository.CreateAsync(vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
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

            var model = _converterHelper.ToVehicleViewModel(vehicle);
            return View(model);

        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    var path = model.ImageUrl;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {

                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "vehicles");

                    }

                    var vehicle = _converterHelper.ToVehicle(model, path, false);

                    //TODO: MODIFICAR PARA O USER QUE TIVER LOGADO
                    vehicle.User = await _userHelper.GetUserByEmailAsync("admin@gmail.com");
                    await _vehicleRepository.UpdateAsync(vehicle);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _vehicleRepository.ExistAsync(model.Id))
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
            return View(model);
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