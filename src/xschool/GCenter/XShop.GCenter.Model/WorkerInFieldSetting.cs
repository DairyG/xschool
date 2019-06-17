using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;

namespace XShop.GCenter.Model
{
    public class WorkerInFieldSetting : IModel<int>
    {
        /// <summary>
        /// 到岗时间ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 到岗时间
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }


    }
}
