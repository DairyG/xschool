using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class KpiTemplateRecord : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 考核类型，1-部门，2-人员
        /// </summary>		
        public int KpiType { get; set; }

        /// <summary>
        /// 所属考核方案Id
        /// </summary>		
        public int KpiId { get; set; }

        /// <summary>
        /// 所属考核方案
        /// </summary>		
        public string KpiName { get; set; }

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
        /// 添加时间
        /// </summary>		
        public DateTime? AddDate { get; set; }

        /// <summary>
        /// 状态，0-自评，1-审批中，2-完成，-1-无效
        /// </summary>		
        public int Status { get; set; }

    }
}
