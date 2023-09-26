using CarRepairShop.web.Data;
using CarRepairShop.web.Helpers;
using CarRepairShop.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShop.Web.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        public VehiclesController(
            IVehicleRepository vehicleRepository,
            IUserHelper userHelper,
            IBlobHelper blobHelper,
            IConverterHelper converterHelper
            )
        {
            _vehicleRepository = vehicleRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        [Authorize(Roles = "Mechanic")]
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
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);

            if (vehicle == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            return View(vehicle);
        }

        [Authorize(Roles = "Customer")]
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

                Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "vehicles");
                }

                var vehicle = _converterHelper.ToVehicle(model, imageId, true);

                vehicle.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
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
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);

            if (vehicle == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
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

                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {

                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "vehicles");

                    }

                    var vehicle = _converterHelper.ToVehicle(model, imageId, false);

                    //vehicle.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
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
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);

            if (vehicle == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Excepção de Teste");
                await _vehicleRepository.DeleteAsync(vehicle);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{vehicle.CarPlate} provavelmente está a ser usado!";
                    ViewBag.ErrorMessage = $"{vehicle.CarPlate} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
                        $"Experimente primeiro apagar tdas os serviços que o estão a usar" +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }
        }

        public IActionResult VehicleNotFound()
        {
            return View();
        }
    }
}