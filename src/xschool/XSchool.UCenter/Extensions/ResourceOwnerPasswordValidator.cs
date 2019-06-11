using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
            //_signManager.ExternalLoginSignInAsync()
            var result = await _signManager.PasswordSignInAsync(context.UserName, context.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,string.Empty),
                        new Claim(ClaimTypes.Name, context.UserName),
                        //new Claim(nameof(user.DisplayName), user.DisplayName),
                    };
                context.Result = new GrantValidationResult(context.UserName, OidcConstants.AuthenticationMethods.Password, claims);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "用户登上失败");
            }
        }
    }
}
