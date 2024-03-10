using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Customization.Identity.ErrorDescriber
{
    // This page is for error messages. We can override existing messages and create new ones ...
    public class LocalizationErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            //return new() { Code = "NewDigitError", Description = "Please Enter Digits!" };
            return base.PasswordRequiresDigit();
        }

        public override IdentityError PasswordRequiresLower()
        {
            //return new() { Code = "NewLowerLetters", Description = "Please Use Lower Letters!" };
            return base.PasswordRequiresLower();
        }

        public override IdentityError PasswordTooShort(int length)
        {
            //return new() { Code = "NewTooShortError", Description = "Your Password Is Too Short!" };
            return base.PasswordTooShort(length);
        }
    }
}
