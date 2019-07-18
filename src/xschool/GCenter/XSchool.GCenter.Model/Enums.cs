using System.ComponentModel;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 操作方式
    /// </summary>
    public enum OperationMode
    {
        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Add = 1,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Edit = 2,
    }

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
    /// 性别，[男，女]
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
    /// [绩效考核] 考核方案，[月度，季度，半年，年度]
    /// </summary>
    public enum KpiPlan
    {
        /// <summary>
        /// 不选
        /// </summary>
        [Description("不选")]
        NoSel = 0,
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
    /// [绩效考核] 考核类型，[部门，人员]
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
    /// [绩效考核] 考核步骤，[10=自评，11=初审，12=终审，1=完成]
    /// </summary>
    public enum KpiSteps
    {
        /// <summary>
        /// 自评
        /// </summary>
        [Description("自评")]
        Zero = 10,
        /// <summary>
        /// 初审
        /// </summary>
        [Description("初审")]
        One = 11,
        /// <summary>
        /// 终审
        /// </summary>
        [Description("终审")]
        Two = 12,
        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Complete = 1,
    }
    /// <summary>
    /// [绩效考核] 状态，[-2=未开始，0=初始，10=自评，11=审批中，1=完成，-1=无效]
    /// </summary>
    public enum KpiStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        [Description("未开始")]
        NotStarted = -1,
        /// <summary>
        /// 初始
        /// </summary>
        [Description("初始")]
        Init = 0,
        /// <summary>
        /// 自评
        /// </summary>
        [Description("自评")]
        Zero = 10,
        /// <summary>
        /// 审批中
        /// </summary>
        [Description("审批中")]
        Audit = 11,
        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Complete = 1,
        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]
        Invalid = -2,
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
        Daily = 0,
        /// <summary>
        /// 周报
        /// </summary>
        [Description("周报")]
        Week = 1,
        /// <summary>
        /// 月报
        /// </summary>
        [Description("月报")]
        Month = 2,
        /// <summary>
        /// 季度报
        /// </summary>
        [Description("季度报")]
        Quarter = 3,
        /// <summary>
        /// 半年报
        /// </summary>
        [Description("半年报")]
        Half = 4,
        /// <summary>
        /// 年报
        /// </summary>
        [Description("年报")]
        Year = 5,
    }
    /// <summary>
    /// 是否已读
    /// </summary>
    public enum IsRead
    {
        /// <summary>
        /// 未读
        /// </summary>
        [Description("未读")]
        No = 1,
        /// <summary>
        /// 已读
        /// </summary>
        [Description("已读")]
        Yes = 2,
    }
    /// <summary>
    /// 提醒时间
    /// </summary>
    public enum RemindTime
    {
        /// <summary>
        /// 暂无
        /// </summary>
        [Description("暂无")]
        NoSel = 0,
        /// <summary>
        /// 不提醒
        /// </summary>
        [Description("不提醒")]
        Not = 1,
        /// <summary>
        /// 提前15分钟
        /// </summary>
        [Description("提前15分钟")]
        FifteenMin = 2,
        /// <summary>
        /// 提前1小时
        /// </summary>
        [Description("提前1小时")]
        OneHour = 3,
        /// <summary>
        /// 提前24小时
        /// </summary>
        [Description("提前24小时")]
        OneDay = 4,
    }
    /// <summary>
    /// 提醒方式
    /// </summary>
    public enum RemindWay
    {
        /// <summary>
        /// 暂无
        /// </summary>
        [Description("暂无")]
        NoSel = 0,
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 1,
        /// <summary>
        /// 站内消息
        /// </summary>
        [Description("站内消息")]
        Site = 2,
        /// <summary>
        /// 手机短信
        /// </summary>
        [Description("手机短信")]
        PhoneMsg = 3,
    }
    public enum Emergency
    {
        /// <summary>
        /// 一般
        /// </summary>
        [Description("一般")]
        General = 1,
        /// <summary>
        /// 紧急
        /// </summary>
        [Description("紧急")]
        Emergency = 2,
        /// <summary>
        /// 重要
        /// </summary>
        [Description("重要")]
        Important = 3,
    }
    public enum Repeat
    {
        /// <summary>
        /// 不重复
        /// </summary>
        [Description("不重复")]
        Not = 1,
        /// <summary>
        /// 按天
        /// </summary>
        [Description("按天")]
        Day = 2,
        /// <summary>
        /// 按周
        /// </summary>
        [Description("按周")]
        Week = 3,
        /// <summary>
        /// 按月
        /// </summary>
        [Description("按月")]
        Month = 4,
    }
    public enum ContractType
    {
        /// <summary>
        /// 收款合同
        /// </summary>
        [Description("收款合同")]
        Rece = 1,
        /// <summary>
        /// 付款合同
        /// </summary>
        [Description("付款合同")]
        Pay = 2,
        /// <summary>
        /// 事务合同
        /// </summary>
        [Description("事务合同")]
        Affair = 3
    }

    public enum PayNum
    {
        /// <summary>
        /// 单次付款
        /// </summary>
        [Description("单次付款")]
        Single = 1,
        /// <summary>
        /// 多次付款
        /// </summary>
        [Description("多次付款")]
        Many = 2
    }

    public enum IsInvoice
    {
        /// <summary>
        /// 开票
        /// </summary>
        [Description("开票")]
        Yes = 1,
        /// <summary>
        /// 不开票
        /// </summary>
        [Description("不开票")]
        No = 0
    }

    public enum InvoiceToType
    {
        /// <summary>
        /// 个人
        /// </summary>
        [Description("个人")]
        Person = 1,
        /// <summary>
        /// 企业
        /// </summary>
        [Description("企业")]
        Company = 2
    }

    public enum InvoiceType
    {
        /// <summary>
        /// 企业增值税普通发票
        /// </summary>
        [Description("企业增值税普通发票")]
        Normal = 1,
        /// <summary>
        /// 企增值税专用发票业
        /// </summary>
        [Description("增值税专用发票")]
        Given = 2,
        /// <summary>
        /// 组织（非企业）增值税普通发票
        /// </summary>
        [Description("组织（非企业）增值税普通发票")]
        PersonNormal = 2

    }
}
