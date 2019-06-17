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
    public class WorkerInFieldController : ApiBaseController
    {
        private readonly WorkerInFieldSettingBusiness _business;
        public WorkerInFieldController(WorkerInFieldSettingBusiness business)
        {
            this._business = business;
        }
        [HttpPost]
        [Description("添加到岗时间")]
        public Result Add([FromForm]WorkerInFieldSetting workerInField)
        {
            return _business.Add(workerInField);
        }
        [HttpPost]
        [Description("根据Id获取到岗时间")]
        public WorkerInFieldSetting GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        public IPageCollection<WorkerInFieldSetting> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<WorkerInFieldSetting>();
            condition.And(p => p.Id > 0);
            return _business.Page(page, limit, condition.Combine());
        }
    }
}