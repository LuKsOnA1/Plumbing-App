using EntityLayer.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Customization.Identity.Validators
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();

            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new()
                {
                    Code = "UsernameContainError",
                    Description = "Password can not contain Username!"
                });
            }

            if (password.StartsWith("1234"))
            {
                errors.Add(new()
                {
                    Code = "PasswordStartsDigit",
                    Description = "Password can not starts with '1234'!"
                });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
