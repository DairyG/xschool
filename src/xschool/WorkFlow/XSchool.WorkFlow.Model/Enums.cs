using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XSchool.WorkFlow.Model
{
   public  class Enums
    {
        /// <summary>
        /// 禁启状态，[禁用，启用]
        /// </summary>
        public enum EDStatus
        {
            /// <summary>
            /// 禁用
            /// </summary>
            [Description("禁用")]
            Disable = 1,

            /// <summary>
            /// 启用
            /// </summary>
            [Description("启用")]
            Enable = 2,
        }
        /// <summary>
        ///  流程类型
        /// </summary>
        public enum WorkFType
        {
            /// <summary>
            /// 事务流程
            /// </summary>
            [Description("事务流程")]
            Transaction = 1,

            /// <summary>
            /// 费用流程
            /// </summary>
            [Description("费用流程")]
            Expenses = 2,
        }
    }
}
