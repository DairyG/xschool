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

namespace XSchool.UCenter.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class UserController: ControllerBase
    {
        private readonly MSLogger _logger;
        private readonly UserManager<User> _userManager;
        public UserController(UserManager<User> userManager,ILogger<UserController> logger)
        {
            this._userManager = userManager;
            this._logger = logger;
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

        
        
    }
}
