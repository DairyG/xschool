using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Model;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class SummaryReplyController : ApiBaseController
    {
        private readonly SummaryReplyBusiness _summaryReplyBusiness;
        public SummaryReplyController(SummaryReplyBusiness business)
        {
            this._summaryReplyBusiness = business;
        }
        /// <summary>
        /// 根据报告ID获取评论
        /// </summary>
        /// <param name="summaryId"></param>
        /// <returns></returns>
        [HttpGet("{summaryId}")]
        public IList<SummaryReply> Get(int summaryId)
        {
            return _summaryReplyBusiness.Get(summaryId);
        }
        /// <summary>
        /// [添加] 评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromForm]SummaryReply model)
        {
            return _summaryReplyBusiness.Add(model);
        }
    }
}