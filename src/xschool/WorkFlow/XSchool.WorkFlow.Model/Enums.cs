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
        ///  事务流程单据状态
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

        /// <summary>
        ///  事务流程人员审批状态：-2 驳回，-1 拒绝，1等待审批，2同意，3未接收审批
        /// </summary>
        public enum AudioStatus
        {
            /// <summary>
            /// 驳回 
            /// </summary>
            [Description("驳回")]
            驳回 = -2,

            /// <summary>
            /// 拒绝
            /// </summary>
            [Description("拒绝")]
            拒绝 = -1,
            /// <summary>
            /// 等待审批
            /// </summary>
            [Description("等待审批")]
            等待审批 = 1,

            /// <summary>
            /// 同意
            /// </summary>
            [Description("同意")]
            同意 = 2,
            /// <summary>
            /// 未接收审批
            /// </summary>
            [Description("未接收审批")]
            未接收审批 = 3
        }
        

    }
}
