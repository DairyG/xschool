using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.Helpers;
using XSchool.Query.Pageing;


namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class InsuranceTypeController : ApiBaseController
    {
        private readonly WorkerInFieldSettingBusiness _business;
        private readonly BasicInfoWrapper _basicInfoWrapper;
        public InsuranceTypeController(WorkerInFieldSettingBusiness business, BasicInfoWrapper basicInfoWrapper)
        {
            this._business = business;
            _basicInfoWrapper = basicInfoWrapper;
        }
        [HttpPost]
        [Description("添加基础数据")]
        public Result Add([FromForm]WorkerInFieldSetting workerInField)
        {
            //workerInField.Type = (BasicInfoType)Enum.Parse(typeof(BasicInfoType), workerInField.Type);
            workerInField.IsSystem = IsSystem.No;
            workerInField.WorkinStatus = EDStatus.Enable;
            return _business.Add(workerInField);
        }
        [HttpPost]
        [Description("根据Id获取基础数据")]
        public WorkerInFieldSetting GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        [Description("修改基础数据")]
        public Result Update([FromForm]WorkerInFieldSetting workerInField)
        {
            return _business.Update(workerInField);
        }
        [HttpPost]
        [Description("获取基础数据列表")]
        public Result<IPageCollection<WorkerInFieldSetting>> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]string search)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("SortId", OrderBy.Asc)
            };
            var condition = new Condition<WorkerInFieldSetting>();
            condition.And(p => p.Type.Equals(Enum.Parse(typeof(BasicInfoType), search)));
            return _business.Page(page, limit, condition.Combine(), order);
        }

        [HttpPost]
        [Description("删除基础数据")]
        public Result Delete([FromForm]WorkerInFieldSetting workerInField)
        {
            return _business.Update(workerInField);
        }

        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <param name="type">类型，多个以,分割</param>
        /// <returns></returns>
        [HttpGet]
        public BasicInfoResultDto GetData(string type)
        {
            return _basicInfoWrapper.GetData(type);
        }

    }
}