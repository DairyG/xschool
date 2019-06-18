using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XShop.GCenter.DoMain;
using System.Transactions;
using XSchool.Core;

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
            using (var trans = new TransactionScope())
            {
                action.Invoke();
                trans.Complete();
            }
        }

        public void Add()
        {
            var company = _companyBusiness.GetSingle(1);
            var bank = _bankInfoBusiness.GetSingle(1);

            this.Excute(() => {
                company.CompanyType = company.CompanyType + "__updated";
                _companyBusiness.Update(company);
                //throw new Exception("my exception");
                bank.OpenBank = bank.OpenBank + "__updated";
                _bankInfoBusiness.Update(bank);
            });
        }
    }


    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class CompanyController:ControllerBase
    {
        private readonly CompanyTestDo _companyDo;
        public CompanyController(CompanyTestDo companyDo)
        {
            this._companyDo = companyDo;
        }


        [HttpGet]
        public IActionResult Test()
        {
            _companyDo.Add();
            return new JsonResult(new { id = 10 });
        }
    }
}
