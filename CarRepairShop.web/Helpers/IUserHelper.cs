using CarRepairShop.web.Data.Entities;
using CarRepairShop.web.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CarRepairShop.web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}
