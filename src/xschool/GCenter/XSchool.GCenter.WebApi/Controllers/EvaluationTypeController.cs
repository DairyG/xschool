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
    public class EvaluationTypeController : Controller
    {
        private readonly EvaluationTypeBusiness _business;
        private readonly BasicInfoWrapper _basicInfoWrapper;
        public EvaluationTypeController(EvaluationTypeBusiness business, BasicInfoWrapper basicInfoWrapper)
        {
            this._business = business;
            _basicInfoWrapper = basicInfoWrapper;
        }
        [HttpGet]
        [Description("获取基础数据列表")]
        public IList<EvaluationType> Get()
        {
            return _business.Get();
        }
        [HttpPost]
        [Description("添加基础数据")]
        public Result Add([FromForm]EvaluationType model)
        {
            return _business.Add(model);
        }

        [HttpPost]
        [Description("获取基础数据列表")]
        public IPageCollection<EvaluationType> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<EvaluationType>();
            condition.And(p => p.Id > 0);
            return _business.Page(page, limit, condition.Combine());
        }

    }
}