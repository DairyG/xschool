using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Model;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ScheduleCompleteController : ApiBaseController
    {
        private readonly ScheduleCompleteBusiness _scheduleCompleteBusiness;
        public ScheduleCompleteController(ScheduleCompleteBusiness scheduleCompleteBusiness)
        {
            _scheduleCompleteBusiness = scheduleCompleteBusiness;
        }
        /// <summary>
        /// [添加] 日程完成
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromForm]ScheduleComplete model)
        {
            return _scheduleCompleteBusiness.Add(model);
        }
    }
}