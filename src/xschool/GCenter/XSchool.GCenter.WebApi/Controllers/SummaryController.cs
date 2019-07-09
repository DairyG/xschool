using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Model;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class SummaryController : ApiBaseController
    {
        private readonly SummaryBusiness _summaryBusiness;
        public SummaryController(SummaryBusiness summaryBusiness)
        {
            _summaryBusiness = summaryBusiness;
        }
        /// <summary>
        /// 根据类型查询所有总结
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public IList<Summary> Get(int type)
        {
            return _summaryBusiness.Get(type);
        }
        /// <summary>
        /// [添加] 总结
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromForm]Summary model)
        {
            return _summaryBusiness.Add(model);
        }
    }
}