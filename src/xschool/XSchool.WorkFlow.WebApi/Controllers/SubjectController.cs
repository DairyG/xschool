using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.WorkFlow.Businesses;
using XSchool.WorkFlow.Model;
using static XSchool.WorkFlow.Model.Enums;

namespace XSchool.WorkFlow.WebApi.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SubjectBusiness subjectBusiness;
        public SubjectController(SubjectBusiness _subjectBusiness)
        {
            this.subjectBusiness = _subjectBusiness;
        }

        /// <summary>
        /// 添加或编辑流程组别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result AddOrEdit([FromForm]Subject model)
        {
            model.Status = EDStatus.Enable;
            return null;
        }
    }
}