using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace XSchool.GCenter.WebApi.Helper
{
    public class Employee
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CompanyId { get; set; }
        public int DptId { get; set; }

        public int JobId { get; set; }

        public string EmployeeName { get; set; }

        public string CompanyName { get; set; }

        public string DptName { get; set; }

        public string JobName { get; set; }

        public IList<EmployeeDptJobBinding> ExtendCompanySettings { get; set; } = new List<EmployeeDptJobBinding>();
    }

    public class EmployeeDptJobBinding
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DptId { get; set; }

        public int JobId { get; set; }

        public int EmployeeId { get; set; }

        public string CompanyName { get; set; }
        public string DptName { get; set; }
        public string JobName { get; set; }
        public string EmployeeName { get; set; }
    }

    public static class RemoteRequestHelper
    {
        const string Gateway = "http://114.116.54.157:8000/";
        public static async Task<Employee> GetEmployeeByUserIdAsync(int userid)
        {
            HttpClient client = new HttpClient();
            try
            {
                var message = await client.GetAsync($"{Gateway}employee/getemployeebyuserid/{userid}");
                if (message.IsSuccessStatusCode)
                {
                    var value = await message.Content.ReadAsStringAsync();
                    var employee = Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(value);
                    return employee;
                }
            }
            catch
            {

            }
            return null;
        }
    }

}
