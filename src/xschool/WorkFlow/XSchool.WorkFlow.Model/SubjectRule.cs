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
        public BusinessType BusinessType { get; set; }

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
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepName { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobDepName { get; set; }
        /// <summary>
        ///  部门岗位名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 流程主表
        /// </summary>
        public Subject SubjectObj { get; set; }

        /// <summary>
        ///流程节点表
        /// </summary>
        public SubjectStep SubjectStepObj { get; set; }


    }
}
