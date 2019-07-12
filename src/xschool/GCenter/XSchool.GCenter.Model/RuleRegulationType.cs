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
    }
    public class RuleRegulationSearch
    {
        /// <summary>
        /// 制度标题
        /// </summary>
        public string Title { get; set; }
    }
}
