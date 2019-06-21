using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class PersonController : ApiBaseController
    {
        private readonly PersonBusinessWrapper _personWrapper;
        private readonly PersonBusiness _personBusiness;
        public PersonController(PersonBusiness personBusiness, PersonBusinessWrapper personWrapper)
        {
            _personWrapper = personWrapper;
            _personBusiness = personBusiness;
        }

        /// <summary>
        /// [添加/编辑] 人员
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Edit(PersonOperation operation, [FromForm]Person model)
        {
            return _personBusiness.AddOrEdit(model);
        }

    }
}