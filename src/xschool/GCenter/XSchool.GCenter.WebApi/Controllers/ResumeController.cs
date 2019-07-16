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
    public class ResumeController : ApiBaseController
    {
        private readonly ResumeBusiness _resumeBusiness;
        public class Search
        {
            public string Name { get; set; }
            public string Phone { get; set; }
            public int State { get; set; }
        };
        public ResumeController(ResumeBusiness resumeBusiness)
        {
            _resumeBusiness = resumeBusiness;
        }

        /// <summary>
        /// [列表] 简历
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <returns></returns>
        [HttpPost]
        public object Get([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]Search search)
        {
            var condition = new Condition<Resume>();
            if (!string.IsNullOrWhiteSpace(search.Name) && !string.IsNullOrWhiteSpace(search.Phone))
            {
                if (search.State != 0)
                {
                    condition.And(p => p.Status == ResumeStatus.Effective && p.UserName.Contains(search.Name) && p.LinkPhone.Contains(search.Phone) && p.InterviewStatus == (InterviewStatus)search.State);
                }
                else
                {
                    condition.And(p => p.Status == ResumeStatus.Effective && p.UserName.Contains(search.Name) && p.LinkPhone.Contains(search.Phone));
                }
            }
            else if (!string.IsNullOrWhiteSpace(search.Name))
            {
                if (search.State != 0)
                {
                    condition.And(p => p.Status == ResumeStatus.Effective && p.UserName.Contains(search.Name) && p.InterviewStatus == (InterviewStatus)search.State);
                }
                else
                {
                    condition.And(p => p.Status == ResumeStatus.Effective && p.UserName.Contains(search.Name));
                }
            }
            else if (!string.IsNullOrWhiteSpace(search.Phone))
            {
                if (search.State != 0)
                {
                    condition.And(p => p.Status == ResumeStatus.Effective && p.LinkPhone.Contains(search.Phone) && p.InterviewStatus == (InterviewStatus)search.State);
                }
                else
                {
                    condition.And(p => p.Status == ResumeStatus.Effective && p.LinkPhone.Contains(search.Phone));
                }
            }
            else
            {
                if (search.State != 0)
                {
                    condition.And(p => p.Status == ResumeStatus.Effective && p.InterviewStatus == (InterviewStatus)search.State);
                }
                else
                {
                    condition.And(p => p.Status == ResumeStatus.Effective);
                }
            }
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
                {
                    new KeyValuePair<string, OrderBy>("Id", OrderBy.Desc)
                };
            return _resumeBusiness.Page(page, limit, condition.Combine(), p => new
            {
                p.Id,
                p.UserName,
                p.Age,
                p.LinkPhone,
                p.JobCandidates,
                p.JobYears,
                p.ExpectSalary,
                p.ArrivalTime,
                p.InterviewStatus
            }, order);

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
        public int Delete([FromForm]Resume model, [FromForm]int id)
        {
            return _resumeBusiness.Delete(model, id);
        }
        /// <summary>
        /// 根据面试状态查询简历
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">条数</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        [HttpPost]
        public IPageCollection<Resume> GetListByInterviewStatus([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            return _resumeBusiness.GetListByInterviewStatus(page, limit, InterviewStatus.IsPass);
        }

        /// <summary>
        /// 根据ID修改Resume的面试状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateInterviewStatus(Resume model, int id, InterviewStatus state)
        {
            return _resumeBusiness.UpdateInterviewStatus(model, id, state);
        }


    }
}