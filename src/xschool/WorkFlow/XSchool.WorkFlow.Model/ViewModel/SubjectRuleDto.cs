using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XSchool.WorkFlow.Model.ViewModel
{
    /// <summary>
    /// 可视范围、审核节点人员表
    /// </summary>
    public class SubjectRuleDto
    {

        /// <summary>
        /// 流程id
        /// </summary>		
        public int SubjectId { get; set; }
        /// <summary>
        /// 节点id
        /// </summary>		
        public int SubjectStepId { get; set; }

        /// <summary>
        /// 1组织架构 2职位 3部门岗位
        /// </summary>		
        public int dataType { get; set; }

        /// <summary>
        /// 业务类型 1流程管理可见者，2流程管理节点人员
        /// </summary>		
        public int BusinessType { get; set; }

        /// <summary>
        /// 公司id
        /// </summary>		
        public int CompanyId { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>		
        public int DepId { get; set; }

        /// <summary>
        /// 人员id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 职位id
        /// </summary>		
        public int JobDepId { get; set; }

        /// <summary>
        /// 部门岗位id
        /// </summary>		
        public int JobId { get; set; }
    }
}
