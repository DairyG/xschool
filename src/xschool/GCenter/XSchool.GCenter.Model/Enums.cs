using System.ComponentModel;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 银行账户类型
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// 基本账户
        /// </summary>
        [Description("基本账户")]
        Basic = 1,
        /// <summary>
        /// 一般账户
        /// </summary>
        [Description("一般账户")]
        Normal = 2,
    }

    /// <summary>
    /// 通用状态，[有效，无效]
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 有效
        /// </summary>
        [Description("有效")]
        Valid = 1,
        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]
        Invalid = 0,
    }

    /// <summary>
    /// 员工状态，[有效，无效]
    /// </summary>
    public enum PersonStatus
    {
        /// <summary>
        /// 试用
        /// </summary>
        [Description("试用")]
        Trial = 1,
        /// <summary>
        /// 转正
        /// </summary>
        [Description("转正")]
        Positive = 2,
        /// <summary>
        /// 离职
        /// </summary>
        [Description("离职")]
        Departure = 0,
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
    /// 基础信息类型
    /// </summary>
    public enum BasicInfoType
    {
        [Description("到岗时间")]
        /// <summary>
        /// 到岗时间
        /// </summary>
        WorkerInField = 1,
        [Description("面试方式")]
        /// <summary>
        /// 面试方式
        /// </summary>
        InterviewMethod = 2,
        [Description("学历")]
        /// <summary>
        /// 学历
        /// </summary>
        Education = 3,
        [Description("教育性质")]
        /// <summary>
        /// 教育性质
        /// </summary>
        Properties = 4,
    }

    public enum IsSystem
    {
        [Description("是")]
        /// <summary>
        /// 是
        /// </summary>
        Yes = 1,
        [Description("否")]
        /// <summary>
        /// 否
        /// </summary>
        No = 0,
    }
}
