using System.Collections.Generic;

namespace XSchool.GCenter.Model.ViewModel
{
    /// <summary>
    /// 考核模板 提交
    /// </summary>
    public class KpiEvaluationSubmitDto
    {
        /// <summary>
        /// 操作模式
        /// </summary>
        public OperationMode Mode { get; set; }

        /// <summary>
        /// 所属考核方案Id
        /// </summary>		
        public KpiType KpiType { get; set; }       

        /// <summary>
        /// 所属考核方案Id
        /// </summary>		
        public KpiPlan KpiId { get; set; }

        /// <summary>
        /// 考核管理记录
        /// </summary>

        public List<KpiManageRecord> ManageRecord { get; set; } = new List<KpiManageRecord>();
        /// <summary>
        /// 考核管理明细
        /// </summary>
        public List<KpiManageDetail> ManageDetail { get; set; } = new List<KpiManageDetail>();
        /// <summary>
        /// 考核管理审核记录
        /// </summary>
        public List<KpiManageAuditRecord> ManageAuditRecord { get; set; } = new List<KpiManageAuditRecord>();
    }

}
