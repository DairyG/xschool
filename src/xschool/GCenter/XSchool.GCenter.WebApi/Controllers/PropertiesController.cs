using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Linq;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Model;
using XSchool.Query.Pageing;
using System.ComponentModel.DataAnnotations;
using XSchool.Helpers;
using System.Collections.Generic;

namespace XSchool.GCenter.WebApi.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class PropertiesController : ApiBaseController
    {
        private readonly PropertiesBusiness _business;
        public PropertiesController(PropertiesBusiness business)
        {
            this._business = business;
        }
        [HttpPost]
        [Description("添加教育性质")]
        public Result Add([FromForm]PropertiesSetting propertiesSetting)
        {
            return _business.Add(propertiesSetting);
        }
        [HttpPost]
        [Description("根据Id获取教育性质")]
        public PropertiesSetting GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        [Description("添加教育性质")]
        public Result Update([FromForm]PropertiesSetting propertiesSetting)
        {
            return _business.Update(propertiesSetting);
        }
        [HttpPost]
        [Description("获取教育性质列表")]
        public IPageCollection<PropertiesSetting> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<PropertiesSetting>();
            condition.And(p => p.WorkinStatus == 1);
            return _business.Page(page, limit, condition.Combine());
        }

        [HttpPost]
        [Description("删除教育性质")]
        public Result Delete([FromForm]PropertiesSetting propertiesSetting)
        {
            propertiesSetting.WorkinStatus = 0;
            return _business.Update(propertiesSetting);
        }
    }
}