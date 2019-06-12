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
    public class CompanyController:ApiBaseController
    {
        private readonly CompanyBusiness _business;
        public CompanyController(CompanyBusiness business)
        {
            this._business = business;
        }

        [HttpPost]
        [Description("添加公司")]
        public Result Add([FromForm]Company company)
        {
            return _business.Add(company);
        }


        [HttpPost]
        [Description("查询")]
        public IPageCollection<Company> Get([FromForm]int page,[Range(1,50)][FromForm]int limit,[FromForm]CompanySearch search)
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
            return _business.Page(page, limit, condition.Combine());
        }

    }
}
