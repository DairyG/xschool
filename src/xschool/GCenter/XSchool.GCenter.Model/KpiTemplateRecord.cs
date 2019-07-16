using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class KpiTemplateRecord : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 考核类型，1-部门，2-人员
        /// </summary>		
        public KpiType KpiType { get; set; }

        /// <summary>
        /// 所属考核方案Id
        /// </summary>		
        public KpiPlan KpiId { get; set; }

        /// <summary>
        /// 所属考核方案
        /// </summary>		
        public string KpiName { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>		
        public int CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>		
        public string CompanyName { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>		
        public int DptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>		
        public string DptName { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>		
        public int EmployeeId { get; set; }

        /// <summary>
        /// 人员名称
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 考核模板明细
        /// </summary>
        [NotMapped]
        public IList<KpiTemplateDetail> TemplateDetail { get; set; } = new List<KpiTemplateDetail>();
        /// <summary>
        /// 考核模板审核记录
        /// </summary>
        [NotMapped]
        public IList<KpiTemplateAuditRecord> TemplateAuditRecord { get; set; } = new List<KpiTemplateAuditRecord>();
    }
}
