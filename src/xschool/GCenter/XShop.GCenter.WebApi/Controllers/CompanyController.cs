using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XShop.GCenter.Businesses;
using XShop.GCenter.Model;
using XSchool.Query.Pageing;
using System.ComponentModel.DataAnnotations;
using XSchool.Helpers;
using XShop.GCenter.Model.ViewModel;

namespace XShop.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class CompanyController : ApiBaseController
    {
        private readonly CompanyBusiness _company;
        private readonly BankInfoBusiness _bankInfo;
        public CompanyController(CompanyBusiness company, BankInfoBusiness bankInfo)
        {
            _company = company;
            _bankInfo = bankInfo;
        }

        /// <summary>
        /// 查询公司
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="limit">页大小</param>
        /// <returns></returns>
        [HttpPost]
        public IPageCollection<Company> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<Company>();
            condition.And(p => p.IsDelete == 1);
            return _company.Page(page, limit, condition.Combine());
        }

        /// <summary>
        /// 查询公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Result GetInfo(int id)
        {
            var modelCompany = _company.GetSingle(id);
            if (modelCompany == null)
            {
                return Result.Fail("请勿操作无效数据");
            }
            var modelDto = modelCompany.MapTo<CompanyDto>();
            modelDto.Bank = _bankInfo.GetSingle(id);
            return new Result<CompanyDto>()
            {
                Code = "00",
                Data = modelDto
            };
        }

        /// <summary>
        /// 添加/编辑 公司
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        [HttpPost]
        public Result Edit([FromForm]Company model)
        {
            return _company.AddOrEdit(model);
        }

        /// <summary>
        /// 删除 公司
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Result Delete(int id)
        {
            return _company.Del(id);
        }

        /// <summary>
        /// 添加/编辑 开户信息
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        public Result EditBank([FromForm]BankInfo model)
        {
            return _bankInfo.AddOrEdit(model);
        }

    }
}
