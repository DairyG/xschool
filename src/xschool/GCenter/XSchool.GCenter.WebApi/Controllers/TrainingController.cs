using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Model;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class TrainingController : ApiBaseController
    {
        private readonly TrainingBusinesses _trainingBusinesses;
        public TrainingController(TrainingBusinesses trainingBusinesses)
        {
            _trainingBusinesses = trainingBusinesses;
        }

        /// <summary>
        /// [添加/编辑] 成长管理
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Edit([FromForm]Training model)
        {
            return _trainingBusinesses.AddOrEdit(model);
        }

        /// <summary>
        /// [列表] 成长管理
        /// </summary>
        /// <param name="employeeId">员工Id</param>
        /// <returns></returns>
        [HttpGet("{employeeId}")]
        public List<Training> Query(int employeeId)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("Id", OrderBy.Desc)
            };
            return _trainingBusinesses.Query(p => p.EmployeeId == employeeId, p => p, order).ToList();
        }

    }
}