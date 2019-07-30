using System;
using System.Collections.Generic;

namespace XSchool.GCenter.Model.ViewModel
{
    /// <summary>
    /// 模块
    /// </summary>
    public class PowerModuleDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 模板Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 路径
        /// </summary>		
        public string Url { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>		
        public int Pid { get; set; }

        /// <summary>
        /// 节点级别
        /// </summary>		
        public int Level { get; set; }

        /// <summary>
        /// 节点图标
        /// </summary>		
        public string IconName { get; set; }

        /// <summary>
        /// 排序，数字越小越靠前
        /// </summary>		
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 模块中的元素
        /// </summary>
        public List<PowerElement> Elements { get; set; } = new List<PowerElement>();
    }

    /// <summary>
    /// 模块下元素
    /// </summary>
    public class PowerModuleElementDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 模板Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 模块中的元素
        /// </summary>
        public List<PowerElement> Elements { get; set; } = new List<PowerElement>();
    }
}
