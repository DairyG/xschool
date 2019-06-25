using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ResumeController : ApiBaseController
    {
        //private readonly PersonBusinessWrapper _personWrapper;
        private readonly ResumeBusiness _resumeBusiness;
        public ResumeController(ResumeBusiness resumeBusiness)
        {
            // _personWrapper = personWrapper;
            _resumeBusiness = resumeBusiness;
        }

        /// <summary>
        /// [列表] 简历
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <returns></returns>
        [HttpPost]
        public object Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<Resume>();
            condition.And(p => p.Status == ResumeStatus.Effective);
            return _resumeBusiness.Page(page, limit, condition.Combine(), p => new
            {
                p.Id,
                p.UserName,
                p.Age,
                p.LinkPhone,
                p.JobCandidates,
                p.JobYears,
                p.ExpectSalary,
                p.ArrivalTime
            });
        }

        /// <summary>
        /// [详情] 简历
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Resume GetInfo(int id)
        {
            return _resumeBusiness.GetSingle(id);
        }

        /// <summary>
        /// [添加/编辑] 简历
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Edit([FromForm]Resume model)
        {
            return _resumeBusiness.AddOrEdit(model);
        }
        /// <summary>
        /// [删除] 简历
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public int Delete([FromForm]Resume model,[FromForm]int id)
        {
            return _resumeBusiness.Delete(model, id);
        }
    }
}