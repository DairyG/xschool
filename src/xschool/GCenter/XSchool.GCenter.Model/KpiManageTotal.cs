using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 考核管理统计
    /// </summary>
    public class KpiManageTotal : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
		/// 考核类型，1-部门，2-人员
		/// </summary>		
		public KpiType KpiType { get; set; }

        /// <summary>
        /// 所属考核方案Id
        /// </summary>		
        public KpiPlan KpiId { get; set; }

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
        /// 年份
        /// </summary>		
        public int Year { get; set; }

        /// <summary>
        /// 考核分数1
        /// </summary>		
        public decimal Score1 { get; set; }

        /// <summary>
        /// 考核状态1，[-1=未开始，0=初始，10=自评，11=审批中，1=完成，-2=无效]
        /// </summary>		
        public KpiStatus Status1 { get; set; }

        /// <summary>
        /// 考核分数2
        /// </summary>		
        public decimal Score2 { get; set; }

        /// <summary>
        /// 考核状态2
        /// </summary>		
        public KpiStatus Status2 { get; set; }

        /// <summary>
        /// 考核分数3
        /// </summary>		
        public decimal Score3 { get; set; }

        /// <summary>
        /// 考核状态3
        /// </summary>		
        public KpiStatus Status3 { get; set; }

        /// <summary>
        /// 考核分数4
        /// </summary>		
        public decimal Score4 { get; set; }

        /// <summary>
        /// 考核状态4
        /// </summary>		
        public KpiStatus Status4 { get; set; }

        /// <summary>
        /// 考核分数5
        /// </summary>		
        public decimal Score5 { get; set; }

        /// <summary>
        /// 考核状态5
        /// </summary>		
        public KpiStatus Status5 { get; set; }

        /// <summary>
        /// 考核分数6
        /// </summary>		
        public decimal Score6 { get; set; }

        /// <summary>
        /// 考核状态6
        /// </summary>		
        public KpiStatus Status6 { get; set; }

        /// <summary>
        /// 考核分数7
        /// </summary>		
        public decimal Score7 { get; set; }

        /// <summary>
        /// 考核状态7
        /// </summary>		
        public KpiStatus Status7 { get; set; }

        /// <summary>
        /// 考核分数8
        /// </summary>		
        public decimal Score8 { get; set; }

        /// <summary>
        /// 考核状态8
        /// </summary>		
        public KpiStatus Status8 { get; set; }

        /// <summary>
        /// 考核分数9
        /// </summary>		
        public decimal Score9 { get; set; }

        /// <summary>
        /// 考核状态9
        /// </summary>		
        public KpiStatus Status9 { get; set; }

        /// <summary>
        /// 考核分数10
        /// </summary>		
        public decimal Score10 { get; set; }

        /// <summary>
        /// 考核状态10
        /// </summary>		
        public KpiStatus Status10 { get; set; }

        /// <summary>
        /// 考核分数11
        /// </summary>		
        public decimal Score11 { get; set; }

        /// <summary>
        /// 考核状态11
        /// </summary>		
        public KpiStatus Status11 { get; set; }

        /// <summary>
        /// 考核分数12
        /// </summary>		
        public decimal Score12 { get; set; }

        /// <summary>
        /// 考核状态12
        /// </summary>		
        public KpiStatus Status12 { get; set; }

    }
}
