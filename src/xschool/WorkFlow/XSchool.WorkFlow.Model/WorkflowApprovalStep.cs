using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Model
{
    /// <summary>
    ///  工作流审核节点表
    /// </summary>
    public class WorkflowApprovalStep : IModel<int>
    {

        /// <summary>
        /// 
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 工作流业务主键Id
        /// </summary>		
        public int WorkflowBusinessId { get; set; }

        /// <summary>
        ///  审批节点
        /// </summary>		
        public int PassNo { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>		
        public string PassName { get; set; }

        /// <summary>
        /// 是否终点:false非终点 true终点
        /// </summary>
        public bool IsEnd { get; set; }
        /// <summary>
        /// 是否会签（true：会签，false：或签）
        /// </summary>
        public bool IsCountersign { get; set; }

        /// <summary>
        /// 节点类型  1 审核节点，2 是抄送节点，3 是复盘节点（复盘节点时候发起人存userId，部门经理存DepId）
        /// </summary>
        public PassType PassType { get; set; }

        /// <summary>
        /// true 当前节点是审核节点，反之
        /// </summary>
        public bool AuditNo { get; set; }
        /// <summary>
        /// 工作流业务主表
        /// </summary>
        public WorkflowMain WorkflowMain { get; set; }
        /// <summary>
        /// 节点对应审核记录
        /// </summary>

        public virtual ICollection<WorkflowApprovalRecords> workflowApprovalRecordList { get; set; }


}
}
