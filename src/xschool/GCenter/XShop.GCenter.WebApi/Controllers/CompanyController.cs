using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XShop.GCenter.Businesses;
using XShop.GCenter.Model;
using XSchool.Query.Pageing;
using System.ComponentModel.DataAnnotations;
using XShop.GCenter.DoMain;
using System.Collections.Generic;

namespace XShop.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class CompanyController : ApiBaseController
    {
        private readonly CompanyDo _companyDo;
        private readonly CompanyBusiness _companyBusiness;
        private readonly BankInfoBusiness _bankInfoBusiness;
        public CompanyController(CompanyDo companyDo, CompanyBusiness companyBusiness, BankInfoBusiness bankInfoBusiness)
        {
            _companyDo = companyDo;
            _companyBusiness = companyBusiness;
            _bankInfoBusiness = bankInfoBusiness;
        }

        /// <summary>
        /// [列表] 公司
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <returns></returns>
        [HttpPost]
        public IPageCollection<Company> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<Company>();
            condition.And(p => p.Status == 1);
            return _companyBusiness.Page(page, limit, condition.Combine());
        }

        /// <summary>
        /// [详情] 公司
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Result GetInfo(int id)
        {
            return _companyDo.GetInfo(id, true);
        }

        /// <summary>
        /// [添加/编辑] 公司
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Edit([FromForm]Company model)
        {
            return _companyBusiness.AddOrEdit(model);
        }

        /// <summary>
        /// [删除] 公司
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Result Delete(int id)
        {
            return _companyBusiness.Del(id);
        }

        /// <summary>
        /// [列表] 开户信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet("{companyId}")]
        public IList<BankInfo> GetBank(int companyId)
        {
            var condition = new Condition<BankInfo>();
            condition.And(p => p.Status == 1 && p.CompanyId.Equals(companyId));
            return _bankInfoBusiness.Query(condition.Combine());
        }

        /// <summary>
        /// [添加/编辑] 开户信息
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        public Result EditBank([FromForm]BankInfo model)
        {
            return _bankInfoBusiness.AddOrEdit(model);
        }

        /// <summary>
        /// [删除] 开户信息
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Result DelBank(int id)
        {
            return _bankInfoBusiness.Del(id);
        }

    }
}
