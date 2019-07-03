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
    public class ResumeRecordController : ApiBaseController
    {
        private readonly ResumeRecordBusiness _business;
        private readonly ResumeBusiness _resumebusiness;
        public ResumeRecordController(ResumeRecordBusiness business, ResumeBusiness resumebusiness)
        {
            this._business = business;
            this._resumebusiness = resumebusiness;
        }
        [HttpPost]
        [Description("添加基础数据")]
        public Result Add([FromForm]ResumeRecord model)
        {
            var result = _business.Add(model);
            if (result.Succeed)
            {
                _resumebusiness.UpdateInterviewStatus(new Resume(), model.ResumeId, model.InterviewStatus);
            }
            return result;
        }

        [HttpGet]
        [Description("获取基础数据列表")]
        public IList<ResumeRecord> Get(int id)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("ResumeTime", OrderBy.Desc)
            };
            return _business.Query(p=>p.ResumeId == id,p=>p, order);
        }

        [HttpGet]
        [Description("获取基础数据列表")]
        public IList<ResumeRecord> GetByInterviewStatus(int state)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("ResumeTime", OrderBy.Desc)
            };
            return _business.Query(p => p.ResumeId == state, p => p, order);
        }
    }
}