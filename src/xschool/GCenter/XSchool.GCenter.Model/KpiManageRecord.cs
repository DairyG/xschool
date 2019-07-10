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

        /// <summary>
        /// 考核类型，1-部门，2-人员
        /// </summary>
        public KpiType KpiType { get; set; }

        /// <summary>
        /// 所属考核方案Id
        /// </summary>
        public int KpiId { get; set; }

        /// <summary>
        /// 所属考核方案
        /// </summary>
        public string KpiName { get; set; }

        /// <summary>
        /// 考核对象类型，1-部门负责人，2-人员
        /// </summary>
        public KpiObjectType ObjectType { get; set; }

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
        /// 人员名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 考核时间
        /// </summary>
        public string KpiDate { get; set; }

        /// <summary>
        /// 考核分数
        /// </summary>
        public decimal? Score { get; set; }

        /// <summary>
        /// 当前步骤，[0-自评，1-初审，2-终审，-1-完成]
        /// </summary>
        public KpiSteps Steps { get; set; }

        /// <summary>
        /// 当前步骤操作人Id
        /// </summary>
        public int? StepsAuditId { get; set; }

        /// <summary>
        /// 当前步骤操作人姓名
        /// </summary>
        public string StepsAuditName { get; set; }

        /// <summary>
        /// 自我评价
        /// </summary>
        public string ZeroEvaluation { get; set; }

        /// <summary>
        /// 初审人Id
        /// </summary>
        public int? OneAuditId { get; set; }

        /// <summary>
        /// 初审人姓名
        /// </summary>
        public string OneAuditName { get; set; }

        /// <summary>
        /// 初审人评价
        /// </summary>
        public string OneAuditEvaluation { get; set; }

        /// <summary>
        /// 终审人Id
        /// </summary>
        public int? TwoAuditId { get; set; }

        /// <summary>
        /// 终审人姓名
        /// </summary>
        public string TwoAuditName { get; set; }

        /// <summary>
        /// 终审人评价
        /// </summary>
        public string TwoAuditEvaluation { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// 考核完成日期
        /// </summary>
        public DateTime? CompleteDate { get; set; }

        /// <summary>
        /// 状态，0-自评，1-审批中，2-完成，-1-无效
        /// </summary>
        public KpiStatus Status { get; set; } = KpiStatus.Zero;

    }
}

