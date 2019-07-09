using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XSchool.WorkFlow.Model;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Model.ViewModel
{
    /// <summary>
    /// 流程主表
    /// </summary>
    public class SubjectDto
    {
        /// <summary>
        /// 流程主表Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 业务流程名称
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 流程类型id
        /// </summary>
        public FlowType FlowTypeId { get; set; }
        /// <summary>
        /// 业务组别id
        /// </summary>
        public int SubjectTypeId { get; set; }
        /// <summary>
        /// true 必填审批意见，false 不必填
        /// </summary>
        public bool PassInfo { get; set; }
        /// <summary>
        /// 图标URL
        /// </summary>
        public string IcoUrl { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 表单内容
        /// </summary>
        public string FormContent { get; set; }

        /// <summary>
        /// 表单属性
        /// </summary>
        public string FormAttribute { get; set; }
        /// <summary>
        /// 启用状态（枚举）
        /// </summary>
        public EDStatus Status { get; set; }
        /// <summary>
        /// 流程可见范围
        /// </summary>
        public List<SubjectRuleDto> SubjectRuleRangeList { get; set; }
        /// <summary>
        /// 流程节点
        /// </summary>
        public List<SubjectStepDto> SubjectStepFlowList { get; set; }
    }
}
