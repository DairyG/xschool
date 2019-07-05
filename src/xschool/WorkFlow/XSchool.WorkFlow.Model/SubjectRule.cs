using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Model
{
    /// <summary>
    /// 节点人员表、流程可视范围人员表
    /// </summary>
    public class SubjectRule : IModel<int>
    {	
        public int Id { get; set; }

        /// <summary>
        /// 流程id
        /// </summary>		
        public int SubjectId { get; set; }

        /// <summary>
        /// 业务类型 1流程管理可见者，2流程管理节点人员
        /// </summary>		
        public int BusinessType { get; set; }

        /// <summary>
        /// 公司id
        /// </summary>		
        public int? CompanyId { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>		
        public int? DepId { get; set; }

        /// <summary>
        /// 人员id
        /// </summary>		
        public int? UserId { get; set; }

        /// <summary>
        /// 职位id
        /// </summary>		
        public int? JobDepId { get; set; }

        /// <summary>
        /// 部门岗位id
        /// </summary>		
        public int? JobId { get; set; }

    }
}
