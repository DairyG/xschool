using System;
using System.Collections.Generic;
using XSchool.Helpers;

namespace XSchool.GCenter.Model.ViewModel
{
    public class AuthStrategyDto
    {
        /// <summary>
        /// 模块
        /// </summary>
        public List<TreeItem<PowerModuleDto>> Modules { get; set; } = new List<TreeItem<PowerModuleDto>>();

        /// <summary>
        /// 模块元素
        /// </summary>
        public List<PowerModuleElementDto> Elements { get; set; } = new List<PowerModuleElementDto>();
    }
}
