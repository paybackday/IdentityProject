using IdentityProject.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProject.IdentityValidator
{
    public class CustomUserValidation : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (int.TryParse(user.UserName[0].ToString(), out int _))
                errors.Add(new IdentityError { Code = "UserNameNumberStartWith", Description = "Username cannot start with numeric character." });
            if (user.UserName.Length < 3 && user.UserName.Length > 15)
                errors.Add(new IdentityError { Code = "UserNameLength", Description = "Username should be between 3-15 character." });
            if (user.Email.Length > 70)
                errors.Add(new IdentityError { Code = "EmailLength", Description = "Email of user can not be more than 70 character." });

            if (!errors.Any())
                return Task.FromResult(IdentityResult.Success);
            else
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }
    }
}
