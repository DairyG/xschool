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
        private readonly UserManager<User> _userManager;
        public ResourceOwnerPasswordValidator(SignInManager<User> signInManager,UserManager<User> userManager)
        {
            this._signManager = signInManager;
            this._userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //_signManager.ExternalLoginSignInAsync()
            var result = await _signManager.PasswordSignInAsync(context.UserName, context.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(context.UserName);
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name, context.UserName)
                    };
                context.Result = new GrantValidationResult(context.UserName, OidcConstants.AuthenticationMethods.Password, claims);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "用户登录失败");
            }
        }
    }
}
