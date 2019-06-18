using Microsoft.AspNetCore.Mvc;

namespace XSchool.UCenter.Controllers
{
    [Route("[controller]/[action]")]
    public class ApiController: ControllerBase
    {
        [ActionName("verify_phone_number")]
        public IActionResult VerifyPhoneMumber(string phoneNo)
        {
            return Ok(new { VerifyToken = "123456", PhoneNo = phoneNo });
        }
    }
}
