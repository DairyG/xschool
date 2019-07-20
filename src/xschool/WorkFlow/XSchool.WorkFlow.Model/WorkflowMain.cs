using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Model
{
    /// <summary>
    /// 工作流业务主表
    /// </summary>
    public class WorkflowMain : IModel<int>
    {

        /// <summary>
        /// 
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 流程Id
        /// </summary>		
        public int SubjectId { get; set; }

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
        /// 表单内容
        /// </summary>
        public string FormContent { get; set; }

        /// <summary>
        /// 表单属性
        /// </summary>
        public string FormAttribute { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        public int CompanyId { get; set; }
    }
}