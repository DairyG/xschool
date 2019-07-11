using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XSchool.WorkFlow.Model
{
   public  class Enums
    {
        /// <summary>
        /// 禁启状态，[1禁用，2启用]
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
        public enum FlowType
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

        /// <summary>
        ///  节点类型  1 审核节点，2 是抄送节点，3 是复盘节点
        /// </summary>
        public enum PassType
        {
            /// <summary>
            /// 审核节点 
            /// </summary>
            [Description("审核节点")]
            Audit = 1,

            /// <summary>
            /// 抄送节点
            /// </summary>
            [Description("抄送节点")]
            Copy = 2,

            /// <summary>
            /// 复盘节点
            /// </summary>
            [Description("复盘节点")]
            Summary = 3,

        }

        /// <summary>
        ///  业务类型 1流程管理可见者，2流程管理节点人员
        /// </summary>
        public enum BusinessType
        {
            /// <summary>
            /// 流程管理可见者 
            /// </summary>
            [Description("流程管理可见者")]
            Transaction = 1,

            /// <summary>
            /// 流程管理节点人员
            /// </summary>
            [Description("流程管理节点人员")]
            Expenses = 2,
        }
        
    }
}
