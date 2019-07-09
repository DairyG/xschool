using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Model
{
    /// <summary>
    /// 流程管理主表
    /// </summary>
    public class Subject : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 业务流程名称
        /// </summary>
        public string  SubjectName { get; set; }
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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 启用状态（枚举）
        /// </summary>
        public EDStatus Status { get; set; }
        /// <summary>
        /// 流程节点集合
        /// </summary>
        public ICollection<SubjectStep> SubjectStepFlowList { get; set; }
        /// <summary>
        /// 流程可视范围
        /// </summary>
        public ICollection<SubjectRule> SubjectRuleRangeList { get; set; }
    }
}
