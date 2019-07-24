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
                var message = await client.GetAsync($"{Gateway}api/v1/uc/employee/getemployeebyuserid/{userid}");
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
                //var url = $"{Gateway}api/v1/uc/employee/GetEmployees";
                //string data = JsonConvert.SerializeObject(model);
                //var bytes = Encoding.Default.GetBytes(data);
                //using (var client = new WebClient())
                //{
                //    client.Headers.Add("Content-Type", "application/json");
                //    var response = client.UploadData(url, "POST", bytes);
                //    string result = Encoding.Default.GetString(response);
                //    var employeeDptJobList= JsonConvert.DeserializeObject<List<EmployeeDptJobBinding>>(result);
                //    return employeeDptJobList;
                //}

            }
            catch
            {

            }
            return null;
        }
        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Result PostData<T>(string url, T t)
        {
            try
            {
                string data = JsonConvert.SerializeObject(t);
                var bytes = Encoding.Default.GetBytes(data);
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    var response = client.UploadData(url, "POST", bytes);
                    string result = Encoding.Default.GetString(response);
                    return JsonConvert.DeserializeObject<Result>(result);
                }
            }
            catch (Exception e)
            {
                return new Result() {  Succeed= false, Message= "请求失败，请重新尝试" };
            }
        }
    }
}
