using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using XSchool.Core;
using XShop.GCenter.Businesses;
using XShop.GCenter.Model;
using XSchool.Query.Pageing;
using System.ComponentModel.DataAnnotations;

namespace XShop.GCenter.WebApi.Controllers
{
    public class CompanySearch
    {
        public string CompanyName { get; set; }
        public string LegalPersoon { get; set; }
    }

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
        /// 添加/编辑 开户信息
        /// </summary>
        /// <param name="model">传入的参数</param>
        /// <returns></returns>
        public Result EditBank([FromForm]BankInfo model)
        {
            return _bankInfo.AddOrEdit(model);
        }

        [HttpPost]
        [Description("查询")]
        public IPageCollection<Company> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]CompanySearch search)
        {
            var condition = new Condition<Company>();
            if (!string.IsNullOrWhiteSpace(search.CompanyName))
            {
                condition.And(p => p.CompanyName.Contains(search.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(search.LegalPersoon))
            {
                condition.And(p => p.LegalPerson.Contains(search.LegalPersoon));
            }
            return _company.Page(page, limit, condition.Combine());
        }

    }
}
