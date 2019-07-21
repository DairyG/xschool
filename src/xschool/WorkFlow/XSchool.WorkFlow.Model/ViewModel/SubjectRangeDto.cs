using System;
using System.Collections.Generic;
using System.Text;

namespace XSchool.WorkFlow.Model.ViewModel
{
    /// <summary>
    /// 流程模板-修改可见范围
    /// </summary>
    public class SubjectRangeDto
    {
        /// <summary>
        /// 流程id
        /// </summary>		
        public int SubjectId { get; set; }
        /// <summary>
        /// 流程可见范围
        /// </summary>
        public List<SubjectRuleDto> SubjectRuleRangeList { get; set; }
    }
}
