using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.Helpers;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.WebApi.Controllers
{
    /// <summary>
    /// 绩效考核
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class KpiEvaluationController : ApiBaseController
    {
        private readonly KpiTemplateBusiness _tplBusiness;
        private readonly KpiTemplateRecordBusiness _tplRecordBusiness;

        private readonly KpiManageTotalBusiness _magTotalBusiness;
        private readonly KpiManageRecordBusiness _magRecordBusiness;
        private readonly KpiManageDetailBusiness _magDetailBusiness;
        private readonly KpiManageAuditRecordBusiness _magAuditRecordBusiness;

        private readonly KpiEvaluationWrappers _wrappers;
        public KpiEvaluationController(
            KpiTemplateBusiness tplBusiness, KpiTemplateRecordBusiness tplRecordBusiness,
            KpiManageTotalBusiness magTotalBusiness, KpiManageRecordBusiness magRecordBusiness, KpiManageDetailBusiness magDetailBusiness, KpiManageAuditRecordBusiness magAuditRecordBusiness,
            KpiEvaluationWrappers wrappers
            )
        {
            _tplBusiness = tplBusiness;
            _tplRecordBusiness = tplRecordBusiness;

            _magTotalBusiness = magTotalBusiness;
            _magRecordBusiness = magRecordBusiness;
            _magDetailBusiness = magDetailBusiness;
            _magAuditRecordBusiness = magAuditRecordBusiness;

            _wrappers = wrappers;
        }

        /// <summary>
        /// [添加/编辑] 考核模板
        /// </summary>
        /// <param name="modelDto">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result EditTemplat([FromForm]KpiEvaluationTemplatSubmitDto modelDto)
        {
            return _wrappers.AddOrEditTemplat(modelDto);
        }

        /// <summary>
        /// [列表] 考核模板
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <param name="search">筛选条件</param>
        /// <returns></returns>
        [HttpPost]
        public IPageCollection<KpiTemplate> QueryTemplat([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]Search search)
        {
            var condition = new Condition<KpiTemplate>();
            condition.And(p => p.CompanyId == search.CompanyId);
            condition.And(p => p.KpiType == search.KpiType);
            if (search.DptId != null && search.DptId > 0)
            {
                condition.And(p => p.DptId == search.DptId);
            }
            return _tplBusiness.Page(page, limit, condition.Combine());
        }

        /// <summary>
        /// [详情] 考核模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public KpiTemplateRecord GetTemplatRecord(int id)
        {
            return _tplRecordBusiness.GetSingle(p => p.Id == id);
        }


        /// <summary>
        /// [生成] 考核记录
        /// </summary>
        /// <param name="kpiId">考核方案</param>
        /// <returns></returns>
        [HttpGet]
        public Result GeneratedManage(KpiPlan kpiId)
        {
            return _wrappers.GeneratedManage(kpiId);
        }

        /// <summary>
        /// [列表] 考核内容
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <param name="search">参数</param>
        /// <returns></returns>
        public object QueryManageDetail([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]KpiEvaluationManageQueryDto search)
        {
            return _magDetailBusiness.QueryManageDetail(page, limit, search);
        }

        /// <summary>
        /// [列表] 考核管理
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <param name="search">筛选条件</param>
        /// <returns></returns>
        public IPageCollection<KpiManageTotal> QueryManage([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]Search search)
        {
            var condition = new Condition<KpiManageTotal>();
            condition.And(p => p.CompanyId == search.CompanyId);
            condition.And(p => p.KpiType == search.KpiType);
            condition.And(p => p.Year == search.Year);
            condition.And(p => p.KpiId == search.KpiId);
            if (search.DptId != null && search.DptId > 0)
            {
                condition.And(p => p.DptId == search.DptId);
            }
            return _magTotalBusiness.Page(page, limit, condition.Combine());
        }

        /// <summary>
        /// [详情] 考核管理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public Result GetManageRecord([FromForm]KpiEvaluationManageQueryDto dto)
        {
            var model = _magRecordBusiness.GetSingle(p => p.KpiType == dto.KpiType && p.KpiId == dto.KpiId && p.CompanyId == dto.CompanyId && p.DptId == dto.DptId && p.EmployeeId == dto.EmployeeId && p.Year == dto.Year && p.KpiDate == dto.KpiDate);
            if (model != null)
            {
                var result = new KpiManageResultDto();

                List<KeyValuePair<string, OrderBy>> orderDetail = new List<KeyValuePair<string, OrderBy>>() {
                    new KeyValuePair<string, OrderBy>("Id", OrderBy.Desc)
                };
                List<KeyValuePair<string, OrderBy>> orderAudit = new List<KeyValuePair<string, OrderBy>>() {
                    new KeyValuePair<string, OrderBy>("Steps", OrderBy.Asc)
                };

                if (model.Status != KpiStatus.Complete && model.Status != KpiStatus.Invalid)
                {
                    var modelAudits = _tplRecordBusiness.GetSingle(p => p.Id == model.KpiTemplateRecordId);
                    if (modelAudits == null)
                    {
                        return Result.Fail("未查询到模板记录");
                    }
                    var lsAudits = JsonConvert.DeserializeObject<List<KpiTemplateAuditsDto>>(modelAudits.Audits).ToList();
                    if (lsAudits.Count() == 0)
                    {
                        return Result.Fail("未获取到下一步流转人");
                    }
                    if (model.Steps == KpiSteps.Zero)
                    {
                        result.Audits = lsAudits.FirstOrDefault(p => p.Steps == KpiSteps.One);
                    }
                    else if (model.Steps == KpiSteps.One)
                    {
                        result.Audits = lsAudits.FirstOrDefault(p => p.Steps == KpiSteps.Two);
                    }
                }

                result.Record = model;
                result.Detail = _magDetailBusiness.Query(p => p.KpiManageRecordId == model.Id, p => p, orderDetail).ToList();
                result.AuditRecord = _magAuditRecordBusiness.Query(p => p.KpiManageRecordId == model.Id, p => p, orderAudit).ToList();

                return Result.Success(result);
            }
            return Result.Fail("未查询到记录");
        }

        /// <summary>
        /// [考核提交] 考核管理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Result EditManage([FromForm]KpiEvaluationManageSubmitDto dto)
        {
            return _wrappers.EditManage(dto);
        }



        /// <summary>
        /// 筛选条件
        /// </summary>
        public class Search
        {
            /// <summary>
            /// 年份
            /// </summary>
            public int Year { get; set; } = DateTime.Now.Year;
            /// <summary>
            /// 考核方案
            /// </summary>
            public KpiPlan KpiId { get; set; }

            /// <summary>
            /// 考核类型
            /// </summary>
            public KpiType KpiType { get; set; }
            /// <summary>
            /// 公司Id
            /// </summary>
            public int CompanyId { get; set; }
            /// <summary>
            /// 部门Id
            /// </summary>
            public int? DptId { get; set; }
        }

    }
}