using CarRepairShop.web.Data;
using CarRepairShop.web.Helpers;
using CarRepairShop.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRepairShop.web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class MyVehiclesController : Controller
    {
        private readonly IMyVehicleRepository _myVehicleRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public MyVehiclesController(
                IMyVehicleRepository myVehicleRepository,
                IUserHelper userHelper,
                IImageHelper imageHelper,
                IConverterHelper converterHelper
            )
        {
            _myVehicleRepository = myVehicleRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Vehicles
        public IActionResult Index()
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(_myVehicleRepository.GetAll().Where(v => v.User.Id == userId).ToList().OrderBy(v => v.CarPlate));
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vehicle = await _myVehicleRepository.GetByIdAsync(id.Value);

            if (vehicle == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
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

                vehicle.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _myVehicleRepository.CreateAsync(vehicle);
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

			var vehicle = await _myVehicleRepository.GetByIdAsync(id.Value);

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

					var path = model.ImageUrl;

					if (model.ImageFile != null && model.ImageFile.Length > 0)
					{

						path = await _imageHelper.UploadImageAsync(model.ImageFile, "vehicles");

					}

					var vehicle = _converterHelper.ToVehicle(model, path, false);

					//vehicle.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
					await _myVehicleRepository.UpdateAsync(vehicle);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!await _myVehicleRepository.ExistAsync(model.Id))
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

			var vehicle = await _myVehicleRepository.GetByIdAsync(id.Value);

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
			var vehicle = await _myVehicleRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Excepção de Teste");
                await _myVehicleRepository.DeleteAsync(vehicle);
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
