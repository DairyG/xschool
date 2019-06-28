using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;

namespace XSchool.GCenter.Model
{

    public class Evaluation : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 考核项目
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 考核项目分类ID
        /// </summary>
        public int EvaluationTypeId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 启用状态（枚举）
        /// </summary>
        public EDStatus Status { get; set; }
    }
}
