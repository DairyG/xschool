using System;
using System.Collections.Generic;
using XSchool.Core;

namespace XShop.GCenter.Model
{
    /// <summary>
    /// 公司
    /// </summary>
    public class Company : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 信用代码
        /// </summary>
        public string Credit { get; set; }

        /// <summary>
        /// 公司类型
        /// </summary>
        public string CompanyType { get; set; }

        /// <summary>
        /// 企业法人(法人代表)
        /// </summary>
        public string LegalPerson { get; set; }

        /// <summary>
        /// 注册资本
        /// </summary>
        public string RegisteredCapital { get; set; }

        /// <summary>
        /// 公司负责人
        /// </summary>
        public string Responsible { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string ResponsiblePhone { get; set; }

        /// <summary>
        /// 注册日期(成立日期)
        /// </summary>
        public DateTime RegisteredTime { get; set; }

        /// <summary>
        /// 营业期限
        /// </summary>
        public string BusinessDate { get; set; }

        /// <summary>
        /// 营业地址
        /// </summary>
        public string BusinessAddress { get; set; }

        /// <summary>
        /// 营业范围
        /// </summary>
        public string BusinessScope { get; set; }

        /// <summary>
        /// 公司Logo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string CompanyPhone { get; set; }

        /// <summary>
        /// 公司邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 办公地址
        /// </summary>
        public string OfficeAddress { get; set; }

        /// <summary>
        /// 公司网址
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// 公司介绍
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 公司文化
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// 发展历程
        /// </summary>
        public string History { get; set; }

        /// <summary>
        /// 1-有效，0-无效
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 开户行信息
        /// </summary>
        public IList<BankInfo> Bank { get; set; }

    }
}
