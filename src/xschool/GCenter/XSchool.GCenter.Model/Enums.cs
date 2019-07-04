using System.ComponentModel;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 常用状态，[有效，无效]
    /// </summary>
    public enum NomalStatus
    {
        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]
        Invalid = 0,

        /// <summary>
        /// 有效
        /// </summary>
        [Description("有效")]
        Valid = 1,
    }

    /// <summary>
    /// 禁启状态，[禁用，启用]
    /// </summary>
    public enum EDStatus
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 1,

        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 2,
    }

    /// <summary>
    /// 基础信息类型
    /// </summary>
    public enum BasicInfoType
    {
        /// <summary>
        /// 到岗时间
        /// </summary>
        [Description("到岗时间")]
        WorkerInField = 1,

        /// <summary>
        /// 面试方式
        /// </summary>
        [Description("面试方式")]
        InterviewMethod = 2,

        /// <summary>
        /// 学历
        /// </summary>
        [Description("学历")]
        Education = 3,

        /// <summary>
        /// 教育性质
        /// </summary>
        [Description("教育性质")]
        Properties = 4,

        /// <summary>
        /// 社会关系
        /// </summary>
        [Description("社会关系")]
        SocialRelations = 5,

        /// <summary>
        /// 招聘来源
        /// </summary>
        [Description("招聘来源")]
        RecruitmentSource = 6,

        /// <summary>
        /// 合同性质
        /// </summary>
        [Description("合同性质")]
        ContractNature = 7,

        /// <summary>
        /// 工资类别
        /// </summary>
        [Description("工资类别")]
        WagesType = 8,

        /// <summary>
        /// 保险类别
        /// </summary>
        [Description("保险类别")]
        InsuranceType = 9,
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Woman = 2,
    }

    /// <summary>
    /// 是否为系统数据
    /// </summary>
    public enum IsSystem
    {
        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        Yes = 1,

        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        No = 0,
    }

    /// <summary>
    /// 加or减
    /// </summary>
    public enum AddSubtraction
    {
        /// <summary>
        /// 增加
        /// </summary>
        [Description("增加")]
        Add = 1,

        /// <summary>
        /// 扣除
        /// </summary>
        [Description("扣除")]
        Subtraction = 2,
    }

    /// <summary>
    /// 预算管理
    /// </summary>
    public enum BudgetType
    {
        /// <summary>
        /// 建设成本
        /// </summary>
        [Description("建设成本")]
        Construction = 1,
        /// <summary>
        /// 费用预算
        /// </summary>
        [Description("费用预算")]
        CostBudget = 2,
        /// <summary>
        /// 固定资产
        /// </summary>
        [Description("固定资产")]
        FixedAssets = 3,
    }

    /// <summary>
    /// 简历状态
    /// </summary>
    public enum ResumeStatus
    {
        /// <summary>
        /// 有效
        /// </summary>
        [Description("有效")]
        Effective = 1,
        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]
        Invalid = 2,
    }
    /// <summary>
    /// 面试状态
    /// </summary>
    public enum InterviewStatus
    {
        /// <summary>
        /// 未面试
        /// </summary>
        [Description("未面试")]
        NoInterview = 1,
        /// <summary>
        /// 面试中
        /// </summary>
        [Description("面试中")]
        InterviewIng = 2,
        /// <summary>
        /// 已通过
        /// </summary>
        [Description("已通过")]
        IsPass = 3,
        /// <summary>
        /// 未通过
        /// </summary>
        [Description("未通过")]
        NoPass = 4,
    }

    /// <summary>
    /// [绩效考核] 考核类型
    /// </summary>
    public enum KpiType
    {
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        Dept = 1,
        /// <summary>
        /// 人员
        /// </summary>
        [Description("人员")]
        User = 2,
    }
    /// <summary>
    /// [绩效考核] 考核对象类型
    /// </summary>
    public enum KpiObjectType
    {
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门负责人")]
        DeptHead = 1,
        /// <summary>
        /// 人员
        /// </summary>
        [Description("人员")]
        User = 2,
    }

    /// <summary>
    /// [绩效考核] 审核对象类型
    /// </summary>
    public enum KpiAuditObjectType
    {
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门负责人")]
        DeptHead = 1,
        /// <summary>
        /// 部门
        /// </summary>
        [Description("上级部门负责人")]
        LeaderDeptHead = 2,
        /// <summary>
        /// 人员
        /// </summary>
        [Description("人员")]
        Others = 3,
    }

    /// <summary>
    /// [绩效考核] 考核方案
    /// </summary>
    public enum KpiPlan
    {
        /// <summary>
        /// 月度考核方案
        /// </summary>
        [Description("月度考核方案")]
        Monthly = 1,
        /// <summary>
        /// 季度考核方案
        /// </summary>
        [Description("季度考核方案")]
        Quarter = 2,
        /// <summary>
        /// 半年考核方案
        /// </summary>
        [Description("半年考核方案")]
        HalfYear = 3,
        /// <summary>
        /// 年度考核方案
        /// </summary>
        [Description("年度考核方案")]
        Annual = 4,
    }

    /// <summary>
    ///  时间段（周报【第一周，第二周...】；季度报【第一季度，第二季度...】；年报【上半年，下半年】）
    /// </summary>
    public enum SummaryIndex
    {
        /// <summary>
        /// 第一周
        /// </summary>
        [Description("第一周")]
        FirstWeek = 1,
        /// <summary>
        /// 第二周
        /// </summary>
        [Description("第二周")]
        SecondWeek = 2,
        /// <summary>
        /// 第三周
        /// </summary>
        [Description("第三周")]
        ThirdWeek = 3,
        /// <summary>
        /// 第四周
        /// </summary>
        [Description("第四周")]
        FourthWeek = 4,
        /// <summary>
        /// 第五周
        /// </summary>
        [Description("第五周")]
        FifthWeek = 5,
        //---------------------------------------------------------
        /// <summary>
        /// 第一季度
        /// </summary>
        [Description("第一季度")]
        FirstQuarter = 6,
        /// <summary>
        /// 第二季度
        /// </summary>
        [Description("第二季度")]
        SecondQuarter = 7,
        /// <summary>
        /// 第三季度
        /// </summary>
        [Description("第三季度")]
        ThirdQuarter = 8,
        /// <summary>
        /// 第四季度
        /// </summary>
        [Description("第四季度")]
        FourthQuarter = 9,
        //--------------------------------------------------------
        /// <summary>
        /// 上半年
        /// </summary>
        [Description("上半年")]
        FirstHalf = 10,
        /// <summary>
        /// 下半年
        /// </summary>
        [Description("下半年")]
        SecondHalf = 11,
    }
    /// <summary>
    /// 总结的类型
    /// </summary>
    public enum SummaryType
    {
        /// <summary>
        /// 日报
        /// </summary>
        [Description("日报")]
        Daily = 1,
        /// <summary>
        /// 周报
        /// </summary>
        [Description("周报")]
        Week = 2,
        /// <summary>
        /// 月报
        /// </summary>
        [Description("月报")]
        Month = 3,
        /// <summary>
        /// 季度报
        /// </summary>
        [Description("季度报")]
        Quarter = 4,
        /// <summary>
        /// 半年报
        /// </summary>
        [Description("半年报")]
        Half = 5,
        /// <summary>
        /// 年报
        /// </summary>
        [Description("年报")]
        Year = 6,
    }
}
