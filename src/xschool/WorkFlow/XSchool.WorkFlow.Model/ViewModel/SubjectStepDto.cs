using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XSchool.WorkFlow.Model;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.WebApi.ViewModel
{
    /// <summary>
    /// 流程节点dto
    /// </summary>
    public class SubjectStepDto
    {
        /// <summary>
        /// 流程id
        /// </summary>
        public int SubjectId { get; set; }
        /// <summary>
        /// 节点编号
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
        /// 是否会签（true：会签，false：或签）费用型默认都是会签
        /// </summary>
        public bool IsCountersign { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public PassType PassType { get; set; }

        /// <summary>
        /// 节点审核人信息
        /// </summary>
        public List<SubjectRule> SubjectRulePassInfo { get; set; }
    }
}
