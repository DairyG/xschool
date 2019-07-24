using System;
using System.Collections.Generic;

namespace XSchool.GCenter.Model.ViewModel
{
    /// <summary>
    /// 考核模板 提交
    /// </summary>
    public class KpiEvaluationTemplatSubmitDto
    {
        /// <summary>
        /// 考核类型
        /// </summary>		
        public KpiType KpiType { get; set; }

        /// <summary>
        /// 所属考核方案Id
        /// </summary>		
        public KpiPlan KpiId { get; set; }

        /// <summary>
        /// 考核模板记录
        /// </summary>
        public List<KpiTemplateRecord> TemplateRecord { get; set; } = new List<KpiTemplateRecord>();
    }

    /// <summary>
    /// 考核管理 查询
    /// </summary>
    public class KpiEvaluationManageQueryDto
    {
        /// <summary>
        /// 考核类型
        /// </summary>		
        public KpiType KpiType { get; set; }

        /// <summary>
        /// 所属考核方案Id
        /// </summary>		
        public KpiPlan KpiId { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// 考核时间
        /// </summary>
        public string KpiDate { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>		
        public int CompanyId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>		
        public int DptId { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>		
        public int EmployeeId { get; set; }
    }

    /// <summary>
    /// 考核管理 提交
    /// </summary>
    public class KpiEvaluationManageSubmitDto
    {
        /// <summary>
        /// 评价
        /// </summary>
        public string Evaluation { get; set; }

        /// <summary>
        /// 当前处理人
        /// </summary>
        public EmployeeDto Employee { get; set; }

        /// <summary>
        /// 下一步处理人
        /// </summary>
        public EmployeeDto NextEmployee { get; set; }

        /// <summary>
        /// 考核管理记录
        /// </summary>
        public KpiManageRecord ManageRecord { get; set; }

        /// <summary>
        /// 考核管理明细
        /// </summary>
        public List<KpiManageDetail> ManageDetails { get; set; }
    }

    /// <summary>
    /// 考核管理 返回
    /// </summary>
    public class KpiManageResultDto
    {
        /// <summary>
        /// 考核管理记录
        /// </summary>
        public KpiManageRecord Record { get; set; }

        /// <summary>
        /// 考核管理明细
        /// </summary>
        public List<KpiManageDetail> Detail { get; set; } = new List<KpiManageDetail>();
        /// <summary>
        /// 考核管理审核明细
        /// </summary>
        public List<KpiManageAuditRecord> AuditRecord { get; set; } = new List<KpiManageAuditRecord>();

        /// <summary>
        /// 考核模板审核人
        /// </summary>
        public KpiTemplateAuditsDto Audits { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
    }

}
