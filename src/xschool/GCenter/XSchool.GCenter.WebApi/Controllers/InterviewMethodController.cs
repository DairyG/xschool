using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.Query.Pageing;
using XSchool.GCenter.Businesses;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class InterviewMethodController : ApiBaseController
    {
        private readonly InterviewMethodSettingBusiness _business;
        public InterviewMethodController(InterviewMethodSettingBusiness business)
        {
            this._business = business;
        }
        [HttpPost]
        [Description("添加面试方式")]
        public Result Add([FromForm]InterviewMethodSetting interviewMethodSetting)
        {
            return _business.Add(interviewMethodSetting);
        }
        [HttpPost]
        [Description("根据Id获取面试方式")]
        public InterviewMethodSetting GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        [Description("添加面试方式")]
        public Result Update([FromForm]InterviewMethodSetting interviewMethodSetting)
        {
            return _business.Update(interviewMethodSetting);
        }
        [HttpPost]
        [Description("获取面试方式列表")]
        public IPageCollection<InterviewMethodSetting> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<InterviewMethodSetting>();
            condition.And(p => p.WorkinStatus == 1);
            return _business.Page(page, limit, condition.Combine());
        }

        [HttpPost]
        [Description("添加面试方式")]
        public Result Delete([FromForm]InterviewMethodSetting interviewMethodSetting)
        {
            interviewMethodSetting.WorkinStatus = 0;
            return _business.Update(interviewMethodSetting);
        }
    }
}