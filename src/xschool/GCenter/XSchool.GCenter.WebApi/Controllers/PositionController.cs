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
    public class PositionController : ApiBaseController
    {
        private readonly PositionBusiness _business;
        private readonly BasicInfoWrapper _basicInfoWrapper;
        public PositionController(PositionBusiness business, BasicInfoWrapper basicInfoWrapper)
        {
            this._business = business;
            _basicInfoWrapper = basicInfoWrapper;
        }
        [HttpPost]
        [Description("添加基础数据")]
        public Result Add([FromForm]PositionSetting positionSetting)
        {
            //workerInField.Type = (BasicInfoType)Enum.Parse(typeof(BasicInfoType), workerInField.Type);
            positionSetting.IsSystem = IsSystem.No;
            positionSetting.WorkinStatus = EDStatus.Enable;
            return _business.Add(positionSetting);
        }
        [HttpPost]
        [Description("根据Id获取基础数据")]
        public PositionSetting GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        [Description("修改基础数据")]
        public Result Update([FromForm]PositionSetting positionSetting)
        {
            return _business.Update(positionSetting);
        }
        [HttpPost]
        [Description("获取基础数据列表")]
        public IPageCollection<PositionSetting> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<PositionSetting>();
            condition.And(p => p.WorkinStatus == EDStatus.Enable);
            return _business.Page(page, limit, condition.Combine());
        }

        [HttpPost]
        [Description("删除基础数据")]
        public Result Delete([FromForm]PositionSetting positionSetting)
        {
            positionSetting.WorkinStatus = 0;
            return _business.Update(positionSetting);
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