using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XSchool.WorkFlow.Model;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.WebApi.ViewModel
{
    public class SubjectDto
    {
        /// <summary>
        /// 流程主表
        /// </summary>
        public Subject Subject { get; set; }
        /// <summary>
        /// 可见范围
        /// </summary>
        public List<SubjectRule> SubjectRuleRange { get; set; }
        /// <summary>
        /// 流程节点
        /// </summary>
        public List<SubjectStepDto> SubjectStepFlow { get; set; }
    }
}
