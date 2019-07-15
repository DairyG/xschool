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
    public class ScheduleReplyController : ApiBaseController
    {
        private readonly ScheduleReplyBusiness _scheduleReplyBusiness;
        public ScheduleReplyController(ScheduleReplyBusiness scheduleReplyBusiness)
        {
            _scheduleReplyBusiness = scheduleReplyBusiness;
        }
        /// <summary>
        /// [添加] 日程回复
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromForm]ScheduleReply model)
        {
            Result result = Check(model);
            if (result.Succeed)
            {
                result = _scheduleReplyBusiness.Add(model);
            }
            return result;
        }
        /// <summary>
        /// 根据日程ID获取已经完成的人
        /// </summary>
        /// <param name="scheId"></param>
        /// <returns></returns>
        [HttpGet("{scheId}")]
        public IList<ScheduleReply> Get(int scheId)
        {
            return _scheduleReplyBusiness.Get(scheId);
        }

        private Result Check(ScheduleReply model)
        {
            if (model.Id > 0)
            {
                return Result.Fail("添加操作的主键必须为0");
            }
            if (model.ScheduleId <= 0)
            {
                return Result.Fail("日程数据无效");
            }
            if (model.EmployeeId <= 0)
            {
                return Result.Fail("回复人信息丢失，请重新登录");
            }
            if (string.IsNullOrWhiteSpace(model.EmployeeName))
            {
                return Result.Fail("回复人信息丢失，请重新登录");
            }
            if (string.IsNullOrWhiteSpace(model.Reply))
            {
                return Result.Fail("请输入回复内容");
            }
            return Result.Success("成功");
        }
    }
}