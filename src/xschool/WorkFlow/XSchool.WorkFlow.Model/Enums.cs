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
            事务流程 = 1,

            /// <summary>
            /// 请款流程
            /// </summary>
            [Description("请款流程")]
            请款流程 = 2,

            /// <summary>
            /// 付款流程
            /// </summary>
            [Description("付款流程")]
            付款流程 = 3,
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


        /// <summary>
        ///  事务流程
        /// </summary>
        public enum PassStatus
        {
            /// <summary>
            /// 已撤销 
            /// </summary>
            [Description("已撤销")]
            Cancel = 1,

            /// <summary>
            /// 同意
            /// </summary>
            [Description("同意")]
            Agree = 2,
            /// <summary>
            /// 不同意
            /// </summary>
            [Description("不同意")]
            DisAgree = 3,

            /// <summary>
            /// 审批中
            /// </summary>
            [Description("审批中")]
            InApproval = 4,
        }


        
    }
}
