using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class BudgetDetailsController : ApiBaseController
    {
        private readonly BudgetDetailsBusiness _business;
        public BudgetDetailsController(BudgetDetailsBusiness business)
        {
            this._business = business;
        }
        /// <summary>
        /// [添加/修改]
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromForm]List<BudgetDetails> list)
        {
            return _business.Add(list);
        }
        /// <summary>
        /// 根据BudgetSetId获取
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        [HttpGet("{setId}")]
        public IList<BudgetDetails> Get(int setId)
        {
            return _business.Get(setId);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Result Delete(int id)
        {
            return _business.Delete(id);
        }
    }
}