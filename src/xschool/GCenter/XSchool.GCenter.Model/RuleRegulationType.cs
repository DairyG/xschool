using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class RuleRegulationType : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 制度名称
        /// </summary>
        public string RuleName { get; set; }
    }
    public class RuleRegulation : IModel<int>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// TypeId
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 制度标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发布人Id
        /// </summary>
        public int PublisherId { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 附件Url地址
        /// </summary>
        public string EnclosureUrl { get; set; }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string PublisherName { get; set; }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 树结构typeId
        /// </summary>
        public string SelType { get; set; }
    }
    public class RuleRegulationPage
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// TypeId
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// TypeName
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 制度标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发布人Id
        /// </summary>
        public int PublisherId { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 附件Url地址
        /// </summary>
        public string EnclosureUrl { get; set; }
    }
    public class RuleRegulationSearch
    {
        /// <summary>
        /// 制度标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// TypeId
        /// </summary>
        public int TypeId { get; set; }
    }
    #region 制度阅读记录
    /// <summary>
    /// 制度阅读记录
    /// </summary>
    public class RuleRegulationRead : IModel<int>
    {
        public int Id { get; set; }
        public int RuleRegulationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string DptName { get; set; }
        public DateTime ReadDate { get; set; }
    }
    #endregion
    /// <summary>
    /// 阅读范围
    /// </summary>
    public class RuleRegulationReadRange : IModel<int>
    {
        public int Id { get; set; }
        public int RuleTypeId { get; set; }
        public int IsRead { get; set; }
        public DateTime ReadDate { get; set; }
        public OrgType TypeId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int DptId { get; set; }
        public string DptName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
    }
    public class DetailRuleRegulation
    {
        public RuleRegulation ruleRegulationDetail { get; set; }
        public ChooseUser chooseUser { get; set; }
    }
}
