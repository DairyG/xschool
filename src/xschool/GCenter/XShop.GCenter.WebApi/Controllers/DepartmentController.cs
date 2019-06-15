using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Linq;
using XSchool.Core;
using XShop.GCenter.Businesses;
using XShop.GCenter.Model;
using XSchool.Query.Pageing;
using System.ComponentModel.DataAnnotations;
using XSchool.Helpers;
using System.Collections.Generic;

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
        [HttpPost]
        [Description("修改部门")]
        public Result Update([FromForm]Department department)
        {
            return _business.Update(department);
        }
        [HttpGet]
        [Description("删除部门")]
        public Result Delete([FromForm]int Id)
        {
            return _business.Delete(Id);
        }
        [HttpPost]
        [Description("根据Id获取部门信息")]
        public Department GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }

        [HttpGet]
        public Result GetTree()
        {
            return _business.Query();
        }

    }
}