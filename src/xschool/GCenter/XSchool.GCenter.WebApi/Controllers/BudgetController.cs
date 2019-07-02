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
using XSchool.Query.Pageing;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class BudgetController : ApiBaseController
    {
        private readonly BudgetBusiness _business;
        private readonly BasicInfoWrapper _basicInfoWrapper;
        public BudgetController(BudgetBusiness business, BasicInfoWrapper basicInfoWrapper)
        {
            this._business = business;
            _basicInfoWrapper = basicInfoWrapper;
        }
        [HttpPost]
        [Description("添加基础数据")]
        public Result Add([FromForm]Budget budget)
        {
            budget.IsSystem = IsSystem.No;
            budget.BgStatus = EDStatus.Enable;
            return _business.Add(budget);
        }
        [HttpPost]
        [Description("根据Id获取基础数据")]
        public Budget GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        [Description("修改基础数据")]
        public Result Update([FromForm]Budget budget)
        {
            return _business.Update(budget);
        }
        [HttpPost]
        [Description("获取基础数据列表")]
        public object Get([FromForm]string search)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("SortId", OrderBy.Asc)
            };
            return _business.Query(p => p.Type.Equals(Enum.Parse(typeof(BudgetType), search)), p => new { p.Id, p.Pid, p.Name, p.SortId,p.Memo, p.BgStatus,p.LevelMap,p.Type,p.IsSystem }, order);
            //var condition = new Condition<Budget>();
            //condition.And(p => p.Type.Equals(Enum.Parse(typeof(BudgetType), search)));
            //return _business.Query(condition, p => new { p.Id, p.Pid, p.Name, p.SortId, p.BgStatus });
        }

        [HttpPost]
        [Description("删除基础数据")]
        public Result Delete([FromForm]Budget budget)
        {
            //budget.BgStatus = EDStatus.Disable;
            return _business.Update(budget);
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