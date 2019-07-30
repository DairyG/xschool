using System;
using System.Collections.Generic;
using System.Text;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Model.ViewModel
{
    /// <summary>
    /// 流程进度
    /// </summary>
   public  class WorkFlowStepersInfoDto
    {
        /// <summary>
        /// 节点编号
        /// </summary>
        public int PassNo { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string PassName { get; set; }
        /// <summary>
        /// true 当前节点是审核节点，反之
        /// </summary>
        public bool AuditNo { get; set; }
        /// <summary>
        /// 节点类型  1 审核节点，2 是抄送节点，3 是复盘节点
        /// </summary>
        public PassType PassType { get; set; }


        /// <summary>
        /// 节点审核人信息
        /// </summary>
        public List<WorkflowApprovalStepRecordsDto> SubjectRulePassList { get; set; }
    }

    public class WorkflowApprovalStepRecordsDto
    {
        /// <summary>
        ///  -1 拒绝，1等待审批，2同意，3未接收审批
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
    }
}
