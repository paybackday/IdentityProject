using IdentityProject.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProject.IdentityValidator
{
    public class CustomPasswordValidation : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (password.Length < 5)
                errors.Add(new IdentityError { Code = "PasswordLength", Description = "Please enter your password at least 5 character." });
            if (password.ToLower().Contains(user.UserName.ToLower())) // Is password include username?
                errors.Add(new IdentityError { Code = "PasswordContainsUserName", Description = "Your entered password couldn't include username" });
            if (!errors.Any()) //If errors list don't include any error, return success.
                return Task.FromResult(IdentityResult.Success);
            else
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }
    }
}
