using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.GCenter.Model;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class CompanyController : ApiBaseController
    {
        private readonly CompanyBusinessWrapper _wrapper;
        private readonly CompanyBusiness _companyBusiness;
        private readonly BankInfoBusiness _bankInfoBusiness;
        public CompanyController(CompanyBusinessWrapper wrapper, CompanyBusiness companyBusiness, BankInfoBusiness bankInfoBusiness)
        {
            _wrapper = wrapper;
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
        public object Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<Company>();
            condition.And(p => p.Status == NomalStatus.Valid);
            return _companyBusiness.Page(page, limit, condition.Combine(), p => new { p.Id, p.CompanyName, p.Credit, p.CompanyType, p.LegalPerson, p.RegisteredCapital, p.RegisteredTime });
        }

        /// <summary>
        /// [详情] 公司
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Company GetInfo(int id)
        {
            return _wrapper.GetInfo(id, true);
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
            condition.And(p => p.Status == NomalStatus.Valid && p.CompanyId == companyId);
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
