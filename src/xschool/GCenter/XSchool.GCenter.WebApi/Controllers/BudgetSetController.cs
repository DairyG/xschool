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
    public class BudgetSetController : ApiBaseController
    {
        private readonly BudgetSetBusiness _business;
        public BudgetSetController(BudgetSetBusiness business)
        {
            this._business = business;
        }
        /// <summary>
        /// [添加/修改] 预算
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromForm]BudgetSet model)
        {
            Check(model);
            return _business.Add(model);
        }
        [HttpPost]
        public IList<BudgetSet> Get([FromForm]int dptId, [FromForm]int year) {
            return _business.Get(dptId, year);
        }
        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public BudgetSet GetSingle(int id)
        {
            return _business.GetSingle(id);
        }
        private Result Check(BudgetSet model)
        {
            if (model.DptId <= 0)
            {
                return Result.Fail("请选择部门！");
            }
            if (model.Year <= 0)
            {
                return Result.Fail("请选择年份！");
            }
            if (model.Total <= 0)
            {
                return Result.Fail("请输入总预算！");
            }
            return Result.Success();
        }
    }
}