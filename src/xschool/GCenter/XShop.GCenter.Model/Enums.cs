using System.ComponentModel;

namespace XShop.GCenter.Model
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
    /// 银行账户类型
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
}
