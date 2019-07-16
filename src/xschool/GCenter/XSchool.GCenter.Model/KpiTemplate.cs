using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class KpiTemplate : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 考核类型，1-部门，2-人员
        /// </summary>		
        public KpiType KpiType { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>		
        public int CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>		
        public string CompanyName { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>		
        public int DptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>		
        public string DptName { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>		
        public int EmployeeId { get; set; }

        /// <summary>
        /// 人员名称
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 月度，注：保存的是 考核管理记录Id
        /// </summary>		
        public int Monthly { get; set; }

        /// <summary>
        /// 上半年，注：保存的是 考核管理记录Id
        /// </summary>		
        public int HalfYear { get; set; }

        /// <summary>
        /// 季度，注：保存的是 考核管理记录Id
        /// </summary>		
        public int Quarter { get; set; }

        /// <summary>
        /// 年度，注：保存的是 考核管理记录Id
        /// </summary>		
        public int Annual { get; set; }

    }
}
