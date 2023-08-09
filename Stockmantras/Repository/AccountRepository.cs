using aryamoneygrow.Models;
using Microsoft.AspNetCore.Identity;
using Stockmantras.Models;
using System.Threading.Tasks;

namespace Stockmantras.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _usrManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _usrManager = userManager;
            _signinManager = signInManager;
        }



        public async Task<IdentityResult> CreateUserAsync(SignUpUser userModel, string role)
        {
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email
            };
            var result = await _usrManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                await _usrManager.AddToRoleAsync(user, role);
            }
            return result;
        }
        public async Task<SignInResult> PasswordSignInAsync(LoginModel lmodel)
        {
            var result = await _signinManager.PasswordSignInAsync(lmodel.Email, lmodel.Password, lmodel.RememberMe, false);
            return result;
        }
        public async Task SignOutAsync()
        {
            await _signinManager.SignOutAsync();
        }

    }
}
