using XSchool.Core;

namespace XShop.GCenter.Model
{
    /// <summary>
    /// 开户信息
    /// </summary>
    public class BankInfo : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string OpenBank { get; set; }

        /// <summary>
        /// 开户名称
        /// </summary>
        public string OpenBankName { get; set; }

        /// <summary>
        /// 开户账号
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 账户类型，[基本账户/一般账户]
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// 1-有效，0-无效
        /// </summary>
        public int Status { get; set; } = 1;
    }
}
