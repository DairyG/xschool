using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 合同
    /// </summary>
    public class Contract : IModel<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public String No { get; set; }

        /// <summary>
        /// 合同标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 合同开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 合同结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 合同总金额
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// 金额大写
        /// </summary>
        public String AmountStr { get; set; }

        /// <summary>
        /// 请款编号
        /// </summary>
        public String RelationNo { get; set; }

        /// <summary>
        /// 支付次数
        /// </summary>
        public int PayNum { get; set; }

        /// <summary>
        /// 支付详情
        /// </summary>
        public String PayItems { get; set; }

        /// <summary>
        /// 甲方名称
        /// </summary>
        public String JiaName { get; set; }

        /// <summary>
        /// 甲方联系地址
        /// </summary>
        public String JiaAddr { get; set; }

        /// <summary>
        /// 甲方联系人
        /// </summary>
        public String JiaContact { get; set; }

        /// <summary>
        /// 甲方联系电话
        /// </summary>
        public String JiaTel { get; set; }

        /// <summary>
        /// 甲方签字人
        /// </summary>
        public String JiaPerson { get; set; }

        /// <summary>
        /// 甲方签字时间
        /// </summary>
        public DateTime JiaSignDate { get; set; }

        /// <summary>
        /// 乙方名称
        /// </summary>
        public String YiName { get; set; }

        /// <summary>
        /// 乙方联系地址
        /// </summary>
        public String YiAddr { get; set; }

        /// <summary>
        /// 乙方联系人
        /// </summary>
        public String YiContact { get; set; }

        /// <summary>
        /// 乙方联系电话
        /// </summary>
        public String YiTel { get; set; }

        /// <summary>
        /// 乙方签字人
        /// </summary>
        public String YiPerson { get; set; }

        /// <summary>
        /// 乙方签字时间
        /// </summary>
        public DateTime YiSignDate { get; set; }

        /// <summary>
        /// 收款方银行
        /// </summary>
        public String RecBank { get; set; }

        /// <summary>
        /// 收款方银行名称
        /// </summary>
        public String RecBankName { get; set; }

        /// <summary>
        /// 收款方银行账号
        /// </summary>
        public String RecBankNo { get; set; }

        /// <summary>
        /// 收款方银行预留电话
        /// </summary>
        public String RecBankTel { get; set; }

        /// <summary>
        /// 是否开票
        /// </summary>
        public int Invoice { get; set; }

        /// <summary>
        /// 发票类型
        /// </summary>
        public int InvoiceToType { get; set; }

        /// <summary>
        /// 发票抬头
        /// </summary>
        public String InvoiceTitle { get; set; }

        /// <summary>
        /// 发票类型
        /// </summary>
        public int InvoiceType { get; set; }

        /// <summary>
        /// 发票税号
        /// </summary>
        public String InvoiceTaxNo { get; set; }

        /// <summary>
        /// 基本户银行
        /// </summary>
        public String InvoiceBank { get; set; }

        /// <summary>
        /// 基本户银行账号
        /// </summary>
        public String InvoiceBankNo { get; set; }

        /// <summary>
        /// 注册固定电话
        /// </summary>
        public String InvoiceTel { get; set; }

        /// <summary>
        /// 注册地址
        /// </summary>
        public String InvoiceAddr { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public String File { get; set; }

        /// <summary>
        /// 合同内容 
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DptId { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
