using CarRepairShop.web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace CarRepairShop.web.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(
            DataContext context
            )
        {
            _context = context;
        }


        [Authorize(Roles = "Admin")]
        // GET: Users
        public IActionResult Index()
        {
            //return View(_vehicleRepository.GetAll().OrderBy(b => b.CarPlate));
            var users = _context.Users.ToList(); // Obter todos os usuários e converter em uma lista
            return View(users);
        }
    }
}
