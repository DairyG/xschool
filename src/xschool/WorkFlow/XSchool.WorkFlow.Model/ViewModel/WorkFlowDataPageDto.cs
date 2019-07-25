using System;
using System.Collections.Generic;
using System.Text;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Model.ViewModel
{
    /// <summary>
    /// 待我审批
    /// </summary>
    public class WorkFlowDataPageDto
    {
        /// <summary>
        /// WorkflowMainId
        /// </summary>		
        public int Id { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>		
        public string SubjectName { get; set; }

        /// <summary>
        /// 审批状态 
        /// </summary>		
        public PassStatus PassStatus { get; set; }

        /// <summary>
        /// 流程编号=流程组别_时间_随机数
        /// </summary>		
        public string BusinessCode { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime Createtime { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public int CreateUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>		
        public string CreateUserName { get; set; }

        /// <summary>
        /// 申请部门Id
        /// </summary>		
        public int DeptId { get; set; }


        /// <summary>
        /// 待审核人Id
        /// </summary>		
        public int WaitApprovalId { get; set; }
        /// <summary>
        /// 待审核人
        /// </summary>		
        public string WaitApprovalName { get; set; }

        /// <summary>
        /// 历史审批人信息
        /// </summary>		
        public string HistoryApprovalNames { get; set; }

    }
}
