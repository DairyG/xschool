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
        public ResumeRecordController(ResumeRecordBusiness business)
        {
            this._business = business;
        }
        [HttpPost]
        [Description("添加基础数据")]
        public Result Add([FromForm]ResumeRecord model)
        {
            return _business.Add(model);
        }

        [HttpGet]
        [Description("获取基础数据列表")]
        public IList<ResumeRecord> Get(int id)
        {
            return _business.Query(p=>p.ResumeId == id);
        }

    }
}