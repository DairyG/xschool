using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 考核管理记录
    /// </summary>
    public partial class KpiManageRecord : IModel<int>
    {
        public int Id { get; set; }

        // <summary>
        /// 考核模板记录Id
        /// </summary>		
        public int KpiTemplateRecordId { get; set; }

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
        /// 考核时间
        /// </summary>		
        public string KpiDate { get; set; }

        /// <summary>
        /// 当前步骤，[10=自评，11=初审，12=终审，1=完成]
        /// </summary>		
        public KpiSteps Steps { get; set; }

        /// <summary>
        /// 当前步骤操作人公司Id
        /// </summary>		
        public int? StepsCompanyId { get; set; }

        /// <summary>
        /// 当前步骤操作人公司名称
        /// </summary>		
        public string StepsCompanyName { get; set; }

        /// <summary>
        /// 当前步骤操作人部门Id
        /// </summary>		
        public int? StepsDptId { get; set; }

        /// <summary>
        /// 当前步骤操作人部门名称
        /// </summary>		
        public string StepsDptName { get; set; }

        /// <summary>
        /// 当前步骤操作人Id
        /// </summary>		
        public int? StepsEmployeeId { get; set; }

        /// <summary>
        /// 当前步骤操作人姓名
        /// </summary>		
        public string StepsUserName { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>		
        public DateTime? AddDate { get; set; }

        /// <summary>
        /// 考核完成日期
        /// </summary>		
        public DateTime? CompleteDate { get; set; }

        /// <summary>
        /// 状态，[10=考核，11=审批中，1=完成，-1=无效]
        /// </summary>		
        public KpiStatus Status { get; set; }
    }
}

