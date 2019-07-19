using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 考核管理审核记录
    /// </summary>
    public class KpiManageAuditRecord : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
		/// 考核管理记录Id
		/// </summary>		
		public int KpiManageRecordId { get; set; }

        /// <summary>
        /// 当前步骤
        /// </summary>		
        public KpiSteps Steps { get; set; }

        /// <summary>
        /// 评价
        /// </summary>		
        public string Evaluation { get; set; }

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

    }
}

