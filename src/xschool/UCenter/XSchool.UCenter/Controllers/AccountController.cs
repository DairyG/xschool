using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using XSchool.Core;
using XSchool.UCenter.Model;
using MSLogger = Microsoft.Extensions.Logging.ILogger;
using Microsoft.Extensions.Caching.Distributed;
using System;
using Microsoft.AspNetCore.Authentication;
using XSchool.UCenter.Extensions;

namespace XSchool.UCenter.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly MSLogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager _signInManager;
        private readonly IDistributedCache _cache;
        public AccountController(UserManager<User> userManager, SignInManager signInManager, ILogger<UserController> logger, IDistributedCache cache)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._cache = cache;
        }




        [HttpPost]
        [AllowAnonymous]
        [Description("注册用户")]
        public async Task<Result> Register([FromForm][Required]string account, [FromForm][Required]string password)
        {
            var user = new User
            {
                UserName = account,
                NormalizedUserName = account.ToUpper(),
                State = State.Enable
            };
            var haspassword = _userManager.PasswordHasher.HashPassword(user, password);
            var identity = await _userManager.CreateAsync(user, haspassword);
            if (identity.Succeeded)
            {
                return Result.Success(user.Id);
            }
            else
            {
                _logger.LogError("注册用户失败：{0}", string.Join(',', identity.Errors.Select(p => p.Description)));
                return Result.Fail("注册失败,请稍后重试");
            }
        }


        [HttpPost]
        [Description("用户登录")]
        public async Task<object> Login([FromForm]string account, [FromForm]string password)
        {
            
            var signResult = await _signInManager.PasswordSignInAsync(account, password, false, true);
            if (signResult.Succeeded)
            {
                
            }
            return null;
        }

    }
}
