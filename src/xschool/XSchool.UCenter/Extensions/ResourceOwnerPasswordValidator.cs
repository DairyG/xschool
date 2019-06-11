using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using XSchool.UCenter.Model;

namespace XSchool.UCenter.Extensions
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly SignInManager<User> _signManager;
        public ResourceOwnerPasswordValidator(SignInManager<User> signInManager)
        {
            this._signManager = signInManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var result = await _signManager.PasswordSignInAsync(context.UserName, context.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {

            }
        }
    }
}
