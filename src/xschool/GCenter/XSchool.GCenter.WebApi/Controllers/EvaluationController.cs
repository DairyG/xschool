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
    public class EvaluationController : ApiBaseController
    {
        private readonly EvaluationBusiness _business;
        private readonly BasicInfoWrapper _basicInfoWrapper;
        public EvaluationController(EvaluationBusiness business, BasicInfoWrapper basicInfoWrapper)
        {
            this._business = business;
            _basicInfoWrapper = basicInfoWrapper;
        }
        [HttpPost]
        [Description("添加基础数据")]
        public Result Add([FromForm]Evaluation model)
        {
            //workerInField.Type = (BasicInfoType)Enum.Parse(typeof(BasicInfoType), workerInField.Type);
            model.Status = EDStatus.Enable;
            return _business.Add(model);
        }
        [HttpPost]
        [Description("根据Id获取基础数据")]
        public Evaluation GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        [Description("修改基础数据")]
        public Result Update([FromForm]Evaluation model)
        {
            return _business.Update(model);
        }
        [HttpPost]
        [Description("获取基础数据列表")]
        public object Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("Index", OrderBy.Asc)
            };
            var condition = new Condition<Evaluation>();
            condition.And(p => p.Id > 0);
            return _business.Page(page, limit, condition.Combine(), order);
        }

        [HttpPost]
        [Description("删除基础数据")]
        public Result Delete([FromForm]Evaluation model)
        {
            //model.Status = EDStatus.Disable;
            return _business.Update(model);
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

        /// <summary>
        /// [列表] 考核项目
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <param name="seach">筛选参数</param>
        /// <returns></returns>
        [HttpPost]
        public IPageCollection<EvaluationDto> GetList([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]EvaluationSeach seach)
        {
            return _business.Page(page, limit, seach);
        }
    }
}