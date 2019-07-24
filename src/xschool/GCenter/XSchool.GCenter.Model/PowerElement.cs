using System;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 模块元素表
    /// </summary>
    public class PowerElement : IModel<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>		
        public int ModuleId { get; set; }

        /// <summary>
        /// 模块元素名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// DomId，注：可以理解为元素的Id
        /// </summary>		
        public string DomId { get; set; }

        /// <summary>
        /// 附加属性
        /// </summary>		
        public string Attr { get; set; }

        /// <summary>
        /// 图标
        /// </summary>		
        public string IconName { get; set; }

        /// <summary>
        /// 样式
        /// </summary>		
        public string Class { get; set; }

        /// <summary>
        /// 状态
        /// </summary>		
        public NomalStatus Status { get; set; }

        /// <summary>
        /// 是否为系统数据
        /// </summary>		
        public IsSystem IsSystem { get; set; }

        /// <summary>
        /// 排序，数字越小越靠前
        /// </summary>		
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 备注
        /// </summary>		
        public string Remarks { get; set; }

    }
}
