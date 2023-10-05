using CarRepairShop.web.Data;
using CarRepairShop.web.Helpers;
using CarRepairShop.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShop.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly ICountryRepository _countryRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;

        public UsersController(
            DataContext context,
            ICountryRepository countryRepository,
            IUserHelper userHelper,
            IBlobHelper blobHelper
            )
        {
            _context = context;
            _countryRepository = countryRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
        }

        // GET: Users
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return new NotFoundViewResult("UserNotFound");
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);

            try
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{user.UserName} provavelmente está a ser usado!";
                    ViewBag.ErrorMessage = $"{user.UserName} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
                        $"Experimente primeiro apagar tdas os serviços que o estão a usar" +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }
        }

        public async Task<IActionResult> ChangeUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;
                model.ImageId = user.ImageId;

                var city = await _countryRepository.GetCityAsync(user.CityId);

                if (city != null)
                {
                    var country = await _countryRepository.GetCountryAsync(city);

                    if (country != null)
                    {
                        model.CountryId = country.Id;
                        model.Cities = _countryRepository.GetComboCities(country.Id);
                        model.Countries = _countryRepository.GetComboCountries();
                        model.CityId = user.CityId;
                        model.Role = user.Role;
                    }
                }
            }

            model.Cities = _countryRepository.GetComboCities(model.CountryId);
            model.Countries = _countryRepository.GetComboCountries();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.Id);

                if (user != null)
                {

                    Guid imageId = Guid.Empty;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                    }

                    var city = await _countryRepository.GetCityAsync(model.CityId);

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Address = model.Address;
                    user.PhoneNumber = model.PhoneNumber;
                    user.CityId = model.CityId;
                    user.City = city;
                    user.Role = model.Role;
                    user.ImageId = imageId;
                    

                    var response = await _userHelper.UpdateUserAsync(user);

                    if (response.Succeeded)
                    {
                        ViewBag.UserMessage = "User Updated!";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
            }

            return View(model);
        }

    }
}
