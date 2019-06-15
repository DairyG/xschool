using System;
using System.Collections.Generic;
using XSchool.Core;
using XSchool.Helpers;
using XShop.GCenter.Businesses;
using XShop.GCenter.Model.ViewModel;

namespace XShop.GCenter.DoMain
{
    public class CompanyDo
    {
        private CompanyBusiness _companyBuiness;
        private BankInfoBusiness _bankInfoBusiness;
        public CompanyDo(CompanyBusiness companyBuiness, BankInfoBusiness bankInfoBusiness)
        {
            this._bankInfoBusiness = bankInfoBusiness;
            this._companyBuiness = companyBuiness;
        }

        public Result GetInfo(int id, bool isLoadBankInfo = false)
        {
            if (id <= 0)
            {
                return Result.Fail("主键编号不能小于等于零");
            }
            var com = _companyBuiness.GetSingle(p => p.Id.Equals(id)).Data.MapTo<CompanyDto>();
            if (com == null)
            {
                return Result.Fail("未找到数据");
            }
            //com.Bank = new List<Model.BankInfo>();
            //if (isLoadBankInfo)
            //{
            //    com.Bank = _bankInfoBusiness.Query(p => p.CompanyId.Equals(id)).Data;
            //}
            return Result.Success(com);
        }


    }
}
