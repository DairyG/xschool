using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Helpers;

namespace XSchool.GCenter.Model.ViewModel
{
    public class PersonDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 在职状态
        /// </summary>
        public PersonStatus Status { get; set; }
        /// <summary>
        /// 在职状态文本
        /// </summary>
        public string StatusText
        {
            get { return Status.GetDescription(); }
        }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeNo { get; set; }

        /// <summary>
        /// 性别，[1-男，2-女]
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone { get; set; }

        /// <summary>
        /// 职位Id
        /// </summary>
        public string PositionId { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 所属角色名称，多个可用,分隔
        /// </summary>
        public string Roles { get; set; }
    }
}
