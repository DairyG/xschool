using System;
using System.Collections.Generic;
using System.Text;

namespace XSchool.WorkFlow.Model.ViewModel
{
    /// <summary>
    /// 流程模板列表展示
    /// </summary>
    public class subjectTypeDto
    {
        /// <summary>
        /// 流程组别id
        /// </summary>
        public int Id{get;set;}
        /// <summary>
        /// 业务组别名称
        /// </summary>
        public string SubjectTypeName { get; set; }

        public List<subjectViewDto> subjectList { get; set; }

    }
    public class subjectViewDto
    {
        /// <summary>
        /// 流程组别id
        /// </summary>
        public int subjectTypeId { get; set; }
        /// <summary>
        /// 流程id
        /// </summary>
        public int subjectId { get; set; }
        /// <summary>
        /// 业务流程名称
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 可见范围
        /// </summary>
        public List<SubjectRuleDto> SubjectRuleList { get; set; }
        
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }
    }
}
