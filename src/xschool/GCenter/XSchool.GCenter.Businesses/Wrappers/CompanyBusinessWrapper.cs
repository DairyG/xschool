using XSchool.Businesses;
using XSchool.GCenter.Model;

namespace XSchool.GCenter.Businesses.Wrappers
{
    public class CompanyBusinessWrapper : BusinessWrapper
    {
        private CompanyBusiness _companyBuiness;
        private BankInfoBusiness _bankInfoBusiness;
        public CompanyBusinessWrapper(CompanyBusiness companyBuiness, BankInfoBusiness bankInfoBusiness)
        {
            _bankInfoBusiness = bankInfoBusiness;
            _companyBuiness = companyBuiness;
        }

        public Company GetInfo(int id, bool isLoadBankInfo = false)
        {
            var model = _companyBuiness.GetSingle(p => p.Id == id);
            if (model == null)
            {
                return null;
            }
            if (isLoadBankInfo)
            {
                model.Bank = _bankInfoBusiness.Query(p => p.CompanyId == id && p.Status == NomalStatus.Valid);
            }
            return model;
        }
    }
}
