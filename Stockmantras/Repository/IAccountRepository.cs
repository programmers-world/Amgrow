using Microsoft.AspNetCore.Identity;
using Stockmantras.Models;
using System.Threading.Tasks;

namespace Stockmantras.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUser user, string role);
        Task<SignInResult> PasswordSignInAsync(LoginModel lmodel);
        Task SignOutAsync();

    }
}