using Microsoft.AspNetCore.Identity;
using Parcial3_CarmonaSantiago.DAL.Entities;
using Parcial3_CarmonaSantiago.Models;

namespace Parcial3_CarmonaSantiago.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        //Task<User> AddUserAsync(AddUserViewModel addUserViewModel);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        //Task<SignInResult> LoginAsync(LoginViewModel loginViewModel);

        Task LogOutAsync();
    }
}
