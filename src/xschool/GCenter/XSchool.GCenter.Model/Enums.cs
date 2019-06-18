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
        /// 一般账户
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
}
