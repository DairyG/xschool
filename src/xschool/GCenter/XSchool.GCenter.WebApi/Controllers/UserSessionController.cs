using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.GCenter.Model.ViewModel;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class UserSessionController : ApiBaseController
    {
        private readonly AuthStrategyWrapper _wrappers;
        public UserSessionController(AuthStrategyWrapper wrappers)
        {
            _wrappers = wrappers;
        }

        /// <summary>
        /// 加载当前登录用户可访问的模块和模块元素
        /// </summary>
        /// <returns></returns>
        [HttpGet("{employeeId}")]
        public AuthStrategyDto GetAuthStrategy(int employeeId)
        {
            return _wrappers.NormalAuthStrategy(employeeId);
        }

    }
}