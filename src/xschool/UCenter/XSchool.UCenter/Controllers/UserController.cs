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




        
        
    }
}
