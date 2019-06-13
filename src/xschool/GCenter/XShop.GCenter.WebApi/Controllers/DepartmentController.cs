using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using XSchool.Core;
using XShop.GCenter.Businesses;
using XShop.GCenter.Model;
using XSchool.Query.Pageing;
using System.ComponentModel.DataAnnotations;

namespace XShop.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class DepartmentController : ApiBaseController
    {
        private readonly DepartmentBusiness _business;
        public DepartmentController(DepartmentBusiness business)
        {
            this._business = business;
        }
        [HttpPost]
        [Description("添加部门")]
        public Result Add([FromForm]Department department)
        {
            return _business.Add(department);
        }
    }
}