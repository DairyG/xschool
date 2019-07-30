using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;

namespace XSchool.WorkFlow.Model
{
    /// <summary>
    /// 工作流审核记录表
    /// </summary>
    public class WorkflowApprovalRecords : IModel<int>
    {

        /// <summary>
        /// 
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 流程节点Id
        /// </summary>		
        public int WorkflowApprovalStepId { get; set; }

        /// <summary>
        /// 审批状态： -1 拒绝，1等待审批，2同意，3未接收审批
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// 读取状态：1未读 2已读（侧重用于抄送节点）
        /// </summary>		
        public int ReadStatus { get; set; }

        /// <summary>
        /// 审核人Id
        /// </summary>		
        public int AuditidUserId { get; set; }

        /// <summary>
        /// 审核人姓名
        /// </summary>		
        public string AuditidUserName { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>		
        public DateTime? AuditidTime { get; set; }

        /// <summary>
        /// 审批意见或操作说明
        /// </summary>		
        public string Memo { get; set; }

        /// <summary>
        /// 最大编号为最新记录，其他为旧的记录
        /// </summary>		
        public int OldCode { get; set; }

        /// <summary>
        /// 1 审核内容，2操作记录
        /// </summary>		
        public int DataType { get; set; }
        /// <summary>
        /// 业务流程节点表
        /// </summary>
        public virtual WorkflowApprovalStep workflowApprovalStep { get; set; }

    }
}
