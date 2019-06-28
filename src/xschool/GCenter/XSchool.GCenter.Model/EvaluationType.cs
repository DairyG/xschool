using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    public class EvaluationType : IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 考核分类
        /// </summary>
        public string Name { get; set; }
    }
}
