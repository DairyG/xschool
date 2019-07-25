using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Core;

namespace XSchool.WorkFlow.Model.ViewModel
{
    public class EmployeeDptJobBinding : IModel<int>
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DptId { get; set; }

        public int JobId { get; set; }

        public int EmployeeId { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
    }

    public class EmployeeDptJobBindingDto : EmployeeDptJobBinding
    {
        public string CompanyName { get; set; }
        public string DptName { get; set; }
        public string JobName { get; set; }
        public string EmployeeName { get; set; }
    }

    public class EmployeePageDto
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string UserName { get; set; }

        public string EmployeeNo { get; set; }

        public string LinkPhone { get; set; }

        public string OfficePhone { get; set; }

        public string Roles { get; set; }

        public bool IsOpenAccount { get; set; }

        public int? UserId { get; set; }

        public string Account { get; set; }

        public string CompanyName { get; set; }

        public string DptName { get; set; }

        public string JobName { get; set; }

        public IList<EmployeeDptJobBindingDto> Bindings { get; set; } = new List<EmployeeDptJobBindingDto>();
    }
    public class EmployeeDptJobDto
    {
        public int CompanyId { get; set; }
        public int DptId { get; set; }
        public int JobId { get; set; }
        public bool OnlySelf { get; set; }
        public bool LoadChildDptEmployee { get; set; }
    }

    public class EmployeeInfo
    {
        public int companyId { get; set; }
        public string companyName { get; set; }

        public int dptId { get; set; }

        public string dptName { get; set; }

        public int jobId { get; set; }
        public string jobName { get; set; }
        public int userId { get; set; }

        public int employeeId { get; set; }
        public string employeeName { get; set; }
    }


}
