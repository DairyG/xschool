using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XSchool.Core;
using XSchool.WorkFlow.Model.ViewModel;

namespace XSchool.WorkFlow.WebApi.Helper
{
    public static class ApiBusinessHelper
    {
        const string Gateway = "http://114.116.54.157:8000/";
       
        public static async Task<List<EmployeeDptJobBinding>> GetEmployeeDptJobByUserIdAsync(EmployeeDptJobDto model)
        {
            try
            {
                HttpClient client = new HttpClient();
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues["CompanyId"] = model.CompanyId.ToString();
                keyValues["DptId"] = model.DptId.ToString();
                keyValues["JobId"] = model.JobId.ToString();
                keyValues["LoadChildDptEmployee"] = model.LoadChildDptEmployee + "";
                keyValues["OnlySelf"] = model.OnlySelf + "";
                FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                var respMsg = await client.PostAsync($"{Gateway}api/v1/uc/employee/GetEmployees", content);
                // 不要错误的调用 了 PutAsync，应该是 PostAsync 
                Task<string> msgBody = respMsg.Content.ReadAsStringAsync();
                var employeeDptJobList = JsonConvert.DeserializeObject<List<EmployeeDptJobBinding>>(msgBody.Result);
                 return employeeDptJobList;
            }
            catch
            {

            }
            return null;
        }
    }
}
