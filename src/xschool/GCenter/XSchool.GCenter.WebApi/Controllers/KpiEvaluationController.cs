using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;

namespace XSchool.GCenter.WebApi.Controllers
{
    /// <summary>
    /// 绩效考核
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class KpiEvaluationController : ApiBaseController
    {
        private readonly KpiManageRecordBusiness _manageRecordBusiness;
        public KpiEvaluationController(KpiManageRecordBusiness manageRecordBusiness)
        {
            _manageRecordBusiness = manageRecordBusiness;
        }

        /// <summary>
        /// [添加/编辑] 考核模板
        /// </summary>
        /// <param name="modelDto">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Edit([FromForm]KpiEvaluationSubmitDto modelDto)
        {
            if (modelDto.Mode == OperationMode.Add)
            {
                return _manageRecordBusiness.Add(modelDto);
            }
            return Result.Fail("调试");
        }
    }
}