using CarRepairShop.web.Data;
using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShop.web.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(
                IServiceRepository serviceRepository
            )
        {
            _serviceRepository = serviceRepository;
        }

        // GET: Services
        public IActionResult Index()
        {
            return View(_serviceRepository.GetAll().OrderBy(s => s.ServiceName));
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            var service = await _serviceRepository.GetByIdAsync(id.Value);

            if (service == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (ModelState.IsValid)
            {
                await _serviceRepository.CreateAsync(service);
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            var service = await _serviceRepository.GetByIdAsync(id.Value);

            if (service == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service)
        {
            if (id != service.Id)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceRepository.UpdateAsync(service);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _serviceRepository.ExistAsync(service.Id))
                    {
                        return new NotFoundViewResult("ServiceNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            var service = await _serviceRepository.GetByIdAsync(id.Value);

            if (service == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            await _serviceRepository.DeleteAsync(service);
            return RedirectToAction(nameof(Index));
        }
    }

}

