using System;
using System.Collections.Generic;
using System.Text;

namespace XSchool.GCenter.Model.ViewModel
{
    /// <summary>
    /// 员工角色 提交
    /// </summary>
    public class EmployeePowerRoleSubmitDto
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 角色Id集合
        /// </summary>
        public List<int> RoleIds { get; set; }
    }
}
