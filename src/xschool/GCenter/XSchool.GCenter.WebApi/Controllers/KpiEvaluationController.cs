using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
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
        private readonly KpiTemplateDetailBusiness _tplDetailBusiness;
        private readonly KpiTemplateAuditRecordBusiness _tplAuditRecordBusiness;

        private readonly KpiManageRecordBusiness _magRecordBusiness;
        public KpiEvaluationController(KpiTemplateBusiness tplBusiness, KpiTemplateRecordBusiness tplRecordBusiness, KpiTemplateDetailBusiness tplDetailBusiness, KpiTemplateAuditRecordBusiness tplAuditRecordBusiness,
            KpiManageRecordBusiness magRecordBusiness)
        {
            _tplBusiness = tplBusiness;
            _tplRecordBusiness = tplRecordBusiness;
            _tplDetailBusiness = tplDetailBusiness;
            _tplAuditRecordBusiness = tplAuditRecordBusiness;

            _magRecordBusiness = magRecordBusiness;
        }

        /// <summary>
        /// [添加/编辑] 考核模板
        /// </summary>
        /// <param name="modelDto">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result EditTemplat([FromForm]KpiEvaluationTemplatSubmitDto modelDto)
        {
            return _tplRecordBusiness.AddOrEdit(modelDto);
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
            var model = _tplRecordBusiness.GetSingle(p => p.Id == id);
            if (model != null)
            {
                List<KeyValuePair<string, OrderBy>> orderDetail = new List<KeyValuePair<string, OrderBy>>() {
                    new KeyValuePair<string, OrderBy>("Id", OrderBy.Desc)
                };
                List<KeyValuePair<string, OrderBy>> orderAudit = new List<KeyValuePair<string, OrderBy>>() {
                    new KeyValuePair<string, OrderBy>("Steps", OrderBy.Asc)
                };
                model.TemplateDetail = _tplDetailBusiness.Query(p => p.KpiTemplateRecordId == id, p => p, orderDetail);
                model.TemplateAuditRecord = _tplAuditRecordBusiness.Query(p => p.KpiTemplateRecordId == id, p => p, orderAudit);
            }
            return model;
        }

        /// <summary>
        /// 筛选条件
        /// </summary>
        public class Search
        {
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