using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.Model
{
    /// <summary>
    /// 流程组别
    /// </summary>
    public class SubjectType : IModel<int>
    { 
        public int Id { get; set; }

        /// <summary>
        /// 业务组别名称
        /// </summary>
        public string SubjectTypeName { get; set; }
        
        /// <summary>
        /// 启用状态（枚举）
        /// </summary>
        public EDStatus Status { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 流程表
        /// </summary>
        public ICollection<Subject> SubjectList { get; set; }
    }
}
