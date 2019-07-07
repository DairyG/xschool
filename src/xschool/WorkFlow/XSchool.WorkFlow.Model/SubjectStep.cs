using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Model
{

    /// <summary>
    /// 科目管理节点表
    /// </summary>
    public class SubjectStep : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 流程id
        /// </summary>
        public int SubjectId{ get; set; }
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
        /// 流程主表
        /// </summary>
        public Subject SubjectObj { get; set; }

        /// <summary>
        /// 流程节点人员表
        /// </summary>
        public ICollection<SubjectRule> SubjectRulePassList { get; set; }
    }
}
