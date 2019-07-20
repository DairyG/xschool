using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using XSchool.WorkFlow.WebApi.Helper;

namespace XSchool.WorkFlow.WebApi.Controllers
{
    [XFilterFilter]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected TokenUser UToken
        {
            get
            {
                return this.HttpContext.Items["TOKEN_USER"] as TokenUser;
            }
        }

        private Employee _employee;
        protected Employee Emplolyee
        {
            get
            {
                if (_employee == null)
                {
                    var task = RemoteRequestHelper.GetEmployeeByUserIdAsync(this.UToken.Id);
                    task.Wait();
                    _employee = task.Result;
                }
                return _employee;
            }
        }
    }

    public class XFilterFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authorizaToken = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrWhiteSpace(authorizaToken))
            {
                authorizaToken = authorizaToken.Replace("Bearer ", string.Empty);
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(authorizaToken);
                var identity = context.HttpContext.User.Identity as ClaimsIdentity;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,jwt.Payload[ClaimTypes.NameIdentifier].ToString()),
                    new Claim(ClaimTypes.Name,jwt.Payload[ClaimTypes.Name].ToString()),
                    new Claim(ClaimTypes.MobilePhone,jwt.Payload[ClaimTypes.MobilePhone].ToString())
                };

                foreach (var claim in claims)
                {
                    identity.TryRemoveClaim(claim);
                    identity.AddClaim(claim);
                }

                TokenUser user = new TokenUser
                {
                    Id = Convert.ToInt32(jwt.Payload[ClaimTypes.NameIdentifier]),
                    UserName = jwt.Payload[ClaimTypes.Name].ToString(),
                    DisplayName = jwt.Payload["displayName"].ToString(),
                    IdCard = jwt.Payload["idcard"].ToString(),
                    Mobile = jwt.Payload[ClaimTypes.MobilePhone].ToString()
                };

                context.HttpContext.Items["TOKEN_USER"] = user;
            }
            base.OnActionExecuting(context);
        }
    }

    public class TokenUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string Mobile { get; set; }

        public string IdCard { get; set; }
    }
}
