using System;
using XSchool.Core;

namespace XShop.GCenter.Model
{
    public class Department: IModel<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 所属公司ID
        /// </summary>
        public int BelongCompany { get; set; }
        /// <summary>
        /// 上级部门ID
        /// </summary>
        public int HigherLevel { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DptName { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DptCode { get; set; }
        /// <summary>
        /// 部门正职
        /// </summary>
        public string DptPositions { get; set; }
        /// <summary>
        /// 部门正职电话
        /// </summary>
        public string PositionsPhone { get; set; }
        /// <summary>
        /// 部门副职
        /// </summary>
        public string DptDeputy { get; set; }
        /// <summary>
        /// 部门副职电话
        /// </summary>
        public string DeputyPhone { get; set; }
        /// <summary>
        /// 部门秘书
        /// </summary>
        public string DptSecretary { get; set; }
        /// <summary>
        /// 部门秘书电话
        /// </summary>
        public string SecretaryPhone { get; set; }
        /// <summary>
        /// 职责描述
        /// </summary>
        public string DutiesDescription { get; set; }
        /// <summary>
        /// 激活状态（0-不启用，1-启用）
        /// </summary>
        public int DptStatus { get; set; }
        /// <summary>
        /// 级联
        /// </summary>
        public string LevelMap { get; set; }
    }
}
