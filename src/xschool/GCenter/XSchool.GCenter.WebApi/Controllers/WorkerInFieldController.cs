using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Model;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.WebApi.Controllers
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
            //workerInField.Type = (BasicInfoType)Enum.Parse(typeof(BasicInfoType), workerInField.Type);
            workerInField.IsSystem = IsSystem.No;
            return _business.Add(workerInField);
        }
        [HttpPost]
        [Description("根据Id获取到岗时间")]
        public WorkerInFieldSetting GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        [Description("添加到岗时间")]
        public Result Update([FromForm]WorkerInFieldSetting workerInField)
        {
            return _business.Update(workerInField);
        }
        [HttpPost]
        [Description("获取到岗时间列表")]
        public IPageCollection<WorkerInFieldSetting> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<WorkerInFieldSetting>();
            condition.And(p => p.WorkinStatus == 1);
            return _business.Page(page, limit, condition.Combine());
        }

        [HttpPost]
        [Description("添加到岗时间")]
        public Result Delete([FromForm]WorkerInFieldSetting workerInField)
        {
            workerInField.WorkinStatus = 0;
            return _business.Update(workerInField);
        }
    }
}