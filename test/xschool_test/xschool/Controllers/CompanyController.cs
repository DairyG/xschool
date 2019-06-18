using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XShop.GCenter.DoMain;

namespace xschool.Controllers
{
    public class CompanyTestDo
    {
        private XShop.GCenter.Businesses.CompanyBusiness _companyBusiness;
        private XShop.GCenter.Businesses.BankInfoBusiness _bankInfoBusiness;
        public CompanyTestDo(XShop.GCenter.Businesses.CompanyBusiness companyBusiness, XShop.GCenter.Businesses.BankInfoBusiness bankInfoBusiness)
        {
            this._companyBusiness = companyBusiness;
            this._bankInfoBusiness = bankInfoBusiness;
        }

        public void Excute(Action action)
        {

        }
    }


    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class CompanyController:ControllerBase
    {
        private readonly CompanyDo _companyDo;
        public CompanyController(CompanyDo companyDo)
        {
            this._companyDo = companyDo;
        }

        public IActionResult Test()
        {
            return null;
        }
    }
}
