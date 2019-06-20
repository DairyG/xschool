using Microsoft.AspNetCore.Mvc;
using System;
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
    public class BonusPenaltyController : ApiBaseController
    {
        private readonly BonusPenaltyBusiness _business;
        private readonly BasicInfoWrapper _basicInfoWrapper;
        public BonusPenaltyController(BonusPenaltyBusiness business, BasicInfoWrapper basicInfoWrapper)
        {
            this._business = business;
            _basicInfoWrapper = basicInfoWrapper;
        }
        [HttpPost]
        [Description("添加基础数据")]
        public Result Add([FromForm]BonusPenaltySetting bonusPenalty)
        {
            bonusPenalty.IsSystem = IsSystem.No;
            bonusPenalty.WorkinStatus = EDStatus.Enable;
            return _business.Add(bonusPenalty);
        }
        [HttpPost]
        [Description("根据Id获取基础数据")]
        public BonusPenaltySetting GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        [Description("修改基础数据")]
        public Result Update([FromForm]BonusPenaltySetting bonusPenalty)
        {
            return _business.Update(bonusPenalty);
        }
        [HttpPost]
        [Description("获取基础数据列表")]
        public IPageCollection<BonusPenaltySetting> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<BonusPenaltySetting>();
            condition.And(p => p.Id > 0);
            return _business.Page(page, limit, condition.Combine());
        }

        [HttpPost]
        [Description("删除基础数据")]
        public Result Delete([FromForm]BonusPenaltySetting bonusPenalty)
        {
            return _business.Update(bonusPenalty);
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