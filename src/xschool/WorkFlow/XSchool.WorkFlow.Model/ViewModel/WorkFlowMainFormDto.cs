using System;
using System.Collections.Generic;
using System.Text;

namespace XSchool.WorkFlow.Model.ViewModel
{
    /// <summary>
    /// 工作流事务性固定模板表单展示
    /// </summary>
    public class WorkFlowMainFormDto
    {
        /// <summary>
        /// 流程id
        /// </summary>		
        public int SubjectId { get; set; }
        /// <summary>
        /// 业务流程名称
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 表单内容
        /// </summary>
        public string FormContent { get; set; }

        /// <summary>
        /// 表单属性
        /// </summary>
        public string FormAttribute { get; set; }
        /// <summary>
        /// 流程审核节点
        /// </summary>
        public IList<SubjectStep> SubjectPassList { get; set; }
    }
    public class WorkflowMainTestDto: WorkflowMain
    {
        /// <summary>
        /// 表单内容
        /// </summary>
        public string DepName { get; set; }

        /// <summary>
        /// 表单属性
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 表单属性
        /// </summary>
        public string CompanyName { get; set; }

    }
}
