using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 考核管理明细
    /// </summary>
    public class KpiManageDetail : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
		/// 考核管理记录Id
		/// </summary>		
		public int KpiManageRecordId { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>		
        public int CompanyId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>		
        public int DptId { get; set; }

        /// <summary>
        /// 人员Id
        /// </summary>		
        public int EmployeeId { get; set; }

        /// <summary>
        /// 考核项目Id
        /// </summary>		
        public int EvaluationId { get; set; }

        /// <summary>
        /// 考核项目名称
        /// </summary>		
        public string EvaluationName { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>		
        public string EvaluationType { get; set; }

        /// <summary>
        /// 权重
        /// </summary>		
        public int Weight { get; set; }

        /// <summary>
        /// 说明
        /// </summary>		
        public string Explain { get; set; }

        /// <summary>
        /// 年份
        /// </summary>		
        public int Year { get; set; }

        /// <summary>
        /// 考核时间
        /// </summary>		
        public string KpiDate { get; set; }

        /// <summary>
        /// 自评分
        /// </summary>		
        public decimal? SelfScore { get; set; }

        /// <summary>
        /// 初审分
        /// </summary>		
        public decimal? OneScore { get; set; }

        /// <summary>
        /// 终审分
        /// </summary>		
        public decimal? TwoScore { get; set; }

    }
}

